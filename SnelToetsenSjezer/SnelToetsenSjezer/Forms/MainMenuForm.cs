using SnelToetsenSjezer.WinForms.Forms;

namespace SnelToetsenSjezer
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void btn_startgame_Click(object sender, EventArgs e)
        {
            Form myGameForm = new GameForm();
            myGameForm.Show();
        }
    }
}