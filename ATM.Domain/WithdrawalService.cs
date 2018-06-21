using ATM.Domain.Enumerator;
using ATM.Domain.Interfaces;

namespace ATM.Domain
{
    public class WithdrawalService
    {
        IBankDatabase _bankDatabase;
        ICashDispenser _cashDispenser;

        public WithdrawalService(IBankDatabase bankDatabase, ICashDispenser cashDispenser)
        {
            _bankDatabase = bankDatabase;
            _cashDispenser = cashDispenser;
        }

        public WithdrawalDebitReturn Debit(int accountNumber, decimal amount)
        {
            var availableBalance = _bankDatabase.GetAvailableBalance(accountNumber);
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
