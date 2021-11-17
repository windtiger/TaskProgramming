using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Part11
{
    public class BankAccount
    {
        int _balance;
        public int Balance { get => _balance; }

        public void Deposit(int amount)
        {
            Interlocked.Add(ref _balance, amount);
        }

        public void Withdraw(int amount)
        {
            Interlocked.Add(ref _balance, -amount);
        }
    }
}
