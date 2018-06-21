namespace ATM.Domain.Interfaces
{
    public interface IBankDatabase
    {
        bool AuthenticateUser(int userAccountNumber, int userPin);
        void Credit(int userAccountNumber, decimal amount);
        void Debit(int userAccountNumber, decimal amount);
        decimal GetAvailableBalance(int userAccountNumber);
        decimal GetTotalBalance(int userAccountNumber);
    }
}
