using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Models.Tables.Interfaces;

namespace Web.Models.Tables
{
    public class Guardian : IGuardian
    {

        public int GuardianId { get; set; }

        [Required, Display(Name ="First name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage ="First letter must be capitalize")]
        [StringLength(50, ErrorMessage ="First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "First letter must be capitalize")]
        [StringLength(50, MinimumLength =1, ErrorMessage ="Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [Display(Name ="MI")]
        public string MiddleName { get; set; }

        public string Street { get; set; }

        [Required]
        public string Barangay { get; set; }

        [Required]
        public string Municipality { get; set; }

        [Required]
        public string Province { get; set; }

        [DisplayFormat(DataFormatString = "{0:####-###-####}", ApplyFormatInEditMode = true)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"(\d{4})(\d{3})(\d{4})", ErrorMessage = "Not a valid Phone number")]
        [Required, Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [ReadOnly(true)]
        public bool IsActive { get; set; }

        public bool IsSelected { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => LastName + ", " + FirstName;

        [Display(Name = "Address")]
        public string FullAddress => $"{Barangay} {Municipality} {Province}";

        public virtual StudentGuardian StudentGuardian { get; set; }
    }
}
