using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Web;
using RestSharp;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RemoteServer
{
    public class serverImplementation : server
    {
        public void Downloadjobs(string remoteUri)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri("http://mysite.com/myfile.py"), @"c:\myfile.py");

        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine( e.ProgressPercentage);
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
           Console.WriteLine("Download completed!");
        }


        public void uploadSolutions(string fileName)
        {
            string savePath = "c:\\temp\\uploads\\";
            string pathToCheck = savePath + fileName;

            // Create a temporary file name to use for checking duplicates.
            string tempfileName = "";

            // Check to see if a file already exists with the
            // same name as the file to upload.        
            if (System.IO.File.Exists(pathToCheck))
            {
                int counter = 2;
                while (System.IO.File.Exists(pathToCheck))
                {
                    // if a file with this name already exists,
                    // prefix the filename with a number.
                    tempfileName = counter.ToString() + fileName;
                    pathToCheck = savePath + tempfileName;
                    counter++;
                }

                fileName = tempfileName;

            }
            Console.WriteLine("Your file was saved Succesfully");
        }
    }
}
