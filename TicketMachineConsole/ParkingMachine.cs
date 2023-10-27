using System;
using System.Collections.Generic;
using System.Text;


namespace Parking
{
    /// <summary>
    /// An object of the class ParkingMachine represents a parking machine.
    /// 
    /// Insert money first. 
    /// 
    /// Then buy a ticket or select cancel to refund.
    /// </summary>
    public class ParkingMachine
    {
        // There is two places to save money.
        // Total in the machine, from all finnished purchases.
        private int total;

        private int currentTotal;

        // Cost to park.
        private int costPerHour;

        public ParkingMachine(int costPerHour)
        {
            total = 0;
            currentTotal = 0;
            this.costPerHour = costPerHour;
        }
        public int CurrentTotal
        {
            get
            {
                return currentTotal;
            }
            protected set
            {
                currentTotal = value;
            }
        }
        public int Total
        {
            get
            {
                return total;
            }

        }
        public int CostPerHour
        {
            get
            {
                return costPerHour;
            }
        }

        public void InsertMoney(int amount)
        {
            if (amount > 0)
            {
                CurrentTotal += amount;
            }
        }

        public Ticket BuyTicket()
        {
            Ticket ticket = new Ticket(currentTotal, costPerHour);

            total += CurrentTotal;
            CurrentTotal = 0;

            return ticket;
        }

        public TimeSpan GetParkingTimeSpan()
        {
            Ticket tempTicket = new Ticket(currentTotal, costPerHour);
            return tempTicket.GetValidTimeSpan();
        }
        public DateTime GetValidTo()
        {
            Ticket tempTicket = new Ticket(currentTotal, costPerHour);
            return tempTicket.GetValidTo();
        }

        public int Cancel()
        {
            int temp = currentTotal;
            CurrentTotal = 0;
            return temp;
        }
    }
}
