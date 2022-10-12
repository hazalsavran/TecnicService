using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceInfoController : Controller
    {
        readonly IServiceInfoService _serviceInfoService;

        public ServiceInfoController(IServiceInfoService serviceInfoService)
        {
            _serviceInfoService = serviceInfoService;
		}

		[HttpGet("getregisterpagedata")]
		public IActionResult GetRegisterPageData()
		{
			var result = _serviceInfoService.GetServiceRecordPageData();
            if (!result.Success)
            {
				return BadRequest(result);
			}

			return Ok(result);
		}

		[HttpPost("createservicerecord")]
		public IActionResult CreateServiceRecord([FromBody] ServiceRegisterDto serviceRegisterDto)
		{
			var result = _serviceInfoService.CreateServiceRecord(serviceRegisterDto);
            if (!result.Success)
            {
				return BadRequest(result);
			}

			return Ok(result);

		}



		//[HttpPost("getallbyservicestatus")]
		//public IActionResult GetAllByServiceStatus([FromQuery]int serviceStatus)
		//{
		//	//Swagger
		//	var result = _serviceInfoService.GetAllByServiceStatus(serviceStatus);
		//	if (result.Success)
		//	{
		//		return Ok(result);
		//	}
		//	return BadRequest(result);

		//}

		//[HttpGet("getallpendingassemblystatus")]
		//public IActionResult GetAllPendingAssemblyStatus()
		//{
		//	var result = _serviceInfoService.GetAllPendingAssemblyStatus();
		//	if (result.Success)
		//	{
		//		return Ok(result);
		//	}
		//	return BadRequest(result);

		//}

		[HttpPost("getbyidpendingassemblystatus")]
		public IActionResult GetByIdPendingAssemblyStatus([FromQuery] int serviceInfoId)
		{

			var result = _serviceInfoService.GetByIdPendingAssemblyStatus(serviceInfoId);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpPost("getbyidcompletedassemblystatus")]
		public IActionResult GetByIdCompletedAssemblyStatus([FromQuery] int serviceInfoId)
		{
			var result = _serviceInfoService.GetByIdCompletedAssemblyStatus(serviceInfoId);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpPost("getservicedetailbyserviceInfoId")]
		public IActionResult GetServiceDetailByServiceInfoId([FromQuery] int serviceInfoId)
		{
			var result = _serviceInfoService.GetServiceDetailByServiceInfoId(serviceInfoId);
			if (result.Success)
			{
				return Ok(result);
			}
            return BadRequest(result);
        }

		//[HttpGet("getallcompletedassemblystatus")]
		//public IActionResult GetAllCompletedAssemblyStatus(PaginationDto paginationDto)
		//{
		//	var result = _serviceInfoService.GetAllCompletedAssemblyStatus();
		//	if (result.Success)
		//	{
		//		return Ok(result);
		//	}
		//	return BadRequest(result);
		//}

		[HttpPost("GetAllPending(1)Assemblyinfo(2)&CompletedService(3)")]
		public IActionResult GetAllCompletedStatus(PaginationDto paginationDto,int statusID)
		{
            if (statusID==1)
            {
				var result = _serviceInfoService.GetAllPendingAssemblyStatus(paginationDto);
				if (result.Success)
				{
					return Ok(result);
				}
			}
            if (statusID==2)
            {
				var result = _serviceInfoService.GetAllCompletedAssemblyStatus(paginationDto);
				if (result.Success)
				{
					return Ok(result);
				}
			}
            if (statusID==3)
            {
				var result = _serviceInfoService.GetAllServiceCompletedStatus(paginationDto);
				if (result.Success)
				{
					return Ok(result);
				}
			}
			
			return BadRequest();
		}


		[HttpPost("completeassembly")]
		public IActionResult CompleteAssembly([FromBody] CompleteAssemblyDto completeAssemblyDto)
		{
			var result = _serviceInfoService.CompleteAssembly(completeAssemblyDto);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);

		}

		//[HttpGet("getallservicecompletedstatus")]
		//public IActionResult GetAllServiceCompletedStatus()
		//{
		//	var result = _serviceInfoService.GetAllServiceCompletedStatus();
		//	if (result.Success)
		//	{
		//		return Ok(result);
		//	}
		//	return BadRequest(result);

		//}
	
		[HttpPost("CompleteService")]
		public IActionResult CompleteService([FromBody] CompleteServiceDto completeServiceDto)
		{
			var result = _serviceInfoService.CompleteService(completeServiceDto);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpPost("ServiceInfoGetById")]
		public IActionResult GetById([FromQuery] int id)
		{
			var result = _serviceInfoService.GetById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}


		[HttpPost("ServicePreRegistration")]
		public IActionResult Add([FromBody] AddServiceinfoDto serviceInfo)
		{
			var result = _serviceInfoService.Add(serviceInfo);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpPost("ServiceUpdateById")]
		public IActionResult Update([FromBody] ServiceInfo serviceInfo)
		{
			var result = _serviceInfoService.Update(serviceInfo);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpPost("ServiceRemoveById")]
		public IActionResult Remove([FromQuery] int serviceInfoId)
		{
			var result = _serviceInfoService.Remove(serviceInfoId);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}
	}
}
