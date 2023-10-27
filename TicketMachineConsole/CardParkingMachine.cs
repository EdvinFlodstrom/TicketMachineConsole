using System;
using System.Collections.Generic;
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
            
        }
        public void SetAccountNrAndPin(String accountNr, String pin)
        {
            throw new NotImplementedException();
        }
        public bool IsSetAccountNrAndPin()
        {
            throw new NotImplementedException();
        }
        public new Ticket BuyTicket()
        {
            throw new NotImplementedException();
        }

    }
}
