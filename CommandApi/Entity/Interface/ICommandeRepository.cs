namespace CommandApi.Entity.Interface
{
    public interface ICommandeRepository
    {
        Commande GetCommande(int commandeId);
    }
}
