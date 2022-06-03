﻿using System;
using System.Dynamic;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service
{
	internal sealed class EmployeeService: IEmployeeService
	{
		private readonly IRepositoryManager _repository;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly IDataShaper<EmployeeDto> _dataShaper;
		private readonly IEmployeeLinks _employeeLinks;

		public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,  IEmployeeLinks employeeLinks)
		{
			_repository = repository;
			_logger = logger;
			_mapper = mapper;
			_employeeLinks = employeeLinks;
		}

        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges)
        {
			var company = await CheckIfCompanyExist(companyId, trackChanges);

			var employeeEntity = _mapper.Map<Employee>(employeeForCreation);
			_repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
			await _repository.SaveAsync();

			var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

			return employeeToReturn;
        }

        public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges)
        {
			var company = await CheckIfCompanyExist(companyId, trackChanges);

			var employeeForComany = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);

			_repository.Employee.DeleteEmployee(employeeForComany);
			await _repository.SaveAsync();
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
        {
			var company = await CheckIfCompanyExist(companyId, trackChanges);

			var employeeDb = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);

			var employee = _mapper.Map<EmployeeDto>(employeeDb);
			return employee;
		}

        public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges)
        {
			var company = await CheckIfCompanyExist(companyId, compTrackChanges);

			var employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges);

			var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);

			return (employeeToPatch, employeeEntity);
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetEmployeesAsync(Guid companyId, LinkParameters linkParameters, bool trackChanges)
        {
			if (!linkParameters.EmployeeParameters.ValidAgeRange)
				throw new MaxAgeRangeBadRequestException();

			var company = await CheckIfCompanyExist(companyId, trackChanges);

			var employeesWithMetaData = await _repository.Employee.GetEmployeesAsync(companyId, linkParameters.EmployeeParameters, trackChanges);
			var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);

			var links = _employeeLinks.TryGenerateLinks(employeesDto,linkParameters.EmployeeParameters.Fields,
			  companyId, linkParameters.Context);

			return (linkResponse: links, metaData: employeesWithMetaData.MetaData);
		}

        public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
        {
			_mapper.Map(employeeToPatch, employeeEntity);
			await _repository.SaveAsync();
        }

        public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges)
        {
			var company = await CheckIfCompanyExist(companyId, compTrackChanges);
			
			var employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges);

			_mapper.Map(employeeForUpdate, employeeEntity);
			await _repository.SaveAsync();
        }

		private async Task<Company> CheckIfCompanyExist(Guid companyId, bool trackChanges)
        {
			var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
			if (company is null)
				throw new CompanyNotFoundException(companyId);

			return company;
		}

		private async Task<Employee> GetEmployeeForCompanyAndCheckIfItExists(Guid companyId, Guid id, bool trackChanges)
        {
			var employee = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
			if (employee is null)
				throw new EmployeeNotFoundException(id);

			return employee;
		}

	}
}

