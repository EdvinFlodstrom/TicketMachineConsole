using System;
using System.Collections.Generic;
using System.Text;

namespace Banking
{
    public class Transfer
    {
        private readonly string fromAccountNr;
        private readonly string toAccountNr;
        private readonly int amount;
        public Transfer(string fromAccountNr, string toAccountNr, int amount)
        {
            this.fromAccountNr = fromAccountNr;
            this.toAccountNr = toAccountNr;
            this.amount = amount;
        }
        public string FromAccountNr
        {
            get
            {
                return fromAccountNr;
            }

        }

        public string ToAccountNr
        {
            get
            {
                return toAccountNr;
            }
        }
        public int Amount
        {
            get
            {
                return amount;
            }
        }
    }
}
