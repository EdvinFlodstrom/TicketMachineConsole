using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Parking;

namespace UnitTestTicketMachine
{
    /// <summary>
    /// Test for the TicketMachine class.
    /// 
    /// NOTICE (TODO):
    /// Asserting times may differ with one minute in rare circumstances.
    /// The time is read twice from the operating system. 
    /// </summary>
    [TestClass]
    public class ParkingMachineTest
    {
        [TestMethod]
        public void ValidInsertMoney()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine(20);

            // Act
            machine.InsertMoney(30);
            machine.InsertMoney(60);

            // Assert
            Assert.AreEqual(90, machine.CurrentTotal);
        }
        [TestMethod]
        public void InvalidInsertMoney()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine(20);

            // Act
            machine.InsertMoney(-30);

            // Assert
            Assert.AreEqual(0, machine.CurrentTotal);
        }
        [TestMethod]
        public void CostPerHour()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine(40);

            // Act
            machine.InsertMoney(30);

            // Assert
            Assert.AreEqual(40, machine.CostPerHour);
        }
        [TestMethod]
        public void GetParkingTimeSpan()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine(30);
            int days = 5;
            int hours = 3;
            int minutes = 2;

            TimeSpan expectedParkingTime;
            expectedParkingTime = new TimeSpan(
                days: days,
                hours: hours,
                minutes: minutes,
                seconds: 0);

            // Act
            machine.InsertMoney(1); // 2 minutes
            machine.InsertMoney(hours * 30);
            machine.InsertMoney(days * 24 * 30);


            // Assert
            Assert.AreEqual(expectedParkingTime, machine.GetParkingTimeSpan());
        }
        [TestMethod]
        public void GetValidTo()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine(30);
            int days = 5;
            int hours = 3;
            int minutes = 2;

            TimeSpan expectedParkingTime;
            expectedParkingTime = new TimeSpan(
                days: days,
                hours: hours,
                minutes: minutes,
                seconds: 0);

            machine.InsertMoney(1); // 2 minutes
            machine.InsertMoney(hours * 30);
            machine.InsertMoney(days * 24 * 30);

            DateTime expectedValidTo = DateTime.Now;
            expectedValidTo = expectedValidTo.Add(expectedParkingTime);

            // Act
            DateTime validTo = machine.GetValidTo();

            // Assert
            Assert.AreEqual(expectedValidTo.ToString(), validTo.ToString());
        }

        [TestMethod]
        public void BuyTicket30Min()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine(20);

            // Act
            machine.InsertMoney(10);
            Ticket ticket = machine.BuyTicket();

            // Assert
            Assert.AreEqual(TimeToTicketText(days: 0, hours: 0, minutes: 30), ticket.ToString());
        }
        [TestMethod]
        public void BuyTicket3Hour()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine(20);

            // Act
            machine.InsertMoney(60);
            Ticket ticket = machine.BuyTicket();

            // Assert
            Assert.AreEqual(TimeToTicketText(days: 0, hours: 3, minutes: 0), ticket.ToString());
        }
        [TestMethod]
        public void BuyTicket4Day()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine(20);

            // Act
            machine.InsertMoney(20 * 24 * 4);
            Ticket ticket = machine.BuyTicket();

            // Assert
            Assert.AreEqual(TimeToTicketText(days: 4, hours: 0, minutes: 0), ticket.ToString());
        }
        [TestMethod]
        public void BuyTicket2Day3Hour15Min()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine(20);
            int money = 2 * 24 * 20 + 3 * 20 + 5;

            // Act
            machine.InsertMoney(money);
            Ticket ticket = machine.BuyTicket();

            // Assert
            Assert.AreEqual(TimeToTicketText(days: 2, hours: 3, minutes: 15), ticket.ToString());
        }
        [TestMethod]
        public void MultipleBuyTicket_Total()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine(20);
            int money = 2 * 24 * 20 + 3 * 20 + 5;

            // Act
            machine.InsertMoney(money);
            machine.BuyTicket();
            machine.InsertMoney(money);
            machine.BuyTicket();
            machine.InsertMoney(money);
            machine.BuyTicket();

            // Assert
            Assert.AreEqual(3 * money, machine.Total);
        }
        [TestMethod]
        public void MultipleBuyTicket_CurrentTotal_ZeroAfterBuy()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine(20);
            int money = 2 * 24 * 20 + 3 * 20 + 5;

            // Act
            machine.InsertMoney(money);
            machine.BuyTicket();
            machine.InsertMoney(money);
            machine.BuyTicket();
            machine.InsertMoney(money);
            machine.BuyTicket();

            // Assert
            Assert.AreEqual(0, machine.CurrentTotal);
        }
        [TestMethod]
        public void Cancel_CurrentTotal_Zeroed()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine(20);

            // Act
            machine.InsertMoney(100);
            int refund = machine.Cancel();

            // Assert
            Assert.AreEqual(0, machine.CurrentTotal);
        }
        [TestMethod]
        public void Cancel_AmoutRefunded()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine(20);

            // Act
            machine.InsertMoney(100);
            int refund = machine.Cancel();

            // Assert
            Assert.AreEqual(100, refund);

        }
        private string TimeToTicketText(int days, int hours, int minutes)
        {
            DateTime validToTime = DateTime.Now;
            validToTime = validToTime.Add(new TimeSpan(days, hours, minutes, 0));
            return "Parking ticket valid for:" + Environment.NewLine +
                days + " days" + Environment.NewLine +
                hours + " hours" + Environment.NewLine +
                minutes + " minutes" + Environment.NewLine +
                Environment.NewLine +
                "Valid to: " + validToTime.ToString();
        }

    }
}
