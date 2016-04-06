namespace mUnit.Sandbox.IO
{
    using System;
    using Core.Interfaces;
    public class ConsoleWriter : IOutputWriter
    {
        public void Write(string output)
        {
            Console.WriteLine(output);
        }
    }
}
