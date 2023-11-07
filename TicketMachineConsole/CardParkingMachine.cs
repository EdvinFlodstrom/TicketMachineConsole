using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Banking;

namespace Parking
{
    public class CardParkingMachine : ParkingMachine
    {
        private Bank bank;
        private String accountNr;
        private String customerAccountNr;
        private String customerPin;

        public CardParkingMachine(int costPerHour, Bank bank, String accountNr) : base(costPerHour)
        {
            this.bank = bank;
            this.accountNr = accountNr;
        }
        public void SetAccountNrAndPin(String accountNr, String pin)
        {
            this.customerAccountNr = accountNr;
            this.customerPin = pin;
        }
        public bool IsSetAccountNrAndPin()
        {
            if (customerAccountNr == null || customerPin == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public new Ticket BuyTicket()
        {
            try
            {               
                if (bank.Transfer(new Transfer(customerAccountNr, accountNr, base.CurrentTotal), customerPin))
                {
                    Ticket ticket = base.BuyTicket();

                    customerAccountNr = null;
                    customerPin = null;

                    return ticket;
                }
                else
                {
                    return null;
                }                
            }
            catch
            {
                return null;
            }
        }
    }
}
