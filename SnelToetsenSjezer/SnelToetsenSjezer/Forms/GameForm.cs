using SnelToetsenSjezer.Business;
using SnelToetsenSjezer.Domain.Interfaces;
using System.Diagnostics;

namespace SnelToetsenSjezer.WinForms.Forms
{
    public partial class GameForm : Form
    {
        public static IHotKeyGameService? MyHotKeyGameService = null;

        public GameForm(IHotKeyGameService gameService)
        {
            InitializeComponent();

            MyHotKeyGameService = gameService;
            MyHotKeyGameService.SetGameStateUpdatedCallback(UpdateForm);
            MyHotKeyGameService.SetGameTimerCallback(UpdateTimer);

            MyHotKeyGameService.StartGame();
        }

        public void UpdateTimer(int seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            lbl_timer.Text = time.ToString((seconds > 3600) ? @"hh\:mm\:ss" : @"mm\:ss");
        }

        public void UpdateForm(string bla)
        {
            int[] currHotKeyAndCount = MyHotKeyGameService!.GetCurrHotKeyAndCount();
            int[] currHotKeyStepAndCount = MyHotKeyGameService.GetCurrHotKeyStepAndCount();
            int currHotKeyAttempts = MyHotKeyGameService.GetCurrHotKeyAttempt();
            
            string currHotKeyText = $"Hot key {currHotKeyAndCount[0] + 1} of {currHotKeyAndCount[1]}";
            if (currHotKeyAttempts > 1) currHotKeyText += $" (Attempt #{currHotKeyAttempts})";

            lbl_currhotkey.Text = currHotKeyText;
            lbl_category_val.Text = MyHotKeyGameService.CurrHotKey_CategoryName();
            lbl_description_val.Text = MyHotKeyGameService.CurrHotKey_Description();

            progbar_correctsteps.Visible = true;
            progbar_correctsteps.Minimum = 0;
            progbar_correctsteps.Step = 1;
            progbar_correctsteps.Value = currHotKeyStepAndCount[0];
            progbar_correctsteps.Maximum = currHotKeyStepAndCount[1];
        }

        public void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            MyHotKeyGameService!.KeyDown(e.KeyCode.ToString());
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            MyHotKeyGameService!.KeyUp(e.KeyCode.ToString());
        }
    }
}
