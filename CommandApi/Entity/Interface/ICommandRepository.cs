namespace CommandApi.Entity.Interface
{
    public interface ICommandRepository
    {
        Command GetCommand(int commandeId);
    }
}
