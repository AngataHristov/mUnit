namespace mUnit.Core.Core.TestRunners
{
    using System;
    using System.Reflection;
    using Enumerations;

    public class NormalTestRunner : TestRunner
    {
        public NormalTestRunner(MethodInfo testMethod, object typeInstance)
            : base(testMethod, typeInstance)
        {
        }

        public override void RunTest()
        {
            try
            {
                this.TestMethod.Invoke(this.TypeInstance, null);
                this.TestResult = TestResult.Passed;
            }
            catch (Exception ex)
            {
                this.SetFailResult(ex.InnerException.Message);
            }
        }
    }
}
