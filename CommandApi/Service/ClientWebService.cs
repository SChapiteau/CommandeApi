using CommandApi.Entity;

namespace CommandApi.Service
{
    internal class ClientWebService
    {
        private object urlWebServiceClient;

        public ClientWebService(object urlWebServiceClient)
        {
            this.urlWebServiceClient = urlWebServiceClient;
            Init();
        }

        private void Init()
        {
            //Initialise web service
            throw new NotImplementedException();
        }

        public Client GetClientCrm(int id)
        {
            throw new NotImplementedException();
        }

    }
}