namespace DomecChallange.Dtos.UserDtos
{
    public class UserAuthDto
    {
        public string UserName { get; set; }
        public string BearerToken { get; set; }
        public List<UserCliam> Claims { get; set; } = new();
    }
}
