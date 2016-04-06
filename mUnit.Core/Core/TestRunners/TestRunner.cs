namespace mUnit.Core.Core.TestRunners
{
    using System.Reflection;
    using Enumerations;

    public abstract class TestRunner
    {
        protected TestRunner(MethodInfo testMethod, object typeInstance)
        {
            this.TestMethod = testMethod;
            this.TestResult = TestResult.NotRun;
            this.TypeInstance = typeInstance;
        }

        protected MethodInfo TestMethod { get; set; }

        protected object TypeInstance { get; set; }

        public TestResult TestResult { get; protected set; }

        public string FailReason { get; protected set; }

        protected void SetFailResult(string message)
        {
            this.FailReason = message;
            this.TestResult = TestResult.Failed;
        }

        public abstract void RunTest();
    }
}
