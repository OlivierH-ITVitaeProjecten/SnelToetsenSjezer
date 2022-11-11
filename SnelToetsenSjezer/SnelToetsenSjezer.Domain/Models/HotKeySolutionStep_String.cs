namespace SnelToetsenSjezer.Domain.Models
{
    public class HotKeySolutionStep_String : HotKeySolutionStep
    {
        private string ExpectedInput = "";
        public HotKeySolutionStep_String(string expectedInput)
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
