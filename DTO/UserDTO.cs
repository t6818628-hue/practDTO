namespace p1_api.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Login { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}