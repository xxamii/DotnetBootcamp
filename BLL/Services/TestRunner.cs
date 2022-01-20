using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using BLL.Abstractions.Interfaces;
using Core.Models;
using Core.Attributes;
using Core.Exceptions;

namespace BLL.Services
{
    public class TestRunner : ITestRunner
    {
        public List<TestInfo> RunTests()
        {
            List<TestInfo> testResults = new List<TestInfo>();
            List<Type> testClasses = GetAllTestClasses();

            foreach(Type type in testClasses)
            {
                List<TestInfo> currentTestReesults = RunTestMethodsOfClass(type);
                testResults.AddRange(currentTestReesults);
            }

            return testResults;
        }

        private List<Type> GetAllTestClasses()
        {
            List<Type> result = new List<Type>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach(Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                result.AddRange(types.Where(type => type.IsClass && type.IsPublic && type.CustomAttributes.Any(x => x.AttributeType == typeof(TestClass))));
            }

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
