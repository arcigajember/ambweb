using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Models.Tables.Interfaces;

namespace Web.Models.Tables
{
    public class Student : IStudent
    {

        public int StudentId { get; set; }

        public int SectionId { get; set; }

        [Display(Name = "Student Number"), Required]
        public string StudentNumber { get; set; }

        [Required, Display(Name = "First name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "First letter must be upper case")]
        [StringLength(50, ErrorMessage ="First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required, Display(Name="Last name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "First letter must be upper case")]
        [StringLength(50, MinimumLength = 1, ErrorMessage= "Last name cannot be longer than 50 characaters.")]
        public string LastName { get; set; }

        [Display(Name ="MI")]
        public string MiddleName { get; set; }

        [Required]
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

        [Required]
        public string Status { get; set; }

        [Required]
        public string Gender { get; set; }

        [ReadOnly(true)]
        public bool IsActive { get; set; }

        public string Uid { get; set; }

        public string FullName => $"{LastName}, {FirstName}";

        public string Address => $"{Barangay} {Municipality} {Province}";

        public virtual Room Room { get; set; }
        public virtual Section Section { get; set; }
        public virtual IEnumerable<AttendanceDetails> AttendanceDetails { get; set; }
    }
}
