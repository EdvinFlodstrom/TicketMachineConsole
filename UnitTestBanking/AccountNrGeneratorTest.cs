using Banking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestBanking
{
    [TestClass]
    public class AccountNrGeneratorTest
    {
        [TestMethod]
        public void GetUniqueAccountNr()
        {
            // Arrange
            AccountNrGenerator generator = new AccountNrGenerator();

            // Act
            string accountNr = generator.GetUniqieAccountNr();

            // Assert
            Assert.AreEqual("1001", accountNr);
        }
        [TestMethod]
        public void GetUniqueAccountNr2()
        {
            // Arrange
            AccountNrGenerator generator = new AccountNrGenerator();

            // Act
            generator.GetUniqieAccountNr();
            string accountNr = generator.GetUniqieAccountNr();

            // Assert
            Assert.AreEqual("1002", accountNr);
        }
        [TestMethod]
        public void GetUniqueAccountNr3()
        {
            // Arrange
            AccountNrGenerator generator = new AccountNrGenerator();

            // Act
            generator.GetUniqieAccountNr();
            generator.GetUniqieAccountNr();
            string accountNr = generator.GetUniqieAccountNr();

            // Assert
            Assert.AreEqual("1003", accountNr);
        }
        [TestMethod]
        public void GetUniqueAccountNr4()
        {
            // Arrange
            AccountNrGenerator generator = new AccountNrGenerator();

            // Act
            generator.GetUniqieAccountNr();
            generator.GetUniqieAccountNr();
            generator.GetUniqieAccountNr();
            string accountNr = generator.GetUniqieAccountNr();

            // Assert
            Assert.AreEqual("1004", accountNr);
        }
    }
}
