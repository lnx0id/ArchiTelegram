using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace BeholderArchi
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
        }

        /// <summary>
        ///  Saves given string to log file. And print last log at log panel.
        /// </summary>
        public static async Task ConsoleLog(string text) {
            try
            {
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (File.Exists(docPath + "\\ARCHII\\log.txt"))
                {
                    StreamReader streamReader = new StreamReader(docPath+"\\ARCHII\\log.txt");
                    var ReadedText = streamReader.ReadToEnd();
                    streamReader.Close();

                    StreamWriter streamWriter = new StreamWriter(docPath + "\\ARCHII\\log.txt");
                    streamWriter.Write(ReadedText+"\n"+text);
                    streamWriter.Close();
                }
                else {
                    DirectoryInfo CreatedDir = Directory.CreateDirectory(docPath+"\\ARCHII");
                    Stream file = File.Create(docPath + "\\ARCHII\\log.txt");
                    StreamWriter streamWriter = new StreamWriter(file);
                    streamWriter.Write(text);
                    streamWriter.Close();
                    
                }
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
        /// <summary>
        ///  Saves given string to log file. And print last log at log panel.
        /// </summary>
        public static void ConsoleLog(string text, Form formMain)
        {
            try
            {
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (File.Exists(docPath + "\\ARCHII\\log.txt"))
                {
                    StreamReader streamReader = new StreamReader(docPath + "\\ARCHII\\log.txt");
                    var ReadedText = streamReader.ReadToEnd();
                    streamReader.Close();

                    StreamWriter streamWriter = new StreamWriter(docPath + "\\ARCHII\\log.txt");
                    streamWriter.Write(ReadedText + "\n" + text);
                    streamWriter.Close();
                }
                else
                {
                    DirectoryInfo CreatedDir = Directory.CreateDirectory(docPath + "\\ARCHII");
                    Stream file = File.Create(docPath + "\\ARCHII\\log.txt");
                    StreamWriter streamWriter = new StreamWriter(file);
                    streamWriter.Write(text);
                    streamWriter.Close();

                }
            }
            catch 
            {
                Console.WriteLine("I cant read:(");
            }
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

        /// <summary>
        ///  Saves given json typed string to file with all chats, where bot ever been.
        /// </summary>
        public static void AddChatInfo(string jsonString) {
            try
            {
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (File.Exists(docPath + "\\ARCHII\\chats.json"))
                {
                    StreamReader streamReader = new StreamReader(docPath + "\\ARCHII\\chats.json");
                    string readToEnd = streamReader.ReadToEnd();
                    streamReader.Close();
                    if (!readToEnd.Contains(jsonString)) {
                        StreamWriter streamWriter = new StreamWriter(docPath + "\\ARCHII\\chats.json");
                        streamWriter.Write(readToEnd + "\n" + jsonString);
                        streamWriter.Close();
                    }
                }
                else {
                    DirectoryInfo CreatedDir = Directory.CreateDirectory(docPath + "\\ARCHII");
                    Stream file = File.Create(docPath + "\\ARCHII\\chats.json");
                    StreamWriter streamWriter = new StreamWriter(docPath + "\\ARCHII\\chats.json");
                    streamWriter.Write(jsonString);
                    streamWriter.Close();
                }
            }
            catch
            {
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
    }
}