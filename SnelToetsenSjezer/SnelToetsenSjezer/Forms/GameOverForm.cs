using SnelToetsenSjezer.Domain.Interfaces;
using SnelToetsenSjezer.Domain.Models;

namespace SnelToetsenSjezer.WinForms.Forms
{
    public partial class GameOverForm : Form
    {
        public GameOverForm(IHotKeyGameService gameService)
        {
            InitializeComponent();

            List<HotKey> gameHotKeys = gameService.GetGameHotKeys();

            NumHotKeysValue.Text = gameHotKeys.Count().ToString();
            TimeSpentValue.Text = gameService.GameTicksToTimeStr();

            gameHotKeys.ForEach(hotKey =>
            {
                datagrid_hotkeydetails.Rows.Add(
                    hotKey.Description,
                    hotKey.Attempt.ToString(),
                    gameService.GameTicksToTimeStr(hotKey.Duration)
                );
            });
        }
    }
}
