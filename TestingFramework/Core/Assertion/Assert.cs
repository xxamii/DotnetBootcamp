using System;
using System.Collections.Generic;
using System.Text;
using Core.Exceptions;

namespace Core.Assertion
{
    public static class Assert
    {
        public static void IsTrue(bool statement)
        {
            if (!statement)
            {
                throw new AssertException($"statement is false");
            }
        }

        public static void IsEqual(object expected, object actual)
        {
            if (!expected.Equals(actual))
            {
                throw new AssertException($"expected: {expected}, actual: {actual}");
            }
        }
    }
}
