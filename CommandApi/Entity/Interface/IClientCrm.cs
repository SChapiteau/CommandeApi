using CommandApi.Controllers;
using CommandApi.Entity;
using CommandApi.Service;

namespace CommandApi.DAL
{
    public interface IClientCrm
    {

        public Client GetClient(int clientId);

        bool HasCommandInProgress(int clientId);
        
    }
}
