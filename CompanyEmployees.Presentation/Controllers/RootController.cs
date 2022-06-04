using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CompanyEmployees.Presentation
{
	[Route("api")]
	[ApiController]
	public class RootController
	{
		private readonly LinkGenerator _linkGenerator;
		public RootController(LinkGenerator linkGenerator) => _linkGenerator = linkGenerator;
	}
}

