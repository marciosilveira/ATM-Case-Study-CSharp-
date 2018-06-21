namespace ATM.Dto
{
    public class Account
    {
        private int _pin;
        public int AccountNumber { get; private set; }
        public decimal AvailableBalance { get; private set; }
        public decimal TotalBalance { get; private set; }

        public Account(int accountNumber, int pin, decimal totalBalance, decimal availableBalance)
        {
            AccountNumber = accountNumber;
            _pin = pin;
            TotalBalance = totalBalance;
            AvailableBalance = availableBalance;
        }

        public void Credit(decimal amount) => TotalBalance += amount;

        public void Debit(decimal amount)
        {
            AvailableBalance -= amount;
            TotalBalance -= amount;
        }

        public bool ValidatePin(int userPin) => userPin == _pin;
    }
}