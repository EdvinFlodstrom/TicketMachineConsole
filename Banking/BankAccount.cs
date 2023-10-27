using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Banking
{
    public class BankAccount
    {
        private string pin;
        private string accountNumber;
        private int balance;
        private List<Transfer> successfullTransfers = new List<Transfer>();

        public BankAccount(string accountNumber, string pin)
        {
            this.accountNumber = accountNumber;
            this.pin = pin;
        }
        public BankAccount(string accountNumber, string pin, int balance) : this(accountNumber, pin)
        {
            this.accountNumber = accountNumber;
            this.pin = pin;
            this.balance = balance;
        }
        public String AccountNumber
        {
            get
            {
                return accountNumber;
            }
        }
        public int Balance
        {
            get
            {
                return balance;
            }
        }

        public bool Transfer(Transfer transfer)
        {
            if (transfer.FromAccountNr == accountNumber)
            {
                if (balance < transfer.Amount)
                {
                    return false;
                }
                balance -= transfer.Amount;
            }
            else
            {
                balance += transfer.Amount;
            }

            successfullTransfers.Add(transfer);
            return true;
        }

        public bool ValidatePin(string pin)
        {
            return pin == this.pin ? true : false;
        }

        public string GetAllTransfersAsString()
        {
            return successfullTransfers.ToString();
        }
        public List<Transfer> GetTransfers()
        {
            return successfullTransfers;
        }
    }
}
