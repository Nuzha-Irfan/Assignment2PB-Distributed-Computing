using Microsoft.Scripting.Hosting;
using Microsoft.Win32;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WebServer.Models;

namespace ClientDesktop
{
    /// <summary>
    /// Interaction logic for upload.xaml
    /// </summary>
    public partial class upload : Window
    {
        private static string filename;
        private static string clientName;
        private static string port;
        private static string ipAddress;
        private static string connectedClient;
        public upload()
        {
            InitializeComponent();
            clientName = Data.getClientName();
            port=Data.getPortNumber();
            ipAddress = Data.getIpAdd();

            Data.SetConnectedClient(clientName);
            //connectedClient = Data.getClientName();
            NameText.Content = clientName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            jobsDescription description = new jobsDescription();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".txt"; // Required file extension 
            fileDialog.Filter = "Text documents (.py)|*.py"; // Optional file extensions
            string path = System.IO.Path.Combine(@"~\Solutions\");

            fileDialog.ShowDialog();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }

            filename = System.IO.Path.GetFileName(fileDialog.FileName);
            path = path + filename;
            File.Copy(fileDialog.FileName, path);


            description.clientName = clientName;
            description.jobDes = desc.Text;
            description.filename = path;
            RestClient restClient = new RestClient("https://localhost:44304/");
            RestRequest restRequest = new RestRequest("api/jobsDescriptions", Method.Post);
            restRequest.AddJsonBody(description);
            RestResponse restResponse = restClient.Execute(restRequest);

            jobsDescription returnDetails = JsonConvert.DeserializeObject<jobsDescription>(restResponse.Content);
            if (returnDetails != null)
            {
                for (int i = 0; i < 100; i++)
                {
                    progressbar1.Dispatcher.Invoke(() => progressbar1.Value = i, DispatcherPriority.Background);
                    Thread.Sleep(100);
                }
                MessageBox.Show("Job Succesfully uploaded");
                
            }
            else
            {
                MessageBox.Show("Error details:" + restResponse.Content);
            }
        }


        static void DoJob()
        {
            ScriptEngine pythonEngine = IronPython.Hosting.Python.CreateEngine();
            MemoryStream stdout = new MemoryStream();
            pythonEngine.Runtime.IO.SetOutput(stdout, Encoding.Default);
            MemoryStream stderr = new MemoryStream();
            pythonEngine.Runtime.IO.SetErrorOutput(stderr, Encoding.Default);

            ScriptSource pythonScript;
            //-Python script from an internal string literal------------------------
            pythonScript = pythonEngine.CreateScriptSourceFromFile(filename);

            try
            {

                pythonScript.Execute();

                string stdOut = Encoding.Default.GetString(stdout.ToArray());
                string stdErr = Encoding.Default.GetString(stderr.ToArray());

                if (!String.IsNullOrEmpty(stdOut))
                {
                    Console.WriteLine(stdOut);
                    MessageBox.Show(stdOut);
                }
                else
                {
                    Console.WriteLine(stdErr);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Data.SetConnectedClient(Data.getClientName());
            connect connectWindow = new connect();
            for (int i = 0; i < 100; i++)
            {
                progressbar1.Dispatcher.Invoke(() => progressbar1.Value = i, DispatcherPriority.Background);
                Thread.Sleep(100);
            }
            connectWindow.Show();
        }
    }
}
