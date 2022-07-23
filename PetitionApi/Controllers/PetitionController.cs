using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetitionApi.DTOs;
using PetitionApi.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetitionApi.Controllers
{
    public class PetitionController : ControllerBase
    {
        private IPetitionRepository _repo;

        public PetitionController(IPetitionRepository repo)
        {
            _repo = repo;
        }

        //for roles, 1 is admin and 2 is user

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1, 2")]
        [HttpGet("/api/[controller]/[action]")]
        public IResult GetPetitions()
        {
            return Results.Ok(_repo.GetPetitions());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1, 2")]
        [HttpGet("/api/[controller]/[action]/{PetitionId}")]
        public IResult GetPetitionByPetitionId(int PetitionId)
        {
            if (!string.IsNullOrEmpty(PetitionId.ToString()))
            {
                var petition = _repo.GetPetitionsByPetitionId(PetitionId);

                if (petition is null) return Results.NotFound("Petition Not Found");

                return Results.Ok(petition);
            }

            return Results.BadRequest("Invalid Credentials");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1, 2")]
        [HttpGet("/api/[controller]/[action]/{UserId}")]
        public IResult GetPetitionByUserId(int UserId)
        {
            if (!string.IsNullOrEmpty(UserId.ToString()))
            {
                var petition = _repo.GetPetitionsByPetitionId(UserId);

                if (petition is null) return Results.NotFound("Petition Not Found");

                return Results.Ok(petition);
            }

            return Results.BadRequest("Invalid Credentials");
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1, 2")]
        [HttpPost("/api/[controller]/[action]")]
        public IResult CreatePetition([FromBody] PetitionDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.CreatedBy.ToString()) && !string.IsNullOrEmpty(dto.Name))
            {
                var createdPetiton = _repo.CreatePetition(dto);

                if (createdPetiton is null) return Results.NotFound("Petition Not Created");

                return Results.Created("Petiton created successfully", createdPetiton);
            }

            return Results.BadRequest("Invalid Credentials");
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1, 2")]
        [HttpPost("/api/[controller]/[action]")]
        public IResult SignPetition([FromBody] SignPetitionDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.SignedBy.ToString()) && !string.IsNullOrEmpty(dto.SignedEmail)
                        && !string.IsNullOrEmpty(dto.PetitionId.ToString()))
            {
                var signedPetiton = _repo.SignPetition(dto);

                if (signedPetiton is null) return Results.NotFound("You have already signed this petition");

                return Results.Created("Petiton signed successfully", signedPetiton);
            }

            return Results.BadRequest("Invalid Credentials");
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpDelete("/api/[controller]/[action]/{PetitionId}")]
        public IResult DeletePetition(int PetitionId)
        {
            if (!string.IsNullOrEmpty(PetitionId.ToString()))
            {
                _repo.DeletePetition(PetitionId);

                return Results.Ok("Petition Deleted successfully");
            }

            return Results.BadRequest("Invalid Credentials");
        }
    }
}

