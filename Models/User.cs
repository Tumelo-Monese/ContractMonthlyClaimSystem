namespace ContractMonthlyClaimSystem.Models
{
    public enum UserRole
    {
        Lecturer,
        ProgrammeCoordinator,
        AcademicManager
    }

    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        public string Password { get; set; } = string.Empty;
    }
}


