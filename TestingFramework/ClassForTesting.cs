﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Attributes;
using Core.Helpers;
using Core.Exceptions;

namespace PL
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

        [Fact]
        public void SomeTest_a_true_b()
        {
            int a = 1;
            int b = 1;

            Assert.IsTrue(a == b);
        }

        [Fact]
        public void SomeTest_a_false_b()
        {
            int a = 1;
            int b = 2;

            Assert.IsTrue(a == b);
        }

        [RunBeforeEachTest]
        public void IRunBefore()
        {
            Console.WriteLine("I run before");
        }

        [RunBeforeEachTest]
        public void BeforeIRun()
        {
            Console.WriteLine("Before I run");
        }

        [RunAfterAllTests]
        public void IRunAfter()
        {
            Console.WriteLine("I run afte");
        }

        [RunAfterAllTests]
        public void AfterIRun()
        {
            Console.WriteLine("After I run");
        }
    }
}