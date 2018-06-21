namespace ATM.Application
{
    public abstract class Transaction
    {
        public int AccountNumber { get; private set; }
        public Screen Screen { get; private set; }

        public Transaction(int userAccountNumber, Screen atmScreen)
        {
            this.AccountNumber = userAccountNumber;
            this.Screen = atmScreen;
        }

        public abstract void Execute();
    }
}
