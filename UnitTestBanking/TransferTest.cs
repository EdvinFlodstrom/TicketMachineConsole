using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Banking;

namespace UnitTestBanking
{
    [TestClass]
    public class TransferTest
    {
        [TestMethod]
        public void Property_FromAccountNr_IsReadOnly()
        {
            // Arrange
            Transfer transfer = new Transfer("10", "12", 1000);

            // Assert
            Assert.IsFalse(typeof(Transfer).GetProperty("FromAccountNr").CanWrite);
        }
        [TestMethod]
        public void Property_ToAccountNr_IsReadOnly()
        {
            // Arrange
            Transfer transfer = new Transfer("110", "112", 10000);

            // Assert
            Assert.IsFalse(typeof(Transfer).GetProperty("ToAccountNr").CanWrite);
        }
        [TestMethod]
        public void Property_Amount_IsReadOnly()
        {
            // Arrange
            Transfer transfer = new Transfer("110", "112", 10000);

            // Assert
            Assert.IsFalse(typeof(Transfer).GetProperty("Amount").CanWrite);
        }
        [TestMethod]
        public void CreateTransfer_FromAccountNr_IsSet()
        {
            // Arrange
            Transfer transfer;

            // Act
            transfer = new Transfer("23", "33", 2000);

            // Assert
            Assert.AreEqual("23", transfer.FromAccountNr);
        }
        [TestMethod]
        public void CreateTransfer_FromAccountNr_IsSet_2()
        {
            // Arrange
            Transfer transfer;

            // Act
            transfer = new Transfer("67", "33", 2000);

            // Assert
            Assert.AreEqual("67", transfer.FromAccountNr);
        }
        [TestMethod]
        public void CreateTransfer_ToAccountNr_IsSet()
        {
            // Arrange
            Transfer transfer;

            // Act
            transfer = new Transfer("23", "33", 2000);

            // Assert
            Assert.AreEqual("33", transfer.ToAccountNr);
        }
        [TestMethod]
        public void CreateTransfer_ToAccountNr_IsSet_2()
        {
            // Arrange
            Transfer transfer;

            // Act
            transfer = new Transfer("233", "133", 2000);

            // Assert
            Assert.AreEqual("133", transfer.ToAccountNr);
        }
        [TestMethod]
        public void CreateTransfer_Amount_IsSet()
        {
            // Arrange
            Transfer transfer;

            // Act
            transfer = new Transfer("23", "33", 2000);

            // Assert
            Assert.AreEqual(2000, transfer.Amount);
        }
        [TestMethod]
        public void CreateTransfer_Amount_IsSet_2()
        {
            // Arrange
            Transfer transfer;

            // Act
            transfer = new Transfer("23", "33", 500);

            // Assert
            Assert.AreEqual(500, transfer.Amount);
        }
    }
}
