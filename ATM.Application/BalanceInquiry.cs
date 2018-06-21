using ATM.Domain.Interfaces;
using static System.Threading.Thread;

namespace ATM.Application
{
    public class BalanceInquiry : Transaction
    {
        private IBankDatabase _bankDatabase;

        public BalanceInquiry(int userAccountNumber, Screen atmScreen, IBankDatabase bankDatabase)
            : base(userAccountNumber, atmScreen)
        {
            _bankDatabase = bankDatabase;
        }

        public override void Execute()
        {
            var availableBalance = _bankDatabase.GetAvailableBalance(AccountNumber);
            var totalBalance = _bankDatabase.GetTotalBalance(AccountNumber);

            base.Screen.DisplayMessageLine("\nBalance Information:");
            base.Screen.DisplayMessage(" - Available balance: ");
            base.Screen.DisplayDollarAmount(availableBalance);
            base.Screen.DisplayMessage("\n - Total balance:     ");
            base.Screen.DisplayDollarAmount(totalBalance);
            base.Screen.DisplayMessageLine(string.Empty);

            Sleep(5000);
        }
    }
}