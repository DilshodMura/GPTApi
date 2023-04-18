using Domain.Services;
using GPTApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GPTApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResponseController : Controller
    {
        private readonly IChatService _chatService;
        private readonly ILoginService _loginService;
        private readonly ITokenGenerator _tokenGenerator;

        public ResponseController(IChatService chatService, ILoginService loginService, ITokenGenerator tokenGenerator)
        {
            _chatService = chatService;
            _loginService = loginService;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<string>> Post(long userId, [FromBody] string messageText)
        {
            var response = await _chatService.GetResponse(userId, messageText);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
        {
            var user = await _loginService.AuthenticateUserAsync(request.Email, request.Password);

            if (user == null)
            {
                // Authentication failed
                return Unauthorized();
            }

            var accessToken = await _tokenGenerator.GenerateAccessTokenAsync(user);
            var refreshToken = await _tokenGenerator.GenerateRefreshTokenAsync(user);

            return Ok(new AuthResponse
            {
                UserId= user.Id,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
        [HttpGet("{userId}/topics")]
        public async Task<ActionResult<List<string>>> GetTopics(long userId)
        {
            var topics = await _chatService.GetTopics(userId);
            return Ok(topics);
        }

        [HttpPost("clear-topics/{userId}")]
        public async Task<IActionResult> ClearTopics(long userId)
        {
            try
            {
                await _chatService.ClearTopics(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, ex.Message);
            }
        }
    }
}