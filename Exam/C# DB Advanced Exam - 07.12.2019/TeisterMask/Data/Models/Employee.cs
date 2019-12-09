namespace TeisterMask.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Validations.DataValidator.Employee;

    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxUsernameLenght, MinimumLength = MinUsernameLenght)]
        [RegularExpression(UsernamePattern)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(PhonePattern)]
        public string Phone { get; set; }

        public ICollection<EmployeeTask> EmployeesTasks { get; set; } = new HashSet<EmployeeTask>();
    }
}
