using System;
using BLL.Services;
using Core.Models;
using Core.Exceptions;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            var testRunner = new TestRunner();
            var testResults = testRunner.RunTests();

            foreach (TestInfo testResult in testResults)
            {
                Console.WriteLine($"{testResult.IsSuccessful} - {testResult.TestName}: {testResult.Message}");
            }

            //Type type = typeof(ClassForTesting);

            //var method = type.GetMethods()[1];

            //var obj = Activator.CreateInstance(type);

            //try
            //{
            //    method.Invoke(obj, null);
            //}
            //catch (AssertException exception)
            //{
            //    Console.WriteLine(exception.Message);
            //}

            Console.ReadLine();
        }
    }
}
