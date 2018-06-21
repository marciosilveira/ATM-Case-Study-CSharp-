using ATM.Domain.Enumerator;
using ATM.Domain.Interfaces;

namespace ATM.Domain
{
    public class DepositService
    {
        IBankDatabase _bankDatabase;
        DepositSlot _depositSlot;

        public DepositService(IBankDatabase bankDatabase, DepositSlot depositSlot)
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
