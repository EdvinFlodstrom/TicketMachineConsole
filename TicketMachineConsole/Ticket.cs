using System;
using System.Collections.Generic;
using System.Text;

namespace Parking
{
    public class Ticket
    {
        private DateTime purchaseTime;
        private readonly int costPerHour;
        private readonly int price;

        /// <summary>
        /// A ticket. Cannot be changed once created.
        /// </summary>
        /// <param name="price">The amount of money payed for the ticket. A hole number.</param>
        /// <param name="costPerHour">The cost per hour to park. A hole number.</param>
        public Ticket(int price, int costPerHour)
        {
            this.price = price;
            this.costPerHour = costPerHour;
            purchaseTime = DateTime.Now;
        }
        /// <summary>
        /// Property to read cost per hour.
        /// </summary>
        public int CostPerHour
        {
            get
            {
                return costPerHour;
            }
        }
        /// <summary>
        /// Property to read the amout payed for the ticket.
        /// </summary>
        public int Price
        {
            get
            {
                return price;
            }
        }
        /// <summary>
        /// Returns the amount of time the ticket is valid for. 
        /// </summary>
        /// <returns>TimeSpan object with days, hours and minutes. 
        /// The number of seconds is set to zero.</returns>
        public TimeSpan GetValidTimeSpan()
        {
            int minutes = price * 60 / costPerHour;
            // hole number division
            int hours = minutes / 60;
            // rest
            minutes = minutes % 60;
            // hole number division
            int days = hours / 24;
            // rest
            hours = hours % 24;
            return new TimeSpan(days, hours, minutes, 0);
        }
        /// <summary>
        /// Returns the date and time the ticket is valid to.
        /// </summary>
        /// <returns>A DateTime object for the validity date.</returns>
        public DateTime GetValidTo()
        {
            DateTime date;
            date = purchaseTime.Add(GetValidTimeSpan());
            return date;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            TimeSpan time = GetValidTimeSpan();
            return "Parking ticket valid for:" + Environment.NewLine +
                time.Days + " days" + Environment.NewLine +
                time.Hours + " hours" + Environment.NewLine +
                time.Minutes + " minutes" + Environment.NewLine +
                Environment.NewLine +
                "Valid to: " + GetValidTo().ToString();
        }
    }
}
