using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PetitionApi.DTOs;
using PetitionApi.Interfaces;
using PetitionApi.Models;

namespace PetitionApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserRepository _userRepo;

    public UserController(IUserRepository repo)
    {
        _userRepo = repo;
    }


    [HttpGet ("/api/[controller]/[action]")]
    public IResult GetUsers()
    {
      return Results.Ok(_userRepo.GetUsers());
    }

    [HttpPost("/api/[controller]/[action]")]
    public IResult Login([FromBody]UserLogin login)
    {
        if(!string.IsNullOrEmpty(login.Email) && !string.IsNullOrEmpty(login.Password))
        {
            LoginResponse response = new LoginResponse();
            var loggedUser = _userRepo.Login(login);

            if (loggedUser is null) return Results.NotFound("User Not found");

            var claims = new[]
           {
            new Claim(ClaimTypes.NameIdentifier, loggedUser.LastName),
            new Claim(ClaimTypes.Email, loggedUser.Email),
            new Claim(ClaimTypes.Role, loggedUser.RoleId.ToString())
          };

            var mybuilder = WebApplication.CreateBuilder();
            var token = new JwtSecurityToken
            (
                issuer: mybuilder.Configuration["Jwt:Issuer"],
                audience: mybuilder.Configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(mybuilder.Configuration["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            response.CreatedDate = loggedUser.CreatedDate;
            response.Email = loggedUser.Email;
            response.FirstName = loggedUser.FirstName;
            response.LastName = loggedUser.LastName;
            response.RoleId = loggedUser.RoleId;
            response.Token = tokenString;
            response.UserId = loggedUser.UserId;

            return Results.Ok(response);
        }

        return Results.BadRequest("Invalid Credentials");
    }


    [HttpPost("/api/[controller]/[action]")]
    public IResult CreateUser([FromBody] UserRegistration reg)
    {
        try
        {
            if (!string.IsNullOrEmpty(reg.Email) && !string.IsNullOrEmpty(reg.Password) && !string.IsNullOrEmpty(reg.LastName))
            {
                var createdUser = _userRepo.CreateUser(reg);

                if (createdUser is null) return Results.NotFound("User Not found");

                return Results.Created("User created successfully", createdUser);
            }

            return Results.BadRequest("Invalid Credentials");
        }
        catch (Exception e)
        {
            if(e.InnerException != null)
            {
                if (e.InnerException.Message.Contains("Violation of UNIQUE KEY"))
                {
                    return Results.Conflict("The Email you are trying to register with, is already in use");
                }
                return Results.Problem();
            }
            else
            {
                return Results.Problem();
            } 
            
        }
       
    }

    [HttpGet("/api/[controller]/[action]/{Email}")]
    public IResult GetUserByEmail(string Email)
    {
        if (!string.IsNullOrEmpty(Email))
        {
            var user = _userRepo.GetUserByEmail(Email);

            if (user is null) return Results.NotFound("User Not found");

            return Results.Ok(user);
        }

        return Results.BadRequest("Invalid Credentials");
    }
}

