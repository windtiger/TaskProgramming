using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Part13
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

        public void Tranfer(BankAccount bank, int amount)
        {
            this.Withdraw(amount);
            bank.Deposit(amount);
        }
    }
}
