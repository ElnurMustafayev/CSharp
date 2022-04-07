namespace Publisher.Models
{
    public class User
    {
        private static int IdCounter = 0;

        public readonly int Id;
        public readonly DateTime CreatedDate;
        public string? Username { get; set; }

        public User(string username)
        {
            this.Id = ++IdCounter;
            this.CreatedDate = DateTime.Now;

            this.Username = username;
        }
    }
}