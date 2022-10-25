using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using CommandApi.Entity;

namespace CommandApi.DAL
{
    internal class CommandeRepository
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
            
        public Command GetCommand(int commandeId)
        {
            throw new NotImplementedException();
        }
    }
}