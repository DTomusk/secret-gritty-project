using Api.Shared.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.CalendarEvents.Controllers;

[Route("[controller]")]
[ApiController]
[EnableRateLimiting(RateLimitingConfiguration.AuthenticatedPolicy)]
public class CalendarEventsController : ControllerBase
{
}
