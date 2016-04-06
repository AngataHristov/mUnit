namespace mUnit.Sandbox
{
    using System.Reflection;
    using Core.Core;
    using Core.Interfaces;
    using IO;

    public class SandboxMain
    {
        private static readonly string assemblyPath = Assembly.GetExecutingAssembly().Location;

        public static void Main()
        {
            IOutputWriter writer = new ConsoleWriter();

            var engine = new Engine(assemblyPath, writer);
            engine.Run();
        }
    }
}
