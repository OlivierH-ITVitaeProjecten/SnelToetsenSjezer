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
