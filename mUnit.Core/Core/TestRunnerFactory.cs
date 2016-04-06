namespace mUnit.Core.Core
{
    using System;
    using System.Reflection;
    using Enumerations;
    using TestRunners;

    public class TestRunnerFactory
    {
        public static TestRunner GetTestRunner(TestType type, MethodInfo testMethodInfo, object typeInstance)
        {
            switch (type)
            {
                case TestType.Normal:
                    return new NormalTestRunner(testMethodInfo, typeInstance);
                case TestType.ShouldThrow:
                    return new ShouldThrowTestRunner(testMethodInfo, typeInstance);
                case TestType.TestCase:
                    return new TestCaseRunner(testMethodInfo, typeInstance);
                default:
                    throw new NotSupportedException("Test type has no supported runner at this moment");
            }
        }
    }
}