using System;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployees.Presentation.Controllers
{
	public class AuthenticationController: ControllerBase
	{
		private readonly IServiceManager _service;
		public AuthenticationController(IServiceManager service) => _service = service;
	}
}

