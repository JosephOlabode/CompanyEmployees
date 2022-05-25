using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
	public record EmployeeForCreationDto {
		[Required(ErrorMessage = "Employee name is a required field")]
		[MaxLength(30, ErrorMessage = "Maximum length for name is 30 characters")]
		public string? Name { get; init; }

		[Required(ErrorMessage = "Age is a required field")]
		[Range(18, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 18")]
		public int Age { get; set; }

		[Required(ErrorMessage = "Position is a required field")]
		[MaxLength(20, ErrorMessage = "Maximum length of position is 20 characters")]
		public string? Position { get; init; }
	}
}

