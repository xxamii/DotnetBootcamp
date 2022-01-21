using System;
using System.Collections.Generic;
using System.Text;
using Core.Attributes;
using BLL.Helpers;

namespace Testing
{
    [TestClass]
    public class ClassForTesting
    {
        [Fact]
        public void SomeTest_a_equals_b()
        {
            int a = 1;
            int b = 1;

            Assert.IsEqual(a, b);
        }

        [Fact]
        public void SomeTest_a_does_not_equal_b()
        {
            int a = 1;
            int b = 2;

            Assert.IsEqual(a, b);
        }
    }
}
