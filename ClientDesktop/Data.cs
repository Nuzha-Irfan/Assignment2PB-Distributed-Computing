using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDesktop
{
    public static class Data
    {
        public static string clientName;
        public static string portNumber;
        public static string ipAddress;
        public static string filename;
        public static string solution;
        public static int id;
        public static string jobDesc;
        public static int solutionCount ;
        public static string connectedClient;
        public static int jobCount;


        public static void  SetClientName (string name)
        {
            clientName = name;
        }

        public static String getClientName()
        {
            return clientName;
        }

        public static void SetConnectedClient(string name)
        {
            clientName = name;
        }

        public static String getConnectedClient()
        {
            return connectedClient;
        }

        public static void SetjobDesc(string name)
        {
            jobDesc = name;
        }

        public static String getjobDesc()
        {
            return jobDesc;
        }
        public static void setPortNumber(string port)
        {
            portNumber = port;
        }

        public static String getPortNumber()
        {
            return portNumber;
        }


        public static void setIp(string Ip)
        {
            ipAddress = Ip;
        }

        public static String getIpAdd()
        {
            return ipAddress;
        }

        public static void setFilename(string file)
        {
            filename = file;
        }

        public static String getFilename()
        {
            return filename;
        }

        public static void setSolution(string solu)
        {
            solution = solu;
        }

        public static string getSolution()
        {
            return solution;
        }

        public static void setSolutionCount(int sol)
        {
            solutionCount = sol;
        }
        public static int getSolutionCount()
        {
            return solutionCount;
        }

        public static void setJobCount(int sol)
        {
            jobCount = sol;
        }
        public static int getJobCount()
        {
            return jobCount;
        }

        public static void setId(int idd)
        {
            id = idd;
        }

        public static int getId()
        {
            return id;
        }
    }
}
