using System;
using BLL.Services;
using Core.Models;

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

            Console.ReadLine();
        }
    }
}
