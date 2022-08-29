using SnelToetsenSjezer.Business;
using SnelToetsenSjezer.Domain.Interfaces;
using System.Diagnostics;

namespace SnelToetsenSjezer.WinForms.Forms
{
    public partial class GameForm : Form
    {
        private static readonly IHotKeyService _myHotKeyService = new HotKeyService();
        private static readonly Dictionary<string,bool> _currentlyPressedKeys = new Dictionary<string,bool>();

        public GameForm()
        {
            InitializeComponent();
            _myHotKeyService.ProcessHotkeysXmlFile("../../../../hotkeys.xml");
            UpdateForm();
        }

        public void UpdateForm()
        {
            int[] currHotKeyAndCount = _myHotKeyService.GetCurrHotKeyAndCount();
            int[] currHotKeyStepAndCount = _myHotKeyService.GetCurrHotKeyStepAndCount();

            lbl_currhotkey.Text = $"Hot key {currHotKeyAndCount[0]+1} of {currHotKeyAndCount[1]}";
            lbl_category_val.Text = _myHotKeyService.CurrHotKey_CategoryName();
            lbl_description_val.Text = _myHotKeyService.CurrHotKey_Description();

            progbar_correctsteps.Visible = true;
            progbar_correctsteps.Minimum = 0;
            progbar_correctsteps.Step = 1;
            progbar_correctsteps.Value = currHotKeyStepAndCount[0];
            progbar_correctsteps.Maximum = currHotKeyStepAndCount[1];
        }

        public void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            string keyCodeAsString = e.KeyCode.ToString();
            if(!_currentlyPressedKeys.ContainsKey(keyCodeAsString) || !_currentlyPressedKeys[keyCodeAsString])
            {
                bool keyIsValid = _myHotKeyService.CheckAgainstExpectedKey(keyCodeAsString, out bool wholeSetCorrect);
                if(keyIsValid) progbar_correctsteps.PerformStep();
                _currentlyPressedKeys[keyCodeAsString] = true;

                Debug.WriteLine(keyIsValid + " " + e.KeyCode);
                Debug.WriteLine($"keyCode: {e.KeyCode} keyValue: {e.KeyValue} keyData: {e.KeyData}");
                if(wholeSetCorrect)
                {
                    Debug.WriteLine("The whole hotkey set is correct woohoo!");
                    _myHotKeyService.NextHotKey();
                    UpdateForm();
                }
            }

        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            string keyCodeAsString = e.KeyCode.ToString();
            if (_currentlyPressedKeys.ContainsKey(keyCodeAsString))
            {
                _currentlyPressedKeys[keyCodeAsString] = false;
            }
        }
    }
}
