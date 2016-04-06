namespace mUnit.Core.Core.TestRunners
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Attributes;
    using Enumerations;

    public class ShouldThrowTestRunner : TestRunner
    {
        private const string FailMessageOutPut = "Expected exception {0} was not thrown";

        public ShouldThrowTestRunner(MethodInfo testMethod, object typeInstance)
            : base(testMethod, typeInstance)
        {
            // TODO: Validate method attributes
        }

        public override void RunTest()
        {
            var throwAttr = (ShouldThrowAttribute)this.TestMethod
                    .GetCustomAttributes()
                    .First(a => a is ShouldThrowAttribute);
            var expectedExceptionType = throwAttr.ExceptionType;

            var message = string.Format(FailMessageOutPut, expectedExceptionType.Name);

            try
            {
                this.TestMethod.Invoke(this.TypeInstance, null);

                this.SetFailResult(message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.GetType().Name == expectedExceptionType.Name)
                {
                    this.TestResult = TestResult.Passed;
                }
                else
                {
                    this.SetFailResult(message);
                }
            }
        }
    }
}
