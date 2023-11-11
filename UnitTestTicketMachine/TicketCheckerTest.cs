using Banking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parking;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestTicketMachine
{
    [TestClass]
    public class TicketCheckerTest
    {
        [TestMethod]
        public void TicketIsValid()
        {
            //Arrange
            TicketChecker checker = new(0);
            ParkingMachine machine = new(20);

            //Act
            machine.InsertMoney(40);
            Ticket ticket = machine.BuyTicket();
                        
            //Assert
            Assert.AreEqual(true, checker.IsValid(ticket));
        }
        [TestMethod]
        public void TicketIsNotValid()
        {
            //Arrange
            TicketChecker checker = new(0);
            ParkingMachine machine = new(20);

            //Act
            machine.InsertMoney(0);
            Ticket ticket = machine.BuyTicket();

            //Assert
            Assert.AreEqual(false, checker.IsValid(ticket));
        }
        [TestMethod]
        public void Fine0Kr()
        {
            //Arrange
            TicketChecker checker = new(100);
            ParkingMachine machine = new(20);

            //Act
            machine.InsertMoney(60);
            Ticket ticket = machine.BuyTicket();            

            //Assert
            Assert.AreEqual(0, checker.FineCalculator(ticket, 0));
        }
        [TestMethod]
        public void Fine1000Kr()
        {
            //Arrange
            TicketChecker checker = new(100);
            ParkingMachine machine = new(20);

            //Act
            machine.InsertMoney(60);
            Ticket ticket = machine.BuyTicket();

            //Assert
            Assert.AreEqual(1000, checker.FineCalculator(ticket, 8));
        }
    }
}
