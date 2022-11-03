using CommandApi.Controllers;
using CommandApi.Entity;
using CommandApi.Service;

namespace CommandApi.DAL
{
    public class ClientCrmWrapper
    {
        private ClientWebService clientWeb;

        public ClientCrmWrapper()
        {
            var urlWebServiceClient = ConfigurationHelper.getUrlCrm();
            clientWeb = new ClientWebService(urlWebServiceClient);
        }
        

        public Client GetClient(int clientId)
        {
            return clientWeb.GetClientCrm(clientId);
        }

        public bool HasCommandInProgress(int clientId)
        {
            bool isCommandinProgress = false;
            var client = clientWeb.GetClientCrm(clientId);

            foreach (var cmd in client.CommandesClient)
            {
                if (cmd.Etat == EtatCommande.NonPayer)
                {
                    isCommandinProgress = true; break;
                }
            }

            return isCommandinProgress;
        
        }
    }
}
