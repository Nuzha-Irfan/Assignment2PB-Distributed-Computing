using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.IO;
using IronPython.Modules;
using Path = System.IO.Path;

using System.Net.NetworkInformation;
using WebServer.Models;
using RestSharp;
using Newtonsoft.Json;

namespace ClientDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Job job = new Job();
            job.clientName = cname.Text;
            job.port = port.Text;
            job.ipAddress = ip.Text;

            RestClient restClient = new RestClient("https://localhost:44304/");
            RestRequest restRequest = new RestRequest("api/Jobs", Method.Post);
            restRequest.AddJsonBody(job);
            RestResponse restResponse = restClient.Execute(restRequest);

            Job returnDetails = JsonConvert.DeserializeObject<Job>(restResponse.Content);
            if (returnDetails != null)
            {
                MessageBox.Show("Client Succesfully Registered");
                Data.SetClientName(cname.Text);
                Data.setPortNumber(port.Text);
                Data.setIp(ip.Text);
                upload uploadWindow = new upload();
                uploadWindow.Show();
            }
            else
            {
                MessageBox.Show("Error details:" + restResponse.Content);
            }
          
        }
    }
}
