namespace mUnit.Core.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Method)]
    public class ShouldThrowAttribute : Attribute
    {
        public ShouldThrowAttribute(Type exceptioType)
        {
            this.ExceptionType = exceptioType;
        }

        public Type ExceptionType { get; private set; }
    }
}
