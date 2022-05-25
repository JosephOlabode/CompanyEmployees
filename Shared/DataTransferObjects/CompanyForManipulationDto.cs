using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
	public abstract record CompanyForManipulationDto
	{
        [Required(ErrorMessage = "Company name is a required field")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Company address is a required field")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
        public string? Address { get; init; }

        [Required(ErrorMessage = "Company country is a required field")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string? Country { get; init; }

        [Required(ErrorMessage = "Company country is a required field")]
        IEnumerable<EmployeeForCreationDto>? Employees { get; init; }
    }
}

