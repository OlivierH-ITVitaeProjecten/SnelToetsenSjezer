// I need to have some type of "HotKeySolutionStep" derived object that can be
// - HotKeySolutionKey
// - HotKeySolutionKeyCombo
// - HotKeySolutionString
// These should have the following functions
// - CheckForComplete(...)
// - CheckForPartiallyComplete(...)
// - CheckForFailed(...)

//public class HotKeySolutionStep : List<SolutionStepPart> { };

using SnelToetsenSjezer.Domain.Interfaces;
using System.Diagnostics;
/// <summary>
/// HotKeySolutionStep is a parent class of a bunch of other things...<br/>
/// (TODO: write proper description!!)<br/>
/// <br/>
/// 'Steps' are the different bits that come together to form a HotKeySolution, as the name implies they are<br/>
/// the steps of that solution or 'the bits between the commas'
/// </summary>
namespace SnelToetsenSjezer.Domain.Models
{
    public class HotKeySolutionStep : IHotKeySolutionStep
    {
        public virtual bool CheckForCompletion(string input)
        {
            Debug.WriteLine("ehm wtf waarom word dit uitgevoerd ipv overschreven?!");
            return false;
        }

        public virtual bool CheckForFail(string input)
        {
            return false;
        }
    }
}
