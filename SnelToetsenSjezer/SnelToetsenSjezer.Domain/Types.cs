using SnelToetsenSjezer.Domain.Models;

namespace SnelToetsenSjezer.Domain.Types
{
    public class PressedKeysDict : Dictionary<string, bool> { };
    public class GameStateCallbackData : Dictionary<string, string> { };

    /// <summary>
    /// HotKeySolutionStep is a List of SolutionStepPart's.<br/>
    /// <br/>
    /// 'Steps' are the different bits that come together to form a HotKeySolution, as the name implies they are<br/>
    /// the steps of that solution or 'the bits between the commas'
    /// </summary>
    public class HotKeySolutionStep : List<SolutionStepPart> { };

    /// <summary>
    /// HotKeySolution is a List of HotKeySolutionStep's.<br/> 
    /// <br/>
    /// 'Solutions' are sets of HotKeySolutionStep's that need to be completed in order to complete the HotKey for the game
    /// </summary>
    public class HotKeySolution : List<HotKeySolutionStep> { };

    /// <summary>
    /// HotKeySolutions is a List of HotKeySolution's.
    /// </summary>
    public class HotKeySolutions : List<HotKeySolution> { };
}
