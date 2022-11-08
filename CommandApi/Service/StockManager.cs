using CommandApi.Entity;
using CommandApi.Entity.Interface;

namespace CommandApi.Service
{
    
    internal class StockManager : IStockManager
    {
        public StockManager()
        {
        }

        public bool IsStockAvailabelForCommand(Command commandId)
        {
            throw new NotImplementedException();
        }
    }
}