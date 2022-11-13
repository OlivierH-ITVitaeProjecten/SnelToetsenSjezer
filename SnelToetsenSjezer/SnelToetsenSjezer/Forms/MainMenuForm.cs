using SnelToetsenSjezer.Business;
using SnelToetsenSjezer.Domain.Interfaces;
using SnelToetsenSjezer.Domain.Models;
using SnelToetsenSjezer.WinForms.Forms;

namespace SnelToetsenSjezer
{
    public partial class MainMenuForm : Form
    {
        public static IHotKeyService MyHotKeyService = new HotKeyService();
        public static IHotKeyGameService? MyHotKeyGameService = new HotKeyGameService();

        public static List<string> AllCategories = new List<string>();
        public static List<string> SelectedCategories = new List<string>();
        public static List<HotKey> HotKeysInSelectedCategories = new List<HotKey>();

        public MainMenuForm()
        {
            InitializeComponent();
            InitializeHotKeySjezer();
        }

        public void InitializeHotKeySjezer()
        {
            MyHotKeyService.ProcessHotkeysXmlFile("hotkeys.xml");

            AllCategories = MyHotKeyService.GetCategories();
            lb_categories.DataSource = AllCategories;
            lb_categories.SetItemChecked(0, true);
            HandleCategoryChanges();

            cmbbox_continue.SelectedIndex = 0;
        }

        public void HandleCategoryChanges()
        {
            List<string> userSelection = new List<string>();
            foreach (string category in lb_categories.CheckedItems)
            {
                userSelection.Add(category);
            }
            if (SelectedCategories.SequenceEqual(userSelection)) return;

            SelectedCategories = userSelection;
            HotKeysInSelectedCategories = MyHotKeyService.GetHotKeysInCategories(SelectedCategories);
            int hotKeyCount = HotKeysInSelectedCategories.Count();

            lbl_howmanyquestions.Text = $"How many questions? (1-{hotKeyCount})";
            num_howmanyquestions.Minimum = 1;
            num_howmanyquestions.Maximum = hotKeyCount;

            //if (num_howmanyquestions.Value > hotKeyCount) num_howmanyquestions.Value = hotKeyCount;
            num_howmanyquestions.Value = hotKeyCount;
            btn_howmanyquestions_5.Enabled = (hotKeyCount >= 5);
            btn_howmanyquestions_10.Enabled = (hotKeyCount >= 10);
            btn_howmanyquestions_25.Enabled = (hotKeyCount >= 25);

        }

        private void HowManyQuestions_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;
            if (self.Value > self.Maximum) self.Value = self.Maximum;
            if (self.Value < self.Minimum) self.Value = self.Maximum;
        }
        private List<HotKey> GetRandomItemsFromList(int amount, List<HotKey> list)
        {
            var rnd = new Random();
            List<HotKey> randomHotKeys = list.OrderBy(item => rnd.Next()).ToList();
            randomHotKeys.RemoveRange(amount, randomHotKeys.Count() - amount);
            return randomHotKeys;
        }

        private void StartGame_Click(object sender, EventArgs e)
        {
            List<HotKey> randomHotKeys = GetRandomItemsFromList(
                (int)Math.Round(num_howmanyquestions.Value),
                HotKeysInSelectedCategories
            );
            bool auto = (cmbbox_continue.SelectedIndex == 0 || cmbbox_continue.SelectedIndex == 2);
            bool key = (cmbbox_continue.SelectedIndex == 1 || cmbbox_continue.SelectedIndex == 2);

            MyHotKeyGameService!.ConfigureGame(randomHotKeys, auto, key);

            Form myGameForm = new GameForm(MyHotKeyGameService);
            myGameForm.Show();
        }

        private void CategoriesSelectedIndexChanged(object sender, EventArgs e)
        {
            HandleCategoryChanges();
        }
        private void CategoriesItemCheckedChanged(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox chk = (CheckedListBox)sender;
            if (e.NewValue == CheckState.Unchecked && chk.CheckedItems.Count == 1)
                e.NewValue = CheckState.Checked;
        }

        private void btn_howmanyquestions_Click(object sender, EventArgs e)
        {
            string btnId = ((Button)sender).Name.Substring(21);
            switch (btnId)
            {
                case "max":
                    num_howmanyquestions.Value = num_howmanyquestions.Maximum;
                    break;
                default:
                    if (int.TryParse(btnId, out int btnIdNumber))
                    {
                        if (btnIdNumber <= num_howmanyquestions.Maximum)
                            num_howmanyquestions.Value = btnIdNumber;
                    }
                    break;
            }
        }
    }
}