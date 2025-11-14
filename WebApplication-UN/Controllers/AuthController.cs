using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using WebApplication_UN.Models;

namespace WebApplication_UN.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _userFilePath;

        public AuthController(IWebHostEnvironment env)
        {
            _env = env;
            _userFilePath = Path.Combine(_env.ContentRootPath, "Data", "users.json");
        }

        [HttpGet("/login")]
        public ContentResult LoginPage()
        {
            var html = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""utf-8"" />
    <title>Login</title>
</head>
<body>
    <h1>Login</h1>
    <form method=""post"" action=""/auth/login"">
        <label>
            Username:
            <input type=""text"" name=""Username"" />
        </label>
        <br />
        <label>
            Password:
            <input type=""password"" name=""Password"" />
        </label>
        <br />
        <button type=""submit"">Login</button>
    </form>
</body>
</html>";
            return new ContentResult
            {
                Content = html,
                ContentType = "text/html"
            };
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] LoginRequest request)
        {
            if (!System.IO.File.Exists(_userFilePath))
            {
                return Unauthorized("User store not found.");
            }

            var json = System.IO.File.ReadAllText(_userFilePath);
            var users = JsonSerializer.Deserialize<List<UserCredential>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<UserCredential>();

            var match = users.FirstOrDefault(u =>
                u.Username == request.Username && u.Password == request.Password);

            if (match == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new { message = "Login successful" });
        }
    }
}
