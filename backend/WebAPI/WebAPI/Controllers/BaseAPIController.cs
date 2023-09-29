using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseAPIController : ControllerBase
{
    protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>() ??
                                    throw new InvalidOperationException();

    protected IMapper Mapper => HttpContext.RequestServices.GetRequiredService<IMapper>() ??
                                throw new InvalidOperationException();
}