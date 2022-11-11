using SnelToetsenSjezer.Domain.Types;

namespace SnelToetsenSjezer.Domain.Models
{
    public class HotKey
    {
        public bool Completed { get; set; }
        public bool Failed { get; set; }
        public int Attempt { get; set; } = 1;
        public int Duration { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public HotKeySolutions Solutions { get; set; }

        public void ResetForNewGame()
        {
            Completed = false;
            Failed = false;
            Attempt = 1;
            Duration = 0;

            Solutions.ForEach(s =>
            {
                s.correctSteps = 0;
            });
        }
    }
}