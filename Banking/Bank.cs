﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Banking
{
    public class Bank
    {
        // all accounts in the bank <account number, bank account>
        private Dictionary<String, BankAccount> accounts = new Dictionary<string, BankAccount>();
        private AccountNrGenerator accountNrGenerator = new AccountNrGenerator();
        private List<Transfer> failingTransfers = new List<Transfer>();

        public Bank()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pin"></param>
        /// <returns>Returns the account number if successful, otherwise null.</returns>
        public string AddAccount(string pin)
        {
            if (pin == "" || pin == null)
            {
                return null;
            }
            BankAccount bankAccount = new BankAccount(accountNrGenerator.GetUniqieAccountNr(), pin);
            accounts.Add(bankAccount.AccountNumber.ToString(), bankAccount);

            return bankAccount.AccountNumber.ToString();
        }
        public string AddAccount(string pin, int balance)
        {
            if (pin == "" || pin == null)
            {
                return null;
            }
            if (balance < 0)
            {
                balance = 0;
            }
            BankAccount bankAccount = new BankAccount(accountNrGenerator.GetUniqieAccountNr(), pin, balance);
            accounts.Add(bankAccount.AccountNumber.ToString(), bankAccount);

            return bankAccount.AccountNumber.ToString();
        }
        public int GetBalance(string accountNr, string pin)
        {           
            try
            {
                return accounts[accountNr].Balance;
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Call to transfer will fail either pin is wrong or the amount is bigger than allowed.
        /// </summary>
        /// <param name="transfer"></param>
        /// <param name="pin"></param>
        /// <returns>True if successful.</returns>
        public bool Transfer(Transfer transfer, string pin)
        {
            // check that amount > 0, else return false
            if (!accounts[transfer.FromAccountNr].ValidatePin(pin))
            {
                return false;
            }
            if (transfer.Amount > 0)
            {
                try
                {
                    accounts[transfer.ToAccountNr].Transfer(transfer);
                    accounts[transfer.FromAccountNr].Transfer(transfer);
                }
                catch { }

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Transfer> GetTransfers(string accountNr, string pin)
        {
            try
            {
                accounts[accountNr].ValidatePin(pin);
            }
            catch
            {
                return null;
            }
            if (accounts.ContainsKey(accountNr))
            {
                return accounts[accountNr].GetTransfers();
            }
            return null;
        }        
    }
}
