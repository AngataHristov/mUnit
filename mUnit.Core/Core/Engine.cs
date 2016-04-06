namespace mUnit.Core.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Enumerations;
    using Interfaces;
    using TestRunners;

    public class Engine : IEngine
    {
        private readonly string assemblyPath;

        public Engine(string assemblyPath, IOutputWriter writer)
        {
            this.assemblyPath = assemblyPath;
            this.Writer = writer;
        }

        public IOutputWriter Writer { get; private set; }

        public void Run()
        {
            var assemblyLoader = new AssemblyLoader(this.assemblyPath);
            var assembly = assemblyLoader.Assembly;

            var testMethodLoader = new TestMethodLoader(assembly);
            var testMethodesByType = testMethodLoader.LoadTestMethods();

            foreach (KeyValuePair<Type, List<MethodInfo>> testContainer in testMethodesByType)
            {
                var instance = Activator.CreateInstance(testContainer.Key);

                var testMethodes = testContainer.Value;

                foreach (MethodInfo testMethod in testMethodes)
                {
                    var testType = this.GetTestType(testMethod);

                    var testRunner = TestRunnerFactory
                        .GetTestRunner(testType, testMethod, instance);

                    testRunner.RunTest();

                    this.LogTestResult(testRunner, testMethod);


                }
            }
        }

        private void LogTestResult(TestRunner testRunner, MethodInfo testMethod)
        {
            switch (testRunner.TestResult)
            {
                case TestResult.Passed:
                    this.Writer.Write(string.Format("Test {0} passed!", testMethod.Name));
                    break;
                case TestResult.Failed:
                    this.Writer.Write(string.Format("Test {0} failed. Reason: {1}", testMethod.Name, testRunner.FailReason));
                    break;
                case TestResult.NotRun:
                    break;
                case TestResult.Skipped:
                    break;
            }
        }

        private TestType GetTestType(MethodInfo testMethod)
        {
            var uniqueAttributeTypes = new HashSet<TestType>();

            foreach (Attribute attr in testMethod.GetCustomAttributes())
            {
                switch (attr.GetType().Name)
                {
                    case "TestAttribute":
                        uniqueAttributeTypes.Add(TestType.Normal);
                        break;
                    case "ShouldThrowAttribute":
                        uniqueAttributeTypes.Add(TestType.ShouldThrow);
                        break;
                    case "TestCaseAttribute":
                        uniqueAttributeTypes.Add(TestType.TestCase);
                        break;
                }
            }

            if (uniqueAttributeTypes.Contains(TestType.ShouldThrow) &&
                uniqueAttributeTypes.Contains(TestType.TestCase))
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Method cannot have {0} and {1} attributes at the same time.",
                        TestType.ShouldThrow,
                        TestType.TestCase));
            }

            if (uniqueAttributeTypes.Contains(TestType.TestCase))
            {
                return TestType.TestCase;
            }

            if (uniqueAttributeTypes.Contains(TestType.ShouldThrow))
            {
                return TestType.ShouldThrow;
            }

            return TestType.Normal;
        }
    }
}
