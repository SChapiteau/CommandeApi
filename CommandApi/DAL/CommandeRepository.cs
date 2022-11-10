using CommandApi.Entity;
using CommandApi.Entity.Interface;

namespace CommandApi.DAL
{
    internal class CommandeRepository : ICommandeRepository
    {
        private static CommandeRepository _instance;
        public static CommandeRepository Instance
        {
            get{
                if (_instance == null)
                    _instance = new CommandeRepository();
                return _instance;
            }
        }
            
        public Commande GetCommande(int commandeId)
        {
            throw new NotImplementedException();
        }
    }
}