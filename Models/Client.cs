namespace AppClients.Models
{
    public class Client
    {
        public int Id { get; set; }
        public int IdColaborador { get; set; }
        public string Name { get; set; }
        public string? Obs { get; set; }
    }
}
