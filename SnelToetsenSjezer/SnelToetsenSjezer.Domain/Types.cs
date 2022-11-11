using SnelToetsenSjezer.Domain.Models;

namespace SnelToetsenSjezer.Domain.Types
{
    public class PressedKeysDict : Dictionary<string, bool> { };
    public class GameStateCallbackData : Dictionary<string, string> { };

    /// <summary>
    /// HotKeySolutions is a List of HotKeySolution's.
    /// </summary>
    public class HotKeySolutions : List<HotKeySolution> { };
}
