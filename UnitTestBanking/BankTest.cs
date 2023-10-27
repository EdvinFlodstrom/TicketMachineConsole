using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Banking;

namespace UnitTestBanking
{
    [TestClass]
    public class BankTest
    {
        /// <summary>
        /// Testing AddAccount with one parameter.
        /// </summary>
        [TestMethod]
        public void AddAccount()
        {
            // Arrange
            Bank bank = new Bank();

            // Act
            String accountNr = bank.AddAccount("123");

            // Assert
            Assert.AreEqual(0, bank.GetBalance(accountNr, "123"));
        }
        [TestMethod]
        public void AddAccount_Fail_PinIsEmpty()
        {
            // Arrange
            Bank bank = new Bank();

            // Act
            String accountNr = bank.AddAccount("");

            // Assert
            Assert.IsNull(accountNr);
        }
        [TestMethod]
        public void AddAccount_Fail_PinIsNull()
        {
            // Arrange
            Bank bank = new Bank();

            // Act
            String accountNr = bank.AddAccount(null);

            // Assert
            Assert.IsNull(accountNr);
        }
        /// <summary>
        /// Testing AddAccount with two parameters.
        /// </summary>
        [TestMethod]
        public void AddAccount_2()
        {
            // Arrange
            Bank bank = new Bank();

            // Act
            String accountNr = bank.AddAccount("123", 3000);

            // Assert
            Assert.AreEqual(3000, bank.GetBalance(accountNr, "123"));
        }
        /// <summary>
        /// Balance is 0 if set to negative value.
        /// </summary>
        [TestMethod]
        public void AddAccount_2_NegativBalance()
        {
            // Arrange
            Bank bank = new Bank();

            // Act
            String accountNr = bank.AddAccount("123", -3000);

            // Assert
            Assert.AreEqual(0, bank.GetBalance(accountNr, "123"));
        }
        [TestMethod]
        public void AddAccount_Fail_PinIsEmpty_2()
        {
            // Arrange
            Bank bank = new Bank();

            // Act
            String accountNr = bank.AddAccount("", 400);

            // Assert
            Assert.IsNull(accountNr);
        }
        [TestMethod]
        public void AddAccount_Fail_PinIsNull_2()
        {
            // Arrange
            Bank bank = new Bank();

            // Act
            String accountNr = bank.AddAccount(null, 300);

            // Assert
            Assert.IsNull(accountNr);
        }

        [TestMethod]
        public void Transfer_FromAccountBalance()
        {
            // Arrange
            Bank bank = new Bank();
            bank.AddAccount("123");
            bank.AddAccount("123");
            String accountNrFrom = bank.AddAccount("11", 5000);
            String accountNrTo = bank.AddAccount("22", 4000);

            // Act
            Transfer transfer = new Transfer(accountNrFrom, accountNrTo, 600);
            bank.Transfer(transfer, "11");

            // Assert
            Assert.AreEqual(5000 - 600, bank.GetBalance(accountNrFrom, "11"));
        }
        [TestMethod]
        public void GetBalance_InvalidAccountNr()
        {
            // Arrange
            Bank bank = new Bank();

            // Act
            int balance = bank.GetBalance("123", "22");

            Assert.AreEqual(0, balance);
        }
        [TestMethod]
        public void GetBalance_InvalidPin()
        {
            // Arrange
            Bank bank = new Bank();
            bank.AddAccount("123");
            bank.AddAccount("123");
            String accountNrFrom = bank.AddAccount("11", 5000);
            String accountNrTo = bank.AddAccount("22", 4000);

            Transfer transfer = new Transfer(accountNrFrom, accountNrTo, 600);
            bank.Transfer(transfer, "11");

            // Act
            int balance = bank.GetBalance(accountNrTo, "11");

            Assert.AreEqual(0, balance);
        }
        [TestMethod]
        public void Transfer_ToAccountBalance()
        {
            // Arrange
            Bank bank = new Bank();
            bank.AddAccount("123");
            bank.AddAccount("123");
            String accountNrFrom = bank.AddAccount("11", 5000);
            String accountNrTo = bank.AddAccount("22", 4000);

            // Act
            Transfer transfer = new Transfer(accountNrFrom, accountNrTo, 600);
            bank.Transfer(transfer, "11");

            // Assert
            Assert.AreEqual(4000 + 600, bank.GetBalance(accountNrTo, "22"));
        }

