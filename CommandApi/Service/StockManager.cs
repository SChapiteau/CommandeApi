using CommandApi.Entity;
using CommandApi.Entity.Interface;

namespace CommandApi.Service
{
    
    internal class StockManager : ICommandStockManager
    {
        public StockManager()
        {
            //use repository
        }

        public bool IsStockAvailableForCommand(Commande commandeId)
        {
            throw new NotImplementedException();
        }
    }
}