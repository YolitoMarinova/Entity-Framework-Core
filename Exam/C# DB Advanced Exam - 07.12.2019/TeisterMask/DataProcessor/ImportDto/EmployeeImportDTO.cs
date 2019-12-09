using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TeisterMask.DataProcessor.ImportDto
{
    using static Data.Models.Validations.DataValidator.Employee;

    public class EmployeeImportDTO
    {

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

        public int[] Tasks { get; set; }
    }
}
