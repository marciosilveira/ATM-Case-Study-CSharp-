using ATM.Domain;
using static System.Threading.Thread;

namespace ATM.Application
{
    public class BalanceInquiry : Transaction
    {
        public BalanceInquiry(int userAccountNumber, Screen atmScreen, BankDatabase atmBankDatabase)
            : base(userAccountNumber, atmBankDatabase, atmScreen) { }

        public override void Execute()
        {
            var availableBalance = base.BankDatabase.getAvailableBalance(AccountNumber);
            var totalBalance = base.BankDatabase.getTotalBalance(AccountNumber);

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