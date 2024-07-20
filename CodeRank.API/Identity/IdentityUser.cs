using Microsoft.AspNetCore.Identity;

namespace CodeRank.API.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public Guid? InstructorId { get; set; }
        public Guid? StudentId { get; set; }
    }
}
