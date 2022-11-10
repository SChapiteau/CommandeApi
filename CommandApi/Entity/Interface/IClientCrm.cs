namespace CommandApi.Entity.Interface
{
    public interface IClientCrm
    {

        public Client GetClient(int clientId);

        bool HasCommandInProgress(int clientId);
        
    }
}
