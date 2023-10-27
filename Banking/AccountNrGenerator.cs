using System;
using System.Collections.Generic;
using System.Text;

namespace Banking
{
    public class AccountNrGenerator
    {
        private int currentAccountNr = 1000;
        
        public AccountNrGenerator()
        {

        }   
        
        public string GetUniqieAccountNr()
        {
            currentAccountNr++;
            return currentAccountNr.ToString();
        }
    }
}
