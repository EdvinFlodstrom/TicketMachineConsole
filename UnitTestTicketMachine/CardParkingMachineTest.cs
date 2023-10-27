using Banking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parking;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestTicketMachine
{
    [TestClass]
    public class CardParkingMachineTest
    {
        [TestMethod]
        public void InsertMoney()
        {
            // Arrange
            int costPerHour = 30;
            Bank bank = new Bank();
            bank.AddAccount("test");
            String machinePin = "machine";
            String machineAccountNr = bank.AddAccount(machinePin);
            String customerPin = "customer";
            String customerAccountNr = bank.AddAccount(customerPin);
           
            CardParkingMachine machine = new CardParkingMachine(costPerHour, bank, machineAccountNr);
            
            // Act
            machine.InsertMoney(100);
            machine.InsertMoney(200);

            // Assert
            Assert.AreEqual(300, machine.CurrentTotal);
        }
        [TestMethod]
        public void InsertMoney_Invalid()
        {
            // Arrange
            int costPerHour = 30;
            Bank bank = new Bank();
            bank.AddAccount("test");
            String machinePin = "machine";
            String machineAccountNr = bank.AddAccount(machinePin);
            String customerPin = "customer";
            String customerAccountNr = bank.AddAccount(customerPin);

            CardParkingMachine machine = new CardParkingMachine(costPerHour, bank, machineAccountNr);
            
            // Act
            machine.InsertMoney(200);
            machine.InsertMoney(-100);

            // Assert
            Assert.AreEqual(200, machine.CurrentTotal);
        }
        [TestMethod]
        public void Cancel_CurrentTotalIsZero()
        {
            // Arrange
            int costPerHour = 30;
            Bank bank = new Bank();
            bank.AddAccount("test");
            String machinePin = "machine";
            String machineAccountNr = bank.AddAccount(machinePin);
            String customerPin = "customer";
            String customerAccountNr = bank.AddAccount(customerPin);

            CardParkingMachine machine = new CardParkingMachine(costPerHour, bank, machineAccountNr);
            machine.InsertMoney(100);
            machine.InsertMoney(200);

            // Act
            machine.Cancel();

            // Assert
            Assert.AreEqual(0, machine.CurrentTotal);
        }
        [TestMethod]
        public void Cancel_CurrentTotalReturned()
        {
            // Arrange
            int costPerHour = 30;
            Bank bank = new Bank();
            bank.AddAccount("test");
            String machinePin = "machine";
            String machineAccountNr = bank.AddAccount(machinePin);
            String customerPin = "customer";
            String customerAccountNr = bank.AddAccount(customerPin);

            CardParkingMachine machine = new CardParkingMachine(costPerHour, bank, machineAccountNr);
            machine.InsertMoney(100);
            machine.InsertMoney(200);

            // Act
            int refund = machine.Cancel();

            // Assert
            Assert.AreEqual(300, refund);
        }
        [TestMethod]
        public void IsSetAccountNrAndPin_True()
        {
            // Arrange
            int costPerHour = 30;
            Bank bank = new Bank();
            bank.AddAccount("test");
            String machinePin = "machine";
            String machineAccountNr = bank.AddAccount(machinePin);
            String customerPin = "customer";
            String customerAccountNr = bank.AddAccount(customerPin, 10000);

            CardParkingMachine machine = new CardParkingMachine(costPerHour, bank, machineAccountNr);

            // Act
            machine.SetAccountNrAndPin(customerAccountNr, customerPin);
            bool isSetAccountNrAndPin = machine.IsSetAccountNrAndPin();

            // Assert
            Assert.IsTrue(isSetAccountNrAndPin);

        }
        [TestMethod]
        public void IsSetAccountNrAndPin_False()
        {
            // Arrange
            int costPerHour = 30;
            Bank bank = new Bank();
            bank.AddAccount("test");
            String machinePin = "machine";
            String machineAccountNr = bank.AddAccount(machinePin);
            String customerPin = "customer";
            String customerAccountNr = bank.AddAccount(customerPin, 10000);

            CardParkingMachine machine = new CardParkingMachine(costPerHour, bank, machineAccountNr);

            // Act
            bool isSetAccountNrAndPin = machine.IsSetAccountNrAndPin();

            // Assert
            Assert.IsFalse(isSetAccountNrAndPin);

        }
        [TestMethod]
        public void IsSetAccountNrAndPin_False_AfterBuy()
        {
            // Arrange
            int costPerHour = 30;
            Bank bank = new Bank();
            bank.AddAccount("test");
            String machinePin = "machine";
            String machineAccountNr = bank.AddAccount(machinePin);
            String customerPin = "customer";
            String customerAccountNr = bank.AddAccount(customerPin, 10000);

            CardParkingMachine machine = new CardParkingMachine(costPerHour, bank, machineAccountNr);
            machine.InsertMoney(70);
            machine.SetAccountNrAndPin(customerAccountNr, customerPin);
            machine.BuyTicket();

            // Act
            bool isSetAccountNrAndPin = machine.IsSetAccountNrAndPin();

            // Assert
            Assert.IsFalse(isSetAccountNrAndPin);

        }

        [TestMethod]
        public void BuyTicket_3Days10Hours2Minutes_ValidTimeSpan()
        {
            // Arrange
            int costPerHour = 30;
            Bank bank = new Bank();
            bank.AddAccount("test");
            String machinePin = "machine";
            String machineAccountNr = bank.AddAccount(machinePin);
            String customerPin = "customer";
            String customerAccountNr = bank.AddAccount(customerPin, 30000);

            CardParkingMachine machine = new CardParkingMachine(costPerHour, bank, machineAccountNr);

            // Act
            machine.InsertMoney(1); // 2 min
            machine.InsertMoney(10 * 30); // 10 hours
            machine.InsertMoney(3 * 24 * 30); // 3 days
            machine.SetAccountNrAndPin(customerAccountNr, customerPin);
            Ticket ticket = machine.BuyTicket();

            TimeSpan timeSpan = new TimeSpan(days: 3, hours: 10, minutes: 2, seconds: 0);

            // Assert
            Assert.AreEqual(timeSpan, ticket.GetValidTimeSpan());
        }
        [TestMethod]
        public void BuyTicket_3Days10Hours2Minutes_ValidTo()
        {
            // Arrange
            int costPerHour = 30;
            Bank bank = new Bank();
            bank.AddAccount("test");
            String machinePin = "machine";
            String machineAccountNr = bank.AddAccount(machinePin);
            String customerPin = "customer";
            String customerAccountNr = bank.AddAccount(customerPin, 30000);

            CardParkingMachine machine = new CardParkingMachine(costPerHour, bank, machineAccountNr);

            // Act
            machine.InsertMoney(1); // 2 min
            machine.InsertMoney(10 * 30); // 10 hours
            machine.InsertMoney(3 * 24 * 30); // 3 days
            machine.SetAccountNrAndPin(customerAccountNr, customerPin);
            Ticket ticket = machine.BuyTicket();

            TimeSpan timeSpan = new TimeSpan(days: 3, hours: 10, minutes: 2, seconds: 0);
            DateTime date = DateTime.Now;
            date = date.Add(timeSpan);

            DateTime validTo = ticket.GetValidTo();

            // Assert
            Assert.AreEqual(validTo.ToString(), ticket.GetValidTo().ToString());
        }
        [TestMethod]
        public void BuyTicket_3Days10Hours2Minutes_Text()
        {
            // Arrange
            int costPerHour = 30;
            Bank bank = new Bank();
            bank.AddAccount("test");
            String machinePin = "machine";
            String machineAccountNr = bank.AddAccount(machinePin);
            String customerPin = "customer";
            String customerAccountNr = bank.AddAccount(customerPin, 30000);

            CardParkingMachine machine = new CardParkingMachine(costPerHour, bank, machineAccountNr);

            String ticketText = TimeToTicketText(3, 10, 2);

            // Act
            machine.InsertMoney(1); // 2 min
            machine.InsertMoney(10 * 30); // 10 hours
            machine.InsertMoney(3 * 24 * 30); // 3 days
            machine.SetAccountNrAndPin(customerAccountNr, customerPin);
            Ticket ticket = machine.BuyTicket();

            // Assert
            Assert.AreEqual(ticketText, ticket.ToString());
        }
        [TestMethod]
        public void BuyTicket_Multi_CurrentTotalZeroed()
        {
            // Arrange
            int costPerHour = 30;
            Bank bank = new Bank();
            bank.AddAccount("test");
            String machinePin = "machine";
            String machineAccountNr = bank.AddAccount(machinePin);
            String customerPin = "customer";
            String customerAccountNr = bank.AddAccount(customerPin, 10000);
            String customer2Pin = "cust";
            String customer2AccountNr = bank.AddAccount(customer2Pin, 10000);

            CardParkingMachine machine = new CardParkingMachine(costPerHour, bank, machineAccountNr);

            String ticketText = TimeToTicketText(3, 10, 2);

            // Act
            machine.InsertMoney(100);
            machine.SetAccountNrAndPin(customerAccountNr, customerPin);
            Ticket ticket = machine.BuyTicket();

            machine.InsertMoney(200);
            machine.SetAccountNrAndPin(customer2AccountNr, customer2Pin);
            ticket = machine.BuyTicket();

            // Assert
            Assert.AreEqual(0, machine.CurrentTotal);
        }
        [TestMethod]
        public void BuyTicket_MoneyTransferToMachineAccount()
        {
            // Arrange
            int costPerHour = 30;
            Bank bank = new Bank();
            bank.AddAccount("test");
            String machinePin = "machine";
            String machineAccountNr = bank.AddAccount(machinePin);
            String customerPin = "customer";
            String customerAccountNr = bank.AddAccount(customerPin, 10000);
            String customer2Pin = "cust";
            String customer2AccountNr = bank.AddAccount(customer2Pin, 10000);

            CardParkingMachine machine = new CardParkingMachine(costPerHour, bank, machineAccountNr);

            String ticketText = TimeToTicketText(3, 10, 2);

            // Act
            machine.InsertMoney(100);
            machine.SetAccountNrAndPin(customerAccountNr, customerPin);
            Ticket ticket = machine.BuyTicket();

            machine.InsertMoney(200);
            machine.SetAccountNrAndPin(customer2AccountNr, customer2Pin);
            ticket = machine.BuyTicket();

            // Assert
            Assert.AreEqual(300, bank.GetBalance(machineAccountNr, machinePin));
        }
        [TestMethod]
        public void BuyTicket_MoneyTransferFromCustomer()
        {
            // Arrange
            int costPerHour = 30;
            Bank bank = new Bank();
            bank.AddAccount("test");
            String machinePin = "machine";
            String machineAccountNr = bank.AddAccount(machinePin);
            String customerPin = "customer";
            String customerAccountNr = bank.AddAccount(customerPin, 10000);
            String customer2Pin = "cust";
            String customer2AccountNr = bank.AddAccount(customer2Pin, 10000);

            CardParkingMachine machine = new CardParkingMachine(costPerHour, bank, machineAccountNr);

            String ticketText = TimeToTicketText(3, 10, 2);

            // Act
            machine.InsertMoney(100);
            machine.SetAccountNrAndPin(customerAccountNr, customerPin);
            Ticket ticket = machine.BuyTicket();

            machine.InsertMoney(200);
            machine.SetAccountNrAndPin(customer2AccountNr, customer2Pin);
            ticket = machine.BuyTicket();

            // Assert
            Assert.AreEqual(10000 - 100, bank.GetBalance(customerAccountNr, customerPin));
        }
        [TestMethod]
        public void BuyTicket_InvalidPin()
        {
            // Arrange
            int costPerHour = 30;
            Bank bank = new Bank();
            bank.AddAccount("test");
            String machinePin = "machine";
            String machineAccountNr = bank.AddAccount(machinePin);
            String customerPin = "customer";
            String customerAccountNr = bank.AddAccount(customerPin, 10000);

            CardParkingMachine machine = new CardParkingMachine(costPerHour, bank, machineAccountNr);

            String ticketText = TimeToTicketText(3, 10, 2);

            // Act
            machine.InsertMoney(100);
            string invalidPin = "invalid";
            machine.SetAccountNrAndPin(customerAccountNr, invalidPin);
            Ticket ticket = machine.BuyTicket();

            // Assert
            Assert.IsNull(ticket);
        }
        [TestMethod]
        public void BuyTicket_InvalidAccountNr()
        {
            // Arrange
            int costPerHour = 30;
            Bank bank = new Bank();
            bank.AddAccount("test");
            String machinePin = "machine";
            String machineAccountNr = bank.AddAccount(machinePin);
            String customerPin = "customer";
            String customerAccountNr = bank.AddAccount(customerPin, 10000);

            CardParkingMachine machine = new CardParkingMachine(costPerHour, bank, machineAccountNr);

            String ticketText = TimeToTicketText(3, 10, 2);

            // Act
            machine.InsertMoney(100);
            string invalidAccountNr = "invalid";
            machine.SetAccountNrAndPin(invalidAccountNr, "pin");
            Ticket ticket = machine.BuyTicket();

            // Assert
            Assert.IsNull(ticket);
        }
        [TestMethod]
        public void BuyTicket_NoAccount()
        {
            // Arrange
            int costPerHour = 30;
            Bank bank = new Bank();
            bank.AddAccount("test");
            String machinePin = "machine";
            String machineAccountNr = bank.AddAccount(machinePin);
            String customerPin = "customer";
            String customerAccountNr = bank.AddAccount(customerPin, 10000);
            String customer2Pin = "cust";
            String customer2AccountNr = bank.AddAccount(customer2Pin, 10000);

            CardParkingMachine machine = new CardParkingMachine(costPerHour, bank, machineAccountNr);

            String ticketText = TimeToTicketText(3, 10, 2);

            // Act
            machine.InsertMoney(100);
            Ticket ticket = machine.BuyTicket();

            // Assert
            Assert.IsNull(ticket);
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
