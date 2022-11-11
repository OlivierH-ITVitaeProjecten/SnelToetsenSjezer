namespace SnelToetsenSjezer.Domain.Interfaces
{
    public interface IHotKeySolutionStep
    {
        bool CheckForCompletion(string input);
        bool CheckForFail(string input);
    }
}
