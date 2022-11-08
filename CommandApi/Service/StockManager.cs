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

        public bool IsStockAvailabelForCommand(Command commandId)
        {
            throw new NotImplementedException();
        }
    }
}