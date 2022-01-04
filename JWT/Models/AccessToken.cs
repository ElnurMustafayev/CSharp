using System.Security.Claims;

namespace Program.Models
{
    public class AccessToken
    {
        public string? Id { get; set; }
        public string? Role { get; set; }
        public string? nbf { get; set; }
        public string? exp { get; set; }
        public string? iat { get; set; }

        public AccessToken(IEnumerable<Claim> Claims)
        {
            this.Id = Claims.FirstOrDefault(c => c.Type == nameof(Id))?.Value;
            this.Role = Claims.FirstOrDefault(c => c.Type == nameof(Role))?.Value;
            this.nbf = Claims.FirstOrDefault(c => c.Type == nameof(nbf))?.Value;
            this.exp = Claims.FirstOrDefault(c => c.Type == nameof(exp))?.Value;
            this.iat = Claims.FirstOrDefault(c => c.Type == nameof(iat))?.Value;
        }

        public override string ToString()
        {
            var props = typeof(AccessToken).GetProperties();
            var infosCollection = props.Select(p => $"{p.Name}:\t {p.GetValue(this)}");

            return string.Join(Environment.NewLine, infosCollection);
        }
    }
}