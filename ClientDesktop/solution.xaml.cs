using Microsoft.Scripting.Hosting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Schema;
using WebServer.Models;
using static IronPython.Modules._ast;
using System.ServiceModel;
using RemoteServer;
using System.ComponentModel;
using System.Net;
using Microsoft.Win32;

namespace ClientDesktop
{
    /// <summary>
    /// Interaction logic for solution.xaml
    /// </summary>
    public partial class solution : Window
    {
        private int total = 0;
        private server foob;
        public solution()
        {
            InitializeComponent();
            //This is a factory that generates remote connections to our remote class. Thisis what hides the RPC stuff!
            ChannelFactory<server> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8200/StudentBusinessService";
            foobFactory = new ChannelFactory<server>(tcp, URL);
            foob = foobFactory.CreateChannel();



            NameText.Content = Data.getClientName();
            current.Text = Data.getClientName();
            string clientName = Data.getConnectedClient();
        }

        public void getJobs(string name)
        {
            RestClient restClient = new RestClient("https://localhost:44304/");
            RestRequest restRequest = new RestRequest("api/search/{clientName}", Method.Get);
            restRequest.AddUrlSegment("clientName", name);
            RestResponse restResponse = restClient.Execute(restRequest);

            IEnumerable<jobsDescription> jobs = JsonConvert.DeserializeObject<IEnumerable<jobsDescription>>(restResponse.Content);
            serviceInfo.ItemsSource = jobs;

        }

        private void btnGetAll_Click(object sender, RoutedEventArgs e)
        {
            getJobs(Data.getClientName());
        }

        public void btnDownload_click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Data.SetjobDesc(btn.Tag.ToString());
            DoJob();
            total = total + 1;
            Data.setJobCount(total);

         
        }

        public void DoJob()
        {
            jobsDescription description = new jobsDescription();
            jobsCount count = new jobsCount();

            RestClient restClient = new RestClient("https://localhost:44304/");
            RestRequest restRequest = new RestRequest("api/file/{fileName}", Method.Get);
            restRequest.AddUrlSegment("fileName", Data.getjobDesc());
            RestResponse restResponse = restClient.Execute(restRequest);

            jobsDescription jobs = JsonConvert.DeserializeObject<jobsDescription>(restResponse.Content);
            Data.setId(jobs.Id);
            Data.setFilename(jobs.filename);
            Data.SetClientName(jobs.clientName);
            Data.setSolutionCount(Int32.Parse(jobs.solutionCount.ToString()));


            ScriptEngine pythonEngine = IronPython.Hosting.Python.CreateEngine();
            MemoryStream stdout = new MemoryStream();
            pythonEngine.Runtime.IO.SetOutput(stdout, Encoding.Default);
            MemoryStream stderr = new MemoryStream();
            pythonEngine.Runtime.IO.SetErrorOutput(stderr, Encoding.Default);

            ScriptSource pythonScript;
            //-Python script from an internal string literal------------------------
            pythonScript = pythonEngine.CreateScriptSourceFromFile(Data.getFilename());

            try
            {

                pythonScript.Execute();

                string stdOut = Encoding.Default.GetString(stdout.ToArray());
                string stdErr = Encoding.Default.GetString(stderr.ToArray());

                if (!String.IsNullOrEmpty(stdOut))
                {
                    Console.WriteLine(stdOut);
                    Data.setSolution(stdOut);

                    description.Id = Data.getId();
                    description.jobDes = Data.getjobDesc();
                    description.solution = Data.getSolution();
                    description.solutionCount = Data.getSolutionCount() + 1;
                    description.clientName = Data.getClientName();
                    description.filename = Data.getFilename();

                    //RestClient restClient1 = new RestClient("https://localhost:44304/");
                    RestRequest restRequest1 = new RestRequest("api/jobsDescriptions/{id}", Method.Put);
                    restRequest1.AddUrlSegment("id", description.Id);
                    restRequest1.AddJsonBody(JsonConvert.SerializeObject(description));
                    RestResponse restResponse1 = restClient.Execute(restRequest1);

                    if (restResponse1 != null)
                    {
                        MessageBox.Show("Added Solutions");

                        MessageBox.Show("File Succesfully Downloaded");
                      
                        

                        RestRequest restRequest2 = new RestRequest("api/jobsCounts", Method.Post);
                        restRequest2.AddJsonBody(count);
                        RestResponse restResponse2 = restClient.Execute(restRequest2);

                        MessageBox.Show(stdOut, "The Output of the Job ");
                    }
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



        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            connect connectWindow = new connect();
            connectWindow.Show();
        }


        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download completed!");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string filename;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".txt"; // Required file extension 
            fileDialog.Filter = "Text documents (.py)|*.py"; // Optional file extensions
            string path = System.IO.Path.Combine(@"~\Answers\");

            fileDialog.ShowDialog();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }

            filename = System.IO.Path.GetFileName(fileDialog.FileName);
            path = path + filename;
            File.Copy(fileDialog.FileName, path);

            MessageBox.Show("Solution Uploaded");

        }
    }
    }
