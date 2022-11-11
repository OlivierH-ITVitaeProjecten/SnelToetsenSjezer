using System.Diagnostics;

namespace SnelToetsenSjezer.Domain.Models
{
    public class HotKeySolutionStep_KeyCombo : HotKeySolutionStep
    {
        private List<string> ExpectedInput = new List<string>();
        public HotKeySolutionStep_KeyCombo(List<string> expectedInput)
        {
            ExpectedInput = expectedInput;
        }
        public override bool CheckForCompletion(string input)
        {
            Debug.WriteLine("Checking for completion");
            Debug.WriteLine($" - input: {input} | expected: {String.Join("+", ExpectedInput)}");

            List<string> inputPieces = input.Split("+").ToList();
            if (inputPieces.SequenceEqual(ExpectedInput)) return true;
            return false;
        }

        public override bool CheckForFail(string input)
        {
            List<string> inputPieces = input.Split("+").ToList();
            if (!inputPieces.SequenceEqual(ExpectedInput)) return true;
            return false;
        }

        public override string ToString()
        {
            return String.Join("+", ExpectedInput);
        }
    }
}
