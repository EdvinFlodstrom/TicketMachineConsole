using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Banking;

namespace UnitTestBanking
{
    [TestClass]
    public class BankAccountTest
    {
        [TestMethod]
        public void Property_AccountNumber_IsReadOnly()
        {
            // Arrange
            Transfer transfer = new Transfer("10", "12", 1000);

            // Assert
            Assert.IsFalse(typeof(BankAccount).GetProperty("AccountNumber").CanWrite);
        }
        [TestMethod]
        public void Property_Balance_IsReadOnly()
        {
            // Arrange
            Transfer transfer = new Transfer("10", "12", 1000);

            // Assert
            Assert.IsFalse(typeof(BankAccount).GetProperty("Balance").CanWrite);
        }
        [TestMethod]
        public void CreateAccount_AccountNumber_Set()
        {
            // Arrange
            BankAccount account;

            // Act
            account = new BankAccount("22", "12");

            // Assert
            Assert.AreEqual("22", account.AccountNumber);
        }
        [TestMethod]
        public void CreateAccount_AccountNumber_Set_2()
        {
            // Arrange
            BankAccount account;

            // Act
            account = new BankAccount("228", "12");

            // Assert
            Assert.AreEqual("228", account.AccountNumber);
        }

        [TestMethod]
        public void CreateAccount_Balance_Zero()
        {
            // Arrange
            BankAccount account;

            // Act
            account = new BankAccount("22", "12");

            // Assert
            Assert.AreEqual(0, account.Balance);
        }
        [TestMethod]
        public void ValidatePin()
        {
            // Arrange
            BankAccount account = new BankAccount("23", "2244");

            // Act
            bool pinIsValid = account.ValidatePin("2244");

            Assert.IsTrue(pinIsValid);
        }
        public void ValidatePin_2()
        {
            // Arrange
            BankAccount account = new BankAccount("23", "1144");

            // Act
            bool pinIsValid = account.ValidatePin("1144");

            // Assert
            Assert.IsTrue(pinIsValid);
        }
        [TestMethod]
        public void CreateAccount_AccountNumber_Set_3()
        {
            // Arrange
            BankAccount account;

            // Act
            account = new BankAccount("228", "12", 2000);

            // Assert
            Assert.AreEqual("228", account.AccountNumber);
        }
        [TestMethod]
        public void CreateAccount_Balance_Set()
        {
            // Arrange
            BankAccount account;

            // Act
            account = new BankAccount("22", "12", 3000);

            // Assert
            Assert.AreEqual(3000, account.Balance);
        }
        [TestMethod]
        public void ValidatePin_3()
        {
            // Arrange
            BankAccount account = new BankAccount("123", "24", 400);

            // Act
            bool pinIsValid = account.ValidatePin("24");

            Assert.IsTrue(pinIsValid);
        }
        [TestMethod]
        public void Transfer_Insert()
        {
            // Arrange
            BankAccount accountFrom = new BankAccount("2", "12", 5000);
            BankAccount accountTo = new BankAccount("3", "34", 6000);
            Transfer transfer = new Transfer("2", "3", 2000);
            // Act
            accountFrom.Transfer(transfer);
            accountTo.Transfer(transfer);
            // Assert
            Assert.AreEqual(6000 + 2000, accountTo.Balance);
        }
        [TestMethod]
        public void Transfer_Withdraw()
        {
            // Arrange
            BankAccount accountFrom = new BankAccount("2", "12", 5000);
            BankAccount accountTo = new BankAccount("3", "34", 6000);
            Transfer transfer = new Transfer("2", "3", 2000);
            // Act
            accountFrom.Transfer(transfer);
            accountTo.Transfer(transfer);
            // Assert
            Assert.AreEqual(5000 - 2000, accountFrom.Balance);
        }
        [TestMethod]
        public void GetTransfers_NoTransferMade()
        {
            // Arrange
            BankAccount account = new BankAccount("12", "34");

            // Act
            int numberOfTransfers = account.GetTransfers().Count;

            // Assert
            Assert.AreEqual(0, numberOfTransfers);
        }
        [TestMethod]
        public void GetTransfers_NoTransferMade_2()
        {
            // Arrange
            BankAccount account = new BankAccount("12", "34", 3000);

            // Act
            int numberOfTransfers = account.GetTransfers().Count;

            // Assert
            Assert.AreEqual(0, numberOfTransfers);
        }

