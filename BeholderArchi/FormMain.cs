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
            dLC = new RunTelegram();
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
            
        }
        private void FormMain_Leave(object sender, EventArgs e)
        {
            
        }

        private void SeeLogsLable_Click(object sender, EventArgs e)
        {
            openFileByNotepad("log.txt");
        }

        private void Console_Click(object sender, EventArgs e)
        {
            openFileByNotepad("log.txt");
        }

        private void UpdateListButton_Click(object sender, EventArgs e)
        {
            openFileByNotepad("chats.json");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                long chatId = long.Parse(idTextBox.Text);
                var messageContext = Microsoft.VisualBasic.Interaction.InputBox("Что пишем?", "Сообщение ->", "Салам");
                await dLC.SendMessage(messageContext, chatId);
                ConsoleLog($"{messageContext} sended to {chatId}");
            }
            catch {
                Task.Run(() => Program.ConsoleLog("[ERROR] with sending")).Wait();
            }

        }
        private void openFileByNotepad(string fileName)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            try
            {
                if (System.IO.File.Exists($"{docPath}\\ARCHII\\"+fileName))
                {
                    Process.Start("notepad.exe", @$"{docPath}\ARCHII\"+fileName);
                }
                else
                {
                    MessageBox.Show("\n\n-- файл логов ещё не создан дождитесь первого лога", "НЕТУ ФАЙЛА", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n\n-- ВОУ ДЕЛО ПЛОХО", "КАКЕ-ТО ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