        [TestMethod]
        public void MultiTransfer_FromAccountBalance()
        {
            // Arrange
            Bank bank = new Bank();
            bank.AddAccount("123");
            bank.AddAccount("123");
            String accountNrFrom = bank.AddAccount("11", 5000);
            String accountNrTo = bank.AddAccount("22", 8000);

            // Act
            Transfer transfer = new Transfer(accountNrFrom, accountNrTo, 1000);
            bank.Transfer(transfer, "11");

            transfer = new Transfer(accountNrFrom, accountNrTo, 500);
            bank.Transfer(transfer, "11");

            // other direction
            transfer = new Transfer(accountNrTo, accountNrFrom, 2000);
            bank.Transfer(transfer, "22");

            // Assert
            Assert.AreEqual(5000 - 1000 - 500 + 2000, bank.GetBalance(accountNrFrom, "11"));
        }
        [TestMethod]
        public void MultiTransfer_ToAccountBalance()
        {
            // Arrange
            Bank bank = new Bank();
            bank.AddAccount("123");
            bank.AddAccount("123");
            String accountNrFrom = bank.AddAccount("11", 5000);
            String accountNrTo = bank.AddAccount("22", 8000);

            // Act
            Transfer transfer = new Transfer(accountNrFrom, accountNrTo, 1000);
            bank.Transfer(transfer, "11");

            transfer = new Transfer(accountNrFrom, accountNrTo, 500);
            bank.Transfer(transfer, "11");

            // other direction
            transfer = new Transfer(accountNrTo, accountNrFrom, 2000);
            bank.Transfer(transfer, "22");

            // Assert
            Assert.AreEqual(8000 + 1000 + 500 - 2000, bank.GetBalance(accountNrTo, "22"));
        }
        [TestMethod]
        public void MultiTransfer_GetTransfers_CountFrom()
        {
            // Arrange
            Bank bank = new Bank();
            bank.AddAccount("123");
            bank.AddAccount("123");
            String accountNrFrom = bank.AddAccount("11", 5000);
            String accountNrTo = bank.AddAccount("22", 8000);


            Transfer transfer = new Transfer(accountNrFrom, accountNrTo, 1000);
            bank.Transfer(transfer, "11");

            transfer = new Transfer(accountNrFrom, accountNrTo, 500);
            bank.Transfer(transfer, "11");

            // other direction
            transfer = new Transfer(accountNrTo, accountNrFrom, 2000);
            bank.Transfer(transfer, "22");

            // Act
            List<Transfer> transfers = bank.GetTransfers(accountNrFrom, "11");

            // Assert
            Assert.AreEqual(3, transfers.Count);
        }
        [TestMethod]
        public void MultiTransfer_GetTransfers_CountTo()
        {
            // Arrange
            Bank bank = new Bank();
            bank.AddAccount("123");
            bank.AddAccount("123");
            String accountNrFrom = bank.AddAccount("11", 5000);
            String accountNrTo = bank.AddAccount("22", 8000);

            Transfer transfer = new Transfer(accountNrFrom, accountNrTo, 1000);
            bank.Transfer(transfer, "11");

            transfer = new Transfer(accountNrFrom, accountNrTo, 500);
            bank.Transfer(transfer, "11");

            // other direction
            transfer = new Transfer(accountNrTo, accountNrFrom, 2000);
            bank.Transfer(transfer, "22");

            // Act
            List<Transfer> transfers = bank.GetTransfers(accountNrTo, "22");

            // Assert
            Assert.AreEqual(3, transfers.Count);
        }
        [TestMethod]
        public void MultiTransfer_GetTransfers_LastFrom()
        {
            // Arrange
            Bank bank = new Bank();
            bank.AddAccount("123");
            bank.AddAccount("123");
            String accountNrFrom = bank.AddAccount("11", 5000);
            String accountNrTo = bank.AddAccount("22", 8000);


            Transfer transfer = new Transfer(accountNrFrom, accountNrTo, 1000);
            bank.Transfer(transfer, "11");

            transfer = new Transfer(accountNrFrom, accountNrTo, 500);
            bank.Transfer(transfer, "11");

            // other direction
            transfer = new Transfer(accountNrTo, accountNrFrom, 2000);
            bank.Transfer(transfer, "22");

            // Act
            List<Transfer> transfers = bank.GetTransfers(accountNrFrom, "11");

            // Assert
            Assert.AreEqual(transfer, transfers[2]);
        }
        [TestMethod]
        public void MultiTransfer_GetTransfers_SecondTo()
        {
            // Arrange
            Bank bank = new Bank();
            bank.AddAccount("123");
            bank.AddAccount("123");
            String accountNrFrom = bank.AddAccount("11", 5000);
            String accountNrTo = bank.AddAccount("22", 8000);


            Transfer transfer = new Transfer(accountNrFrom, accountNrTo, 1000);
            bank.Transfer(transfer, "11");

            Transfer transfer2 = new Transfer(accountNrFrom, accountNrTo, 500);
            bank.Transfer(transfer2, "11");

            // other direction
            transfer = new Transfer(accountNrTo, accountNrFrom, 2000);
            bank.Transfer(transfer, "22");

            // Act
            List<Transfer> transfers = bank.GetTransfers(accountNrFrom, "11");

            // Assert
            Assert.AreEqual(transfer2, transfers[1]);
        }
        [TestMethod]
        public void GetTransfers_Fail_InvalidAccountNr()
        {
            // Arrange
            Bank bank = new Bank();
            bank.AddAccount("123");
            bank.AddAccount("123");
            String accountNrFrom = bank.AddAccount("11", 5000);
            String accountNrTo = bank.AddAccount("22", 8000);


            Transfer transfer = new Transfer(accountNrFrom, accountNrTo, 1000);
            bank.Transfer(transfer, "11");

            Transfer transfer2 = new Transfer(accountNrFrom, accountNrTo, 500);
            bank.Transfer(transfer2, "11");

            // other direction
            transfer = new Transfer(accountNrTo, accountNrFrom, 2000);
            bank.Transfer(transfer, "22");

            // Act
            String invalidAccountNr = "777";
            List<Transfer> transfers = bank.GetTransfers(invalidAccountNr, "11");

            // Assert
            Assert.IsNull(transfers);
        }
        [TestMethod]
        public void GetTransfers_Fail_InvalidPin()
        {
            // Arrange
            Bank bank = new Bank();
            bank.AddAccount("123");
            bank.AddAccount("123");
            String accountNrFrom = bank.AddAccount("11", 5000);
            String accountNrTo = bank.AddAccount("22", 8000);


            Transfer transfer = new Transfer(accountNrFrom, accountNrTo, 1000);
            bank.Transfer(transfer, "11");

            Transfer transfer2 = new Transfer(accountNrFrom, accountNrTo, 500);
            bank.Transfer(transfer2, "11");

            // other direction
            transfer = new Transfer(accountNrTo, accountNrFrom, 2000);
            bank.Transfer(transfer, "22");

            // Act
            String invalidPin = "777";
            List<Transfer> transfers = bank.GetTransfers(accountNrFrom, invalidPin);

            // Assert
            Assert.IsNull(transfers);
        }
    }
}
