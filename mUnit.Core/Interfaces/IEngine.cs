namespace mUnit.Core.Interfaces
{
    public interface IEngine
    {
        IOutputWriter Writer { get; }

        void Run();
    }
}
