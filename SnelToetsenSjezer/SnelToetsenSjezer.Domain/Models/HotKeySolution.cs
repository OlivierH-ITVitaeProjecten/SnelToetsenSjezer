using System.Diagnostics;

namespace SnelToetsenSjezer.Domain.Models
{
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
}
