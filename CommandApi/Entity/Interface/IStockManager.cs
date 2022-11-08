namespace CommandApi.Entity.Interface
{
    public interface ICommandStockManager
    {
        bool IsStockAvailabelForCommand(Command commandId);

    }
}
