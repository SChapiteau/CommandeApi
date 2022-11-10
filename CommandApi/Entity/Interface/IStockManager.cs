namespace CommandApi.Entity.Interface
{
    public interface ICommandStockManager
    {
        bool IsStockAvailableForCommand(Commande commandeId);

    }
}
