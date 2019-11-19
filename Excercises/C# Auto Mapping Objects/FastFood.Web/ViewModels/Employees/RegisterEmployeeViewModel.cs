using System.ComponentModel.DataAnnotations;

namespace FastFood.Web.ViewModels.Employees
{
    public class RegisterEmployeeViewModel
    {
        [Required]
        public int PositionId { get; set; }

        public string PositionName { get; set; }
    }
}
