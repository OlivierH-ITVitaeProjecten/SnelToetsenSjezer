using SnelToetsenSjezer.Domain.Models;
using System.Diagnostics;

namespace SnelToetsenSjezer.Domain.Types
{
    public class PressedKeysDict : Dictionary<string, bool> { };
    public class GameStateCallbackData : Dictionary<string, string> { };

    /// <summary>
    /// HotKeySolution is a List of HotKeySolutionStep's.<br/> 
    /// <br/>
    /// 'Solutions' are sets of HotKeySolutionStep's that need to be completed in order to complete the HotKey for the game
    /// </summary>
    public class HotKeySolution : List<HotKeySolutionStep>
    {
        public int correctSteps = 0;
        public bool IsNextStepAString()
        {
            if (correctSteps < this.Count())
            {
                HotKeySolutionStep nextStep = this[correctSteps];

                Debug.WriteLine($"IsNextStepAString - correctSteps: {correctSteps} | nextStep: {nextStep.GetType()}");

                if (nextStep.GetType() == typeof(HotKeySolutionStep_String)) return true;
            }
            return false;
        }
    };

    /// <summary>
    /// HotKeySolutions is a List of HotKeySolution's.
    /// </summary>
    public class HotKeySolutions : List<HotKeySolution> { };
}
