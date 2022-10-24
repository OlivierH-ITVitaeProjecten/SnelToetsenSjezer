namespace SnelToetsenSjezer.Domain.Types
{
    public class PressedKeysDict : Dictionary<string, bool> { };
    public class GameStateCallbackData : Dictionary<string, string> { };

    /// <summary>
    /// This is a List of key(s) that are expected for this step, a step can be a single entry (ex: ".. ,A, ..") or multiple entrys (ex: ".. ,A+B+C, ..")
    /// </summary>
    public class HotKeySolutionStep : List<string> { };

    /// <summary>
    /// This is a List of HotKeySolutionStep's
    /// </summary>
    public class HotKeySolution : List<HotKeySolutionStep> { };

    /// <summary>
    /// This is a List of HotKeySolution's
    /// </summary>
    public class HotKeySolutions : List<HotKeySolution> { };
}
