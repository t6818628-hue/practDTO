namespace p1_api.DTO
{
    public class PatchUserDTO
    {
        public string? Login { get; set; }
        public string? PasswordHash { get; set; }
        public int? RoleId { get; set; }
        public bool? IsActive { get; set; }
    }
}