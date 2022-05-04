using System;
using Contracts;

namespace Service
{
	internal sealed class CompanyService
	{
		private readonly IRepositoryManager _repository;
		private readonly ILoggerManager _logger;

		public CompanyService(IRepositoryManager repository, ILoggerManager logger)
		{
			_repository = repository;
			_logger = logger;
		}
	}
}

