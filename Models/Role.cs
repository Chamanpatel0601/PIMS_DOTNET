using System.ComponentModel.DataAnnotations;

namespace PIMS_DOTNET.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }


        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; } = null!;


        [MaxLength(255)]
        public string? Description { get; set; }


        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
