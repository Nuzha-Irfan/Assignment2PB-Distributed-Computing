using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Networking_Server
{
    [ServiceContract]
    public interface server
    {
        [OperationContract]
       void Downloadjobs(string Filename);


       // [OperationContract]
       // void uploadSolutions();


        [OperationContract]
        void uploadSolutions(string Filename);


    }
}
