using System;
using System.Collections.Generic;
using System.Text;

namespace Part10
{
    public class BankAccount
    {
        int _balance;
        public int Balance { get => _balance; }

        private readonly object _lock = new object();

        public void DepositByLock(int amount)
        {
            lock (_lock)
            {
                _balance += amount;
            }
        }

        public void WithdrawByLock(int amount)
        {
            lock (_lock)
            {
                _balance -= amount;
            }
        }

        public void Deposit(int amount)
        {
            _balance += amount;
        }

        public void Withdraw(int amount)
        {
            _balance -= amount;
        }
    }
}
