namespace CommandApi.Entity
{
    public class Client
    {
        public int Id { get; set; }
        public List<Commande> CommandesClient { get; internal set; }
    }
}