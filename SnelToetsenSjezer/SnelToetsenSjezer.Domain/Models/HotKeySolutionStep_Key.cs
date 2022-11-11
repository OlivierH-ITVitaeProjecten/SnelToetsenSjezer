namespace SnelToetsenSjezer.Domain.Models
{
    public class HotKeySolutionStep_Key : HotKeySolutionStep
    {
        private string ExpectedInput = "";
        public HotKeySolutionStep_Key(string expectedInput)
        {
            ExpectedInput = expectedInput;
        }
        public override bool CheckForCompletion(string input)
        {
            if (input == ExpectedInput) return true;
            return false;
        }

        public override bool CheckForFail(string input)
        {
            if (input != ExpectedInput) return true;
            return false;
        }

        public override string ToString()
        {
            return ExpectedInput;
        }
    }
}
