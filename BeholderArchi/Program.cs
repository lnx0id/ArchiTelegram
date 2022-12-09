using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace BeholderArchi
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
        }

        public static async Task ConsoleLog(string text) {
            try
            {
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                WriteFile(docPath + "\\ARCHII","\\log.txt", text);      
            }
            catch
            {
                await ConsoleLog("Ann Error ocured!");
            }
            var formMain = GetForm();

            var label = formMain.Controls
                             .OfType<TextBox>()
                             .FirstOrDefault(l => l.Name == "Console");

            if (label != null && label.InvokeRequired)
            {
                label.Invoke(new MethodInvoker(delegate
                {         
                    label.Text = text + "  [LAST LOG]";
                }));
            }
        }

        public static void AddChatInfo(string jsonString) {
            try
            {
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                WriteFile(docPath+"\\ARCHII", "chats.json", jsonString);
            }
            catch
            {
                Task.Run(() => ConsoleLog("[ERROR] With FIlE JSON")).Wait();
            }
        }

        public static Form GetForm()
        {
            var form1 = Application.OpenForms
                               .OfType<Form>()
                               .FirstOrDefault(f => f.Name == "FormMain");

            if (form1 == null) return null;
            else return form1; 
        }
        private static void WriteFile(string workingDirektory,string filePath, string text)
        {
            if (File.Exists(workingDirektory + filePath))
            {
                StreamReader streamReader = new StreamReader(workingDirektory+filePath);
                var ReadedText = streamReader.ReadToEnd();
                streamReader.Close();
                if (!ReadedText.Contains(text))
                {
                    StreamWriter streamWriter = new StreamWriter(workingDirektory + filePath);
                    streamWriter.Write(ReadedText + "\n" + text);
                    streamWriter.Close();
                }
            }
            else
            {
                DirectoryInfo CreatedDir = Directory.CreateDirectory(workingDirektory);
                Stream file = File.Create(workingDirektory + filePath);
                StreamWriter streamWriter = new StreamWriter(file);
                streamWriter.Write(text);
                streamWriter.Close();

            }
        }
    }
}