namespace Orders.BusinessLogic.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<string> UserRoles { get; set; } = new List<string>();
    }
}
