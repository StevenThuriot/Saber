using System;
using Saber.Exceptions;
using Saber.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Saber.Test
{
    [TestClass]
    public class AssertHelperTest
    {
        [ExpectedException(typeof (SaberAssertException)), TestMethod]
        public void AssertHelper_WrongMessage()
        {
            ExAssert.Throws<ArgumentException>
                    (
                     () => (new TestClass()).Run(),
                     "This is aa test."
                    );
        }

        [ExpectedException(typeof (SaberAssertException)), TestMethod]
        public void AssertHelper_WrongExceptionType()
        {
            ExAssert.Throws<Exception>
                    (
                     () => (new TestClass()).Run(),
                     "This is a test."
                    );
        }

        [ExpectedException(typeof (SaberAssertException)), TestMethod]
        public void AssertHelper_NoExceptionThrown()
        {
            ExAssert.Throws<ArgumentException>
                    (
                     () => (new TestClass2()).Run(),
                     "This is a test."
                    );
        }

        [TestMethod]
        public void AssertHelper_Succeed()
        {
            ExAssert.Throws<ArgumentException>
                    (
                     () => (new TestClass()).Run(),
                     "This is a test."
                    );
        }

        #region Nested type: TestClass

        internal class TestClass
        {
            public void Run()
            {
                throw new ArgumentException("This is a test.");
            }
        }

        #endregion

        #region Nested type: TestClass2

        internal class TestClass2
        {
            public void Run()
            {
            }
        }

        #endregion
    }
}