using ATM.Domain.Enumerator;

namespace ATM.Domain
{
    public class WithdrawalService
    {
        BankDatabase _bankDatabase;
        CashDispenser _cashDispenser;

        public WithdrawalService(BankDatabase bankDatabase, CashDispenser cashDispenser)
        {
            _bankDatabase = bankDatabase;
            _cashDispenser = cashDispenser;
        }

        public WithdrawalDebitReturn Debit(int accountNumber, decimal amount)
        {
            var availableBalance = _bankDatabase.getAvailableBalance(accountNumber);
            if (amount > availableBalance)
                return WithdrawalDebitReturn.InsufficientFunds;

            if (!_cashDispenser.IsSufficiantCashAvailable(amount))
                return WithdrawalDebitReturn.InsufficientCash;

            _bankDatabase.Debit(accountNumber, amount);
            _cashDispenser.DispenseCash(amount);

            return WithdrawalDebitReturn.Success;
        }
    }
}
