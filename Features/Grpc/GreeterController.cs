using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using protoBuffer;

namespace ProductAPI.VSA.Features.Grpc
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreeterController : ControllerBase
    {
        private readonly Greeter.GreeterClient _greeterClient;

        public GreeterController(Greeter.GreeterClient greeterClient)
        {
            _greeterClient = greeterClient;
        }

        [HttpGet("sayhello")]
        public async Task<IActionResult> SayHello([FromQuery] string name)
        {
            var reply = await _greeterClient.SayHelloAsync(new HelloRequest { Name = name });
            return Ok(new { reply.Message });
        }
    }
}
