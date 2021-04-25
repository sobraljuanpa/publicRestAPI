using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using WebAPI.DTOs;

namespace WebAPI.Filters
{
	public class ExceptionFilter : Attribute, IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{

			int statusCode = 500;

			ResponseDTO response = new ResponseDTO()
			{
				IsSuccess = false,
				ErrorMessage = context.Exception.Message
			};

			if (context.Exception is ArgumentException)
			{
				statusCode = 400;
			};
			if (context.Exception is NullReferenceException)
			{
				statusCode = 404;
			};
			if (context.Exception is UnauthorizedAccessException)
			{
				statusCode = 401;
			};

			context.Result = new ObjectResult(response)
			{
				StatusCode = statusCode
			};
		}
	}
}