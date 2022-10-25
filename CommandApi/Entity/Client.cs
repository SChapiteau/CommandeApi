namespace CommandApi.Entity
{
    public class Client
    {
        public int Id { get; set; }
        public List<Command> CommandesClient { get; internal set; }
    }
}