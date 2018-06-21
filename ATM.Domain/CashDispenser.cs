﻿using ATM.Domain.Interfaces;

namespace ATM.Domain
{
    public class CashDispenser : ICashDispenser
    {
        private const int INITIAL_COUNT = 500; // the default initial number of bills in the cash dispenser
        private int _billCount; // number of $20 bills remaining

        public CashDispenser()
        {
            _billCount = INITIAL_COUNT;
        }

        public void DispenseCash(decimal amount) => _billCount -= (int)(amount / 20);
        public bool IsSufficiantCashAvailable(decimal amount) => _billCount >= (int)(amount / 20);
    }
}
