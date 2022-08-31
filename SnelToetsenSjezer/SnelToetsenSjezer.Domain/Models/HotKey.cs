namespace SnelToetsenSjezer.Domain.Models
{
    public class HotKey
    {
        public bool Failed { get; set; }
        public int Attempt { get; set; } = 1;
        public string Category { get; set; }
        public string Description { get; set; }
        public List<List<List<string>>> Solutions { get; set; }
    }
}