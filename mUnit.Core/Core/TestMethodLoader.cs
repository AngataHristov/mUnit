namespace mUnit.Core.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Attributes;

    public class TestMethodLoader
    {
        public TestMethodLoader(Assembly assembly)
        {
            this.Assembly = assembly;
        }

        public Assembly Assembly { get; private set; }

        public IDictionary<Type, List<MethodInfo>> LoadTestMethods()
        {
            IDictionary<Type, List<MethodInfo>> typeData = new Dictionary<Type, List<MethodInfo>>();

            var allTypesInAssembly = this.Assembly.GetTypes();

            foreach (Type type in allTypesInAssembly)
            {
                bool hasTestContainerAttribute = type
                    .GetCustomAttributes(typeof(TestContainerAttribute)).Any();

                if (type.IsClass && hasTestContainerAttribute)
                {
                    this.GetTestMethods(typeData, type);
                }
            }

            return typeData;
        }

        private void GetTestMethods(IDictionary<Type, List<MethodInfo>> typeData, Type type)
        {
            typeData[type] = new List<MethodInfo>();

            var allMethods = type.GetMethods();

            foreach (MethodInfo method in allMethods)
            {
                bool hasTestAttribute = method.GetCustomAttributes().Any(a => a is TestAttribute);
                if (hasTestAttribute)
                {
                    typeData[type].Add(method);
                }
            }
        }
    }
}
