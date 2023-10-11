namespace AppClients.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public TypeUser Role { get; set; }
    }
}

public enum TypeUser
{
    Admin = 0,
    Colaborador = 1
}