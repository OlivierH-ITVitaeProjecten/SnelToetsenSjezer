using SnelToetsenSjezer.Business;
using SnelToetsenSjezer.Domain.Interfaces;
using System.Diagnostics;

namespace SnelToetsenSjezer.WinForms.Forms
{
    public partial class GameForm : Form
    {
        private static readonly IHotKeyService _myHotKeyService = new HotKeyService();
        public GameForm()
        {
            InitializeComponent();

            _myHotKeyService.ProcessHotkeysXmlFile("../../../../hotkeys.xml");
        }

        public void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine($"keyCode: {e.KeyCode} keyValue: {e.KeyValue} keyData: {e.KeyData}");

            bool doTest = _myHotKeyService.CheckAgainstExpectedKey(e.KeyValue.ToString());
            Debug.WriteLine(doTest + " " + e.KeyValue);
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
