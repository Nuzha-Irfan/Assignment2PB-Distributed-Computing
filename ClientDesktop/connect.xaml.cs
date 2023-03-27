using Newtonsoft.Json;
using RestSharp;
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
using System.Windows.Shapes;
using WebServer.Controllers;
using WebServer.Models;

namespace ClientDesktop
{
    /// <summary>
    /// Interaction logic for connect.xaml
    /// </summary>
    public partial class connect : Window
    {
        public connect()
        {
            InitializeComponent();
            string connectedClient = Data.getConnectedClient();

          

            total.Text = Data.getJobCount().ToString();
        }

        private void btnGetAll_Click(object sender, RoutedEventArgs e)
        {

            RestClient restClient = new RestClient("https://localhost:44304/");
            RestRequest restRequest = new RestRequest("api/Jobs" , Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);
            
            IEnumerable<Job> jobs = JsonConvert.DeserializeObject<IEnumerable<Job>>(restResponse.Content);
            serviceInfo.ItemsSource = jobs;
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Data.SetClientName(btn.Tag.ToString());
            //client.clientName = btn.Tag.ToString();
            solution solutiontWindow = new solution();
            solutiontWindow.Show();
        }
    }
}
