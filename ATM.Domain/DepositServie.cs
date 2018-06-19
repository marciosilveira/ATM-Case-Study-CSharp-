using ATM.Domain.Enumerator;

namespace ATM.Domain
{
    public class DepositService
    {
        BankDatabase _bankDatabase;
        DepositSlot _depositSlot;

        public DepositService(BankDatabase bankDatabase, DepositSlot depositSlot)
        {
            _bankDatabase = bankDatabase;
            _depositSlot = depositSlot;
        }

        public DepositDebitReturn Credit(int accountNumber, decimal amount)
        {
            if (_depositSlot.IsEnvelopeReceived)
            {
                _bankDatabase.Credit(accountNumber, amount);
                return DepositDebitReturn.Success;
            }

            return DepositDebitReturn.EnvelopNotReceived;
        }
    }
}
