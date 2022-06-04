﻿using System;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployees.Presentation.Controllers
{
	[ApiVersion("2.0", Deprecated = true)]
	[Route("api/{v:apiversion}/companies")]
	[ApiController]
	public class CompaniesV2Controller: ControllerBase
	{
		private readonly IServiceManager _service;
		public CompaniesV2Controller(IServiceManager service) => _service = service;

		public async Task<IActionResult> GetCompanies()
        {
			var companies = await _service.CompanyService.GetAllCompaniesAsync(trackChanges: false);

			var companiesV2 = companies.Select(x => $"{x.Name} V2");
			return Ok(companiesV2);
		}
	}
}

