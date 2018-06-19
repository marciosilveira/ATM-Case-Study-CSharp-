using ATM.Domain;
using ATM.Domain.Enumerator;
using static System.Threading.Thread;

namespace ATM.Application
{
    public class Deposit : Transaction
    {
        private Keypad _keypad;
        private DepositService _depositService;
        private const int CANCELED = 0;

        public Deposit(int userAccountNumber, Screen atmScreen,
            Keypad atmKeypad, DepositService depositService)
            : base(userAccountNumber, null, atmScreen)
        {
            _keypad = atmKeypad;
            _depositService = depositService;
        }

        public override void Execute()
        {
            var _amount = PromptForDepositAmount();

            if (_amount != CANCELED)
            {
                Screen.DisplayMessage("Please insert a deposit envelope containing ");
                Screen.DisplayDollarAmount(_amount);
                Screen.DisplayMessageLine(" in the deposit slot.");

                var depositCrediReturn = _depositService.Credit(base.AccountNumber, _amount);

                if (depositCrediReturn == DepositDebitReturn.Success)
                {
                    Screen.DisplayMessageLine(
                        "Your envelope has been received.\n" +
                        "The money just deposited will not be available " +
                        "until we \nverify the amount of any " +
                        "enclosed cash, and any enclosed checks clear.");
                }
                else
                    Screen.DisplayMessageLine("You did not insert an envelope, so the ATM has canceled your transaction.");
            }
            else
                Screen.DisplayMessageLine("Canceling transaction.");

            Sleep(3000);
        }

        private decimal PromptForDepositAmount()
        {
            Screen.DisplayMessageLine("Please input a deposit amount in CENTS (or 0 to cancel): ");
            int input = _keypad.GetInput();
            return input == CANCELED ? CANCELED : input / 100M;
        }
    }
}

