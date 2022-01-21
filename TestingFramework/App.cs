using System;
using System.Collections.Generic;
using System.Text;
using BLL.Abstractions.Interfaces;
using Core.Models;

namespace PL
{
    public class App
    {
        private readonly ITestRunner _testRunner;

        public App(ITestRunner testRunner)
        {
            _testRunner = testRunner;
        }

        public void StartApp()
        {
            var testResults = _testRunner.RunTests();

            foreach (TestInfo testResult in testResults)
            {
                Console.WriteLine($"{(testResult.IsSuccessful ? "+" : "-")} - {testResult.TestName}: {testResult.Message}");
            }

            Console.ReadLine();
        }
    }
}
