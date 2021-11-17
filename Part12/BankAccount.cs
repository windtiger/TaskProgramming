using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Part12
{
    public class BankAccount
    {
        int _balance;
        public int Balance { get => _balance; }

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
