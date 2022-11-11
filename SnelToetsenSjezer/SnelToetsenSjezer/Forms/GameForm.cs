using SnelToetsenSjezer.Domain.Interfaces;
using System.Diagnostics;

namespace SnelToetsenSjezer.WinForms.Forms
{
    public partial class GameForm : Form
    {
        public static IHotKeyGameService? HotKeyGameService = null;
        public static bool DealingWithStringInput = false;

        public GameForm(IHotKeyGameService gameService)
        {
            InitializeComponent();

            HotKeyGameService = gameService;
            HotKeyGameService.SetGameStateUpdatedCallback(UpdateForm);
            HotKeyGameService.SetGameTimerCallback(UpdateTimer);
            HotKeyGameService.StartGame();
        }

        public void UpdateTimer(int gameTicks, bool paused)
        {
            lbl_timer.Text = HotKeyGameService!.GameTicksToTimeStr(gameTicks);
            lbl_timer.ForeColor = paused ? Color.Gray : Color.Black;
        }

        public void UpdateForm(string basicState, Dictionary<string, string> stateDetails)
        {
            switch (basicState)
            {
                case "playing":
                    string hotKeyText = $"Hotkey {stateDetails["index"]} of {stateDetails["count"]}";
                    if (stateDetails.ContainsKey("attempt"))
                    {
                        hotKeyText += (Int32.Parse(stateDetails["attempt"]) > 1) ? $"(Attempt #{stateDetails["attempt"]})" : "";
                    }
                    lbl_currhotkey.Text = hotKeyText;
                    lbl_category_val.Text = stateDetails["category"];
                    lbl_description_val.Text = stateDetails["description"];
                    break;
                case "correct":
                    lbl_description_val.Text = "Correct!\nGet ready for the next question!";
                    break;
                case "failed":
                    lbl_description_val.Text = "Failed!\n" +
                        "The correct answer is: " + stateDetails["solution"];
                    break;
                case "finished":
                    lbl_description_val.Text = "Finished!!!\n(should jump to gameover screen/form)";

                    Form myGameOverForm = new GameOverForm(HotKeyGameService!);
                    myGameOverForm.Show();
                    this.Close();
                    break;
            }
            if (stateDetails.ContainsKey("userinputsteps"))
            {
                lbl_userinputsteps_val.Text = stateDetails["userinputsteps"];
            }
            if (stateDetails.ContainsKey("expecting_string"))
            {
                if (stateDetails["expecting_string"] == "1")
                {
                    if (!DealingWithStringInput)
                    {
                        DealingWithStringInput = txt_string_input.Enabled = true;
                        txt_string_input.Focus();
                    }
                }
                else
                {
                    if (DealingWithStringInput)
                    {
                        txt_string_input.Text = "";
                        DealingWithStringInput = txt_string_input.Enabled = false;
                        this.Focus();
                    }
                }
            }
        }

        public void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            HotKeyGameService!.KeyDown(e.KeyCode.ToString());
        }

        public void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            HotKeyGameService!.KeyUp(e.KeyCode.ToString());
        }

        public void GameForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            // Workaround for 'e.CloseReason == CloseReason.UserClosing' not working as advertised
            bool closedByUser = !new StackTrace().GetFrames().Any(
                x => x.GetMethod()!.Name == "Close"
            );

            if (closedByUser)
            {
                Debug.WriteLine("User closed GameForm! force-stopping the game!");
                HotKeyGameService!.StopGame(true);
            }
        }

        private void txt_string_input_TextChanged(object sender, EventArgs e)
        {
            if (DealingWithStringInput)
            {
                HotKeyGameService!.StringInput(txt_string_input.Text);
            }
        }
    }
}
