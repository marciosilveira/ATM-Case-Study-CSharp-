using ATM.Domain;
using ATM.Domain.Enumerator;
using System.Collections.Generic;
using static System.Console;
using static System.Threading.Thread;

namespace ATM.Application
{
    public class Withdrawal : Transaction
    {
        private Keypad _keypad;
        private WithdrawalService _withdrawalService;
        private Dictionary<WithdrawalDebitReturn, string> _withdrawalDebitMessage;

        private const int CANCELED = 6;

        public Withdrawal(int userAccount, Screen atmScreen,
              Keypad atmKeypad, WithdrawalService withdrawalService)
            : base(userAccount, null, atmScreen)
        {
            _keypad = atmKeypad;
            _withdrawalService = withdrawalService;
            InitilizeResource();
        }

        private void InitilizeResource()
        {
            _withdrawalDebitMessage = new Dictionary<WithdrawalDebitReturn, string>()
            {
                { WithdrawalDebitReturn.Success, "Your cash has been dispensed. Please take your cash now." },
                { WithdrawalDebitReturn.InsufficientFunds, "Insufficient funds in your account.\n\nPlease choose a smaller amount." },
                { WithdrawalDebitReturn.InsufficientCash, "Insufficient cash available in the ATM.\n\nPlease choose a smaller amount." }
            };
        }

        private int DisplayMenuOfAmounts()
        {
            int userChoice = 0;

            int[] amounts = { 0, 20, 40, 60, 100, 200 };

            while (userChoice == 0)
            {
                Clear();
                Screen.DisplayMessageLine("\nWITHDRAWAL MENU: ");
                Screen.DisplayMessageLine("1 - $20");
                Screen.DisplayMessageLine("2 - $40");
                Screen.DisplayMessageLine("3 - $60");
                Screen.DisplayMessageLine("4 - $100");
                Screen.DisplayMessageLine("5 - $200");
                Screen.DisplayMessageLine("6 - Cancel transaction");
                Screen.DisplayMessage("\nChoose a withdrawal amount: ");

                int input = _keypad.GetInput();

                switch (input)
                {
                    case 1: // if the user chose a withdrawal amount 
                    case 2: // (i.e., chose option 1, 2, 3, 4 or 5), return the
                    case 3: // corresponding amount from amounts array
                    case 4:
                    case 5:
                        userChoice = amounts[input]; // save user's choice
                        break;
                    case CANCELED: // the user chose to cancel
                        userChoice = CANCELED; // save user's choice
                        break;
                    default: // the user did not enter a value from 1-6
                        Screen.DisplayMessageLine("\nInvalid selection. Try again.");
                        Sleep(2000);
                        break;
                }
            }

            return userChoice;
        }

        public override void Execute()
        {
            bool isCashDispensed = false;

            do
            {
                var _amount = (decimal)DisplayMenuOfAmounts();

                if (_amount == CANCELED)
                {
                    Screen.DisplayMessageLine("\nCancelling transaction...");
                    Sleep(3000);
                    return;
                }

                var withdrawalDebitReturn = _withdrawalService.Debit(base.AccountNumber, _amount);
                isCashDispensed = IsCashDispensed(withdrawalDebitReturn);
            } while (!isCashDispensed);
        }

        private bool IsCashDispensed(WithdrawalDebitReturn withdrawalDebitReturn)
        {
            Screen.DisplayMessageLine(_withdrawalDebitMessage[withdrawalDebitReturn]);
            return withdrawalDebitReturn == WithdrawalDebitReturn.Success;
        }
    }
}
