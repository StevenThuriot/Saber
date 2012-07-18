using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Saber.Domain;
using Saber.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Saber.Helpers;

namespace Saber.Test
{
    public class HierSeProperties
    {
        public int NeNummer { get; set; }
    }

    [TestClass]
    public class GuardTest
    {
        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void NotNull_TestPassingNull()
        {
            Guard.NotNull("test", null);
        }

        [TestMethod]
        public void NotNull_TestPassingSomethingOtherThanNull()
        {
        	var period = new Period(DateTime.Now, DateTime.Now.AddDays(1));
            Guard.NotNull(new StringBuilder(), "Test", new object(), 1, period, DateTime.Now);
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void NotNullOrWhiteSpace_TestPassingNull()
        {
            Guard.NotNullOrWhiteSpace(null);
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void NotNullOrWhiteSpace_TestPassingNullWithAnotherParameter()
        {
            Guard.NotNullOrWhiteSpace(null, "Test");
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void NotNullOrWhiteSpace_TestPassingEmpty()
        {
            Guard.NotNullOrWhiteSpace(String.Empty);
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void NotNullOrWhiteSpace_TestPassingWhiteSpace()
        {
            Guard.NotNullOrWhiteSpace("   ");
        }

        [TestMethod]
        public void NotNullOrWhiteSpace_TestPassingNormalString()
        {
            Guard.NotNullOrWhiteSpace("Test");
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void ParsableToGuid_TestPassingNonGuid()
        {
            Guard.CanParseToGuid("Test");
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void ParsableToGuid_TestPassingNull()
        {
            Guard.CanParseToGuid(null);
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void ParsableToGuid_TestPassingNull2()
        {
            Guard.CanParseToGuid(null, Guid.NewGuid().ToString());
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void ParsableToGuid_TestPassingEmpty()
        {
            Guard.CanParseToGuid(String.Empty);
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void ParsableToGuid_TestPassingWhiteSpace()
        {
            Guard.CanParseToGuid("  ");
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void ParsableToGuid_TestPassingGuidEmpty()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.Empty;

            IEnumerable<Guid> guids = Guard.CanParseToGuid(guid1.ToString(),
                                                                           guid2.ToString(),
                                                                           guid3.ToString());
        }

        [TestMethod]
        public void ParsableToGuid_TestPassingGuid()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();

            IEnumerable<Guid> guids = Guard.CanParseToGuid(guid1.ToString(),
                                                                           guid2.ToString(),
                                                                           guid3.ToString());

            Assert.AreEqual(guid1, guids.ElementAt(0));
            Assert.AreEqual(guid2, guids.ElementAt(1));
            Assert.AreEqual(guid3, guids.ElementAt(2));
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void InTheFuture_TestWithPassedDate()
        {
            Guard.InTheFuture(DateTime.Now.AddDays(-1));
        }

        [TestMethod]
        public void InTheFuture_TestWithFutureDate()
        {
            Guard.InTheFuture(DateTime.Now.AddDays(1));
        }

        [TestMethod]
        public void InThePast_TestWithPassedDate()
        {
            Guard.InThePast(DateTime.Now.AddDays(-1));
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void InThePast_TestWithFutureDate()
        {
            Guard.InThePast(DateTime.Now.AddDays(1));
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void StartBeforeEnd_TestUsingEqualDates()
        {
            DateTime date = DateTime.Now;
            Guard.StartBeforeEnd(date, date);
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void StartBeforeEnd_TestUsingEndBeforeStart()
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddDays(1);
            Guard.StartBeforeEnd(end, start);
        }

        [TestMethod]
        public void StartBeforeEnd_Test()
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddDays(1);
            Guard.StartBeforeEnd(start, end);
        }

        [TestMethod]
        public void CanBeAssigned_Test()
        {
            Guard.CanBeAssigned(typeof (String), typeof (object));
        }

        [TestMethod]
        public void CanBeAssigned_TestUsingInterface()
        {
            Guard.CanBeAssigned(typeof (TestClass), typeof (ITestInterface));
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void CanBeAssigned_TestUsingInterface_Fails()
        {
            Guard.CanBeAssigned(typeof (String), typeof (ITestInterface));
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void CanBeAssigned_TestUsingInterface_FailsCantImplement()
        {
            Guard.CanBeAssigned(typeof (GuardTest), typeof (ITestInterface));
        }

        [ExpectedException(typeof (SaberGuardException)), TestMethod]
        public void CallMethod()
        {
            TestMethod("   ");
        }

        private void TestMethod(string inputString)
        {
            Guard.NotNullOrWhiteSpace(inputString, "lalala");
        }

        #region Nested type: testClass

        public class TestClass : ITestInterface
        {
        }

        #endregion

        #region Nested type: testInterface

        public interface ITestInterface
        {
        }

        #endregion
    }
}