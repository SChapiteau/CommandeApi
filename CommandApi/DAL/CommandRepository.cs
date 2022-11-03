using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using CommandApi.Entity;

namespace CommandApi.DAL
{
    internal class CommandRepository
    {
        private static CommandRepository _instance;
        public static CommandRepository Instance
        {
            get{
                if (_instance == null)
                    _instance = new CommandRepository();
                return _instance;
            }
        }
            
        public Command GetCommand(int commandeId)
        {
            throw new NotImplementedException();
        }
    }
}