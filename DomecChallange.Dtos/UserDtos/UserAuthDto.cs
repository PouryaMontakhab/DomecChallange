namespace DomecChallange.Dtos.UserDtos
{
    public class UserAuthDto
    {
        public string UserName { get; set; }
        public string BearerToken { get; set; }
        public List<UserClaimDto> Claims { get; set; } = new();
    }
}