        [TestMethod]
        public void GetTransfers_NumberOfTransfers()
        {
            // Arrange
            BankAccount accountFrom = new BankAccount("2", "12", 5000);
            BankAccount accountTo = new BankAccount("3", "34", 6000);

            Transfer transfer1 = new Transfer("2", "3", 2000);
            Transfer transfer2 = new Transfer("2", "3", 500);
            Transfer transfer3 = new Transfer("3", "2", 300);

            accountFrom.Transfer(transfer1);
            accountTo.Transfer(transfer1);

            accountFrom.Transfer(transfer2);
            accountTo.Transfer(transfer2);

            accountFrom.Transfer(transfer3);
            accountTo.Transfer(transfer3);

            // Act
            List<Transfer> transfers = accountTo.GetTransfers();

            // Assert
            Assert.AreEqual(3, transfers.Count);
        }
        [TestMethod]
        public void GetTransfers_TransferLogged_1()
        {
            // Arrange
            BankAccount accountFrom = new BankAccount("2", "12", 5000);
            BankAccount accountTo = new BankAccount("3", "34", 6000);
            Transfer transfer1 = new Transfer("2", "3", 2000);
            Transfer transfer2 = new Transfer("2", "3", 500);
            Transfer transfer3 = new Transfer("3", "2", 300);
            accountFrom.Transfer(transfer1);
            accountTo.Transfer(transfer1);
            accountFrom.Transfer(transfer2);
            accountTo.Transfer(transfer2);
            accountFrom.Transfer(transfer3);
            accountTo.Transfer(transfer3);

            // Act
            List<Transfer> transfers = accountTo.GetTransfers();

            // Assert
            Assert.AreEqual(transfer1, transfers[0]);
        }
        [TestMethod]
        public void GetTransfers_TransferLogged_2()
        {
            // Arrange
            BankAccount accountFrom = new BankAccount("2", "12", 5000);
            BankAccount accountTo = new BankAccount("3", "34", 6000);
            Transfer transfer1 = new Transfer("2", "3", 2000);
            Transfer transfer2 = new Transfer("2", "3", 500);
            Transfer transfer3 = new Transfer("3", "2", 300);
            accountFrom.Transfer(transfer1);
            accountTo.Transfer(transfer1);
            accountFrom.Transfer(transfer2);
            accountTo.Transfer(transfer2);
            accountFrom.Transfer(transfer3);
            accountTo.Transfer(transfer3);

            // Act
            List<Transfer> transfers = accountTo.GetTransfers();

            // Assert
            Assert.AreEqual(transfer2, transfers[1]);
        }
        [TestMethod]
        public void GetTransfers_TransferLogged_3()
        {
            // Arrange
            BankAccount accountFrom = new BankAccount("2", "12", 5000);
            BankAccount accountTo = new BankAccount("3", "34", 6000);
            Transfer transfer1 = new Transfer("2", "3", 2000);
            Transfer transfer2 = new Transfer("2", "3", 500);
            Transfer transfer3 = new Transfer("3", "2", 300);
            accountFrom.Transfer(transfer1);
            accountTo.Transfer(transfer1);
            accountFrom.Transfer(transfer2);
            accountTo.Transfer(transfer2);
            accountFrom.Transfer(transfer3);
            accountTo.Transfer(transfer3);

            // Act
            List<Transfer> transfers = accountTo.GetTransfers();

            // Assert
            Assert.AreEqual(transfer3, transfers[2]);
        }
        [TestMethod]
        // Failing transfer is not logged.
        public void GetTransfers_Failed_NotLogged()
        {
            // Arrange
            BankAccount account_2 = new BankAccount("2", "12", 5000);
            BankAccount account_3 = new BankAccount("3", "34", 6000);

            Transfer transfer1 = new Transfer("2", "3", 2000);
            // invalid transfer
            Transfer transfer2 = new Transfer("2", "3", 50000);
            Transfer transfer3 = new Transfer("3", "2", 300);

            // Act
            // withdraw
            bool success = account_2.Transfer(transfer1);
            if (success)
            {
                // insert
                account_3.Transfer(transfer1);
            }

            // invalid transfer
            success = account_2.Transfer(transfer2);
            if (success)
            {
                account_3.Transfer(transfer2);
            }


            success = account_3.Transfer(transfer3);
            if (success)
            {
                account_2.Transfer(transfer3);
            }


            List<Transfer> transfers = account_3.GetTransfers();

            // Assert
            Assert.AreEqual(transfer3, transfers[1]);
        }
        [TestMethod]
        // Failing transfer is not logged.
        public void GetTransfers_Failed_NotLogged_Reverse()
        {
            // Arrange
            BankAccount account_2 = new BankAccount("2", "12", 5000);
            BankAccount account_3 = new BankAccount("3", "34", 6000);

            Transfer transfer1 = new Transfer("2", "3", 2000);
            // invalid transfer
            Transfer transfer2 = new Transfer("2", "3", 50000);
            Transfer transfer3 = new Transfer("3", "2", 300);

            // Act
            // withdraw
            bool success = account_2.Transfer(transfer1);
            if (success)
            {
                // insert
                account_3.Transfer(transfer1);
            }

            // invalid transfer
            success = account_2.Transfer(transfer2);
            if (success)
            {
                account_3.Transfer(transfer2);
            }


            success = account_3.Transfer(transfer3);
            if (success)
            {
                account_2.Transfer(transfer3);
            }


            List<Transfer> transfers = account_2.GetTransfers();

            // Assert
            Assert.AreEqual(transfer3, transfers[1]);
        }
    }
}
