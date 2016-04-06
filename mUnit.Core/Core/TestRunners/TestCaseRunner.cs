namespace mUnit.Core.Core.TestRunners
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Attributes;
    using Enumerations;

    public class TestCaseRunner : TestRunner
    {
        public TestCaseRunner(MethodInfo testMethod, object typeInstance)
            : base(testMethod, typeInstance)
        {
        }

        public override void RunTest()
        {
            var testCaseAttributes = this.TestMethod
                .GetCustomAttributes()
                .Where(a => a is TestCaseAttribute);

            int totalTests = testCaseAttributes.Count();
            int passedTests = 0;

            foreach (TestCaseAttribute Attr in testCaseAttributes)
            {
                object param = Attr.Param;

                try
                {
                    this.TestMethod.Invoke(this.TypeInstance, new object[] { param });

                    passedTests++;
                }
                catch (Exception ex)
                {
                    this.SetFailResult(ex.InnerException.Message);
                }
            }

            if (passedTests == totalTests)
            {
                this.TestResult = TestResult.Passed;
            }
            else
            {
                this.SetFailResult(string.Format("{0}/{1} failed", totalTests - passedTests, totalTests));
            }
        }
    }
}


