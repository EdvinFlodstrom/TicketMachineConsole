using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    public class TicketChecker
    {
        int fineCostPerHour;
        public TicketChecker(int fineCostPerHour)
        {
            this.fineCostPerHour = fineCostPerHour;
        }
        public bool IsValid(Ticket ticket)
        {
            if (DateTime.Now > ticket.GetValidTo())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public int FineCalculator(Ticket ticket, int hoursPassed)
        {
            DateTime currentTime = DateTime.Now.Add(new TimeSpan(0,hoursPassed,0,0));
            if (currentTime < ticket.GetValidTo())
            {
                return 0;
            }
            int fine = 500;
            DateTime ticketTime = ticket.GetValidTo();

            TimeSpan timeDifference = (currentTime - ticketTime).Duration();
            int totalHours = timeDifference.Hours;
            totalHours += (timeDifference.Days * 24);
            totalHours += (timeDifference.Minutes / 60);
            totalHours += (timeDifference.Seconds / 3600);

            return fine + (totalHours * fineCostPerHour);
        }
    }
}
