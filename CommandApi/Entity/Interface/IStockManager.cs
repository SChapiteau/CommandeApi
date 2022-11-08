namespace CommandApi.Entity.Interface
{
    public interface IStockManager
    {
        bool IsStockAvailabelForCommand(Command commandId);

    }
}
