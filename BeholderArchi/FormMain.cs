using System.Diagnostics;
using System.Runtime.InteropServices;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BeholderArchi
{
    public partial class FormMain : Form
    {
        private RunTelegram dLC;


        public FormMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Log don't be saved in log.txt :( use BeholderArchi.Program.ConsoleLog(string)
        /// </summary>
        private void ConsoleLog(string text) {
            Console.Text += "\n"+text;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            dLC = new RunTelegram();
        }
        private void FormMain_Leave(object sender, EventArgs e)
        {
            
        }

        private void SeeLogsLable_Click(object sender, EventArgs e)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            try
            {
                if (System.IO.File.Exists($"{docPath}\\ARCHII\\log.txt"))
                {
                    Process.Start("notepad.exe", @$"{docPath}\ARCHII\log.txt");
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n\n-- ÃÓÊÂÚ ·˚Ú¸ Ù‡ÈÎ Â˘∏ ÌÂ ÒÓÁ‰‡Ì ‰ÓÊ‰ËÚÂÒ¸ ÔÂ‚Ó„Ó ÎÓ„‡", "œŒ’Œ∆≈ Õ≈“” ‘¿…À¿", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Console_Click(object sender, EventArgs e)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            try
            {
                if (System.IO.File.Exists($"{docPath}\\ARCHII\\log.txt")){
                    Process.Start("notepad.exe", @$"{docPath}\ARCHII\log.txt"); 
                }else
                {
                    MessageBox.Show("\n\n-- Ù‡ÈÎ ÎÓ„Ó‚ Â˘∏ ÌÂ ÒÓÁ‰‡Ì ‰ÓÊ‰ËÚÂÒ¸ ÔÂ‚Ó„Ó ÎÓ„‡", "Õ≈“” ‘¿…À¿", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()+"\n\n-- ¬Œ” ƒ≈ÀŒ œÀŒ’Œ", " ¿ ≈-“Œ Œÿ»¡ ¿", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateListButton_Click(object sender, EventArgs e)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (System.IO.File.Exists($"{docPath}\\ARCHII\\chats.json"))
            {
                Process.Start("notepad.exe", @$"{docPath}\ARCHII\chats.json");
            }
            else
            {
                MessageBox.Show("\n\n-- Ù‡ÈÎ Ò ˜‡Ú‡ÏË Â˘∏ ÌÂ ÒÓÁ‰‡Ì ‰ÓÊ‰ËÚÂÒ¸ ÔÂ‚Ó„Ó ÎÓ„‡", "Õ≈“” ‘¿…À¿", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                long chatId = long.Parse(idTextBox.Text);
                var messageContext = Microsoft.VisualBasic.Interaction.InputBox("◊ÚÓ ÔË¯ÂÏ?", "—ÓÓ·˘ÂÌËÂ ->", "—‡Î‡Ï");
                await dLC.SendMessage(messageContext, chatId);
                ConsoleLog($"{messageContext} sended to {chatId}");
            }
            catch {
                Program.ConsoleLog("ERROR!!!", this);
            }

        }
    }
}
