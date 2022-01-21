using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BLL.Abstractions.Interfaces;
using Core.Attributes;
using Core.Exceptions;
using Core.Models;
using DAL;

namespace BLL.Services
{
    public class TestRunner : ITestRunner
    {
        private readonly AssemblyWorker _assemblyWorker;

        public TestRunner(AssemblyWorker assemblyWorker)
        {
            _assemblyWorker = assemblyWorker;
        }

        public List<TestInfo> RunTests()
        {
            List<TestInfo> testResults = new List<TestInfo>();
            List<Type> testClasses = GetAllTestClasses();

            foreach (Type type in testClasses)
            {
                List<TestInfo> currentTestResults = RunTestMethodsOfClass(type);
                testResults.AddRange(currentTestResults);
            }

            return testResults;
        }

        private List<Type> GetAllTestClasses()
        {
            List<Type> result = new List<Type>();

            var assembly = _assemblyWorker.GetTestingAssembly();

            Type[] types = assembly.GetTypes();

            result.AddRange(types.Where(type => type.IsClass && type.IsPublic && type.CustomAttributes.Any(x => x.AttributeType == typeof(TestClass))));

            return result;
        }

        private List<TestInfo> RunTestMethodsOfClass(Type testClass)
        {
            List<TestInfo> testResults = new List<TestInfo>();

            var testClassInstance = Activator.CreateInstance(testClass);

            var methods = testClass.GetMethods();

            var beforeMethods = methods.Where(method => method.CustomAttributes.Any(x => x.AttributeType == typeof(RunBeforeEachTest)));
            var afterMethods = methods.Where(method => method.CustomAttributes.Any(x => x.AttributeType == typeof(RunAfterAllTests)));

            foreach (MethodInfo method in methods)
            {
                if (method.CustomAttributes.Any(x => x.AttributeType == typeof(Fact)))
                {
                    foreach (var beforeMethod in beforeMethods)
                    {
                        RunMethodOfClass(testClassInstance, beforeMethod);
                    }

                    TestInfo testResult = RunTestMethodOfClass(testClassInstance, method);
                    testResults.Add(testResult);
                }
            }

            foreach (var afterMethod in afterMethods)
            {
                RunMethodOfClass(testClassInstance, afterMethod);
            }

            return testResults;
        }

        private TestInfo RunTestMethodOfClass(object testClassInstance, MethodInfo method)
        {
            TestInfo testResult = new TestInfo();
            testResult.TestName = method.Name;
            try
            {
                RunMethodOfClass(testClassInstance, method);
            }
            catch (AssertException exception)
            {
                testResult.IsSuccessful = false;
                testResult.Message = exception.Message;
            }
            catch (Exception exception)
            {
                testResult.IsSuccessful = false;
                testResult.Message = exception.Message;
            }

            return testResult;
        }

        private void RunMethodOfClass(object testClassInstance, MethodInfo method)
        {
            if (testClassInstance != null && method != null)
            {
                Action action = (Action)Delegate.CreateDelegate(typeof(Action), testClassInstance, method);
                action();
            }
        }
    }
}
