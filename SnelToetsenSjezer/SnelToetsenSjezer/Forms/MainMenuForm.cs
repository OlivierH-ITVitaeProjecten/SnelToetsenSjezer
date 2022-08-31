using SnelToetsenSjezer.Business;
using SnelToetsenSjezer.Domain.Interfaces;
using SnelToetsenSjezer.Domain.Models;
using SnelToetsenSjezer.WinForms.Forms;
using System.Diagnostics;

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
            MyHotKeyService.ProcessHotkeysXmlFile("../../../../hotkeys.xml");
            
            AllCategories = MyHotKeyService.GetCategories();
            lb_categories.DataSource = AllCategories;
            lb_categories.SetItemChecked(0, true);

            HotKeysInSelectedCategories = MyHotKeyService.GetHotKeysInCategory(AllCategories[0]);
            int initialMaxHotKeys = HotKeysInSelectedCategories.Count();

            lbl_howmanyquestions.Text = $"How many questions? (1-{initialMaxHotKeys})";
            num_howmanyquestions.Minimum = 1;
            num_howmanyquestions.Maximum = initialMaxHotKeys;
        }

        public void HandleCategoryChanges()
        {            
            SelectedCategories.Clear();
            foreach (string category in lb_categories.CheckedItems)
            {
                SelectedCategories.Add(category);
            }

            HotKeysInSelectedCategories = MyHotKeyService.GetHotKeysInCategories(SelectedCategories);
            int hotKeyCount = HotKeysInSelectedCategories.Count();

            lbl_howmanyquestions.Text = $"How many questions? (1-{hotKeyCount})";
            num_howmanyquestions.Maximum = hotKeyCount;
            if (num_howmanyquestions.Value > hotKeyCount) num_howmanyquestions.Value = hotKeyCount;
        }

        private void HowManyQuestions_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;
            if (self.Value > self.Maximum) self.Value = self.Maximum;
            if (self.Value < self.Minimum) self.Value = self.Maximum;
        }
        private void StartGame_Click(object sender, EventArgs e)
        {
            MyHotKeyGameService!.SetHotKeys(HotKeysInSelectedCategories);

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
    }
}