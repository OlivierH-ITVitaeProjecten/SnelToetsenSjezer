namespace SnelToetsenSjezer.Domain.Models
{
    public enum HotKeyBehaviour
    {
        AllAtOnce,
        Sequential
    }
    public class HotKey
    {
        public bool Failed { get; set; }
        public int Fails { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public List<Array> Steps { get; set; }
        public HotKeyBehaviour Behaviour { get; set; }
    }
}