using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Petition.App.Web.Authentication;
using Petition.App.Web.Models;
using Petition.App.Web.Models.Dtos;
using Petition.App.Web.Services.IServices;
using System.Diagnostics;

namespace Petition.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPetitionService _petitionService;
        private readonly string _accessToken;

        public HomeController(ILogger<HomeController> logger, IPetitionService petitionService)
        {
            _logger = logger;
            _petitionService = petitionService;
            _accessToken = SessionPersister.LoggedInAccessToken;
        }

        public async Task<IActionResult> Index()
        {
            string email = SessionPersister.LoggedInEmail;
            if (string.IsNullOrEmpty(email))
            {
                TempData["Email"] = null;
            }
            else
            {
                TempData["Email"] = email;
            }
            var petitionDetails = await _petitionService.GetPetition(_accessToken);
            if (petitionDetails != null & petitionDetails?.statusCode == 200)
            {
                List<Value> resModel = JsonConvert.DeserializeObject<List<Value>>(Convert.ToString(petitionDetails.value));
                if (resModel != null & resModel.Count > 0)
                {
                    List<PetitionDto> petitionDtos = new();
                    foreach (var item in resModel)
                    {
                        petitionDtos.Add(new PetitionDto
                        {
                            CreatedBy = item.createdBy,
                            Description = item.details,
                            PetitionId = item.petitionId,
                            Title = item.name,
                            ImageUrl = (item.petitionId % 2 == 0) ? "/images/11.jpg" : "/images/13.jpg",
                            TotalCount = item.userPetitions.Count()
                        });

                    }
                    return View(petitionDtos);
                }
            }
            return View();

        }
        public async Task<IActionResult> AllPetitions()
        {
            string email = SessionPersister.LoggedInEmail;
            var users = SessionPersister.LoggedInUsers;
            if (users?.RoleName == "admin")
            {
                TempData["Email"] = email;
                var petitionDetails = await _petitionService.GetPetition(_accessToken);
                if (petitionDetails != null & petitionDetails.statusCode == 200)
                {
                    List<Value> resModel = JsonConvert.DeserializeObject<List<Value>>(Convert.ToString(petitionDetails.value));
                    if (resModel != null & resModel?.Count() > 0)
                    {
                        List<PetitionDto> petitionDtos = new();
                        foreach (var item in resModel)
                        {
                            petitionDtos.Add(new PetitionDto
                            {
                                CreatedBy = item.createdBy,
                                Description = item.details,
                                PetitionId = item.petitionId,
                                Title = item.name,
                                TotalCount = item.userPetitions.Count()
                            });

                        }
                        return View(petitionDtos);
                    }
                }
                else
                    return View();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int petiionId)
        {
            string email = SessionPersister.LoggedInEmail;
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Account");

            var petitionDetails = await _petitionService.GetPetitionByPetitionId(petiionId, _accessToken);
            if(petitionDetails != null & petitionDetails.statusCode == 200)
            {
                List<Value> model = JsonConvert.DeserializeObject<List<Value>>(Convert.ToString(petitionDetails.value));
                PetitionDto petitionDto = new();
                if (model[0] != null)
                {
                    petitionDto.PetitionId = model[0].petitionId;
                    petitionDto.Title = model[0].name;
                    petitionDto.Description = model[0].details;
                    petitionDto.ImageUrl = (model[0].petitionId % 2 == 0) ? "/images/11.jpg" : "/images/13.jpg";
                    petitionDto.SignedName = model[0].userPetitions?.Where(c => c.petitionId == petiionId)?.Select(x => x.signedEmail).ToList();
                    petitionDto.TotalCount = model[0].userPetitions.Count();
                    
                    return View(petitionDto);
                }
            }
            return View();
        }

        [HttpPost]
        [ActionName("Details")]
        public async Task<IActionResult> DetailsPost(PetitionDto model)
        {
            var email = SessionPersister.LoggedInEmail;
            var petitionModel = await _petitionService.GetPetitionByPetitionId(model.PetitionId, _accessToken);
            if (petitionModel != null & petitionModel.statusCode == 200)
            {
                List<Value> modelRes = JsonConvert.DeserializeObject<List<Value>>(Convert.ToString(petitionModel.value));
                var checkIfUserHasSigned = modelRes[0].userPetitions?.Where(c => c.signedEmail == email && c.petitionId == model.PetitionId)?.FirstOrDefault();
                if (checkIfUserHasSigned != null)
                {
                    ViewBag.ErrorMsg = "Petition Already Signed By you";
                    return View(model);
                }

                var signPetition = await _petitionService.SignPetition(new Web.Models.APIDtos.SignPetitionDTO
                {
                    PetitionId = model.PetitionId,
                    Remarks = model.SignedMsg,
                    SignedEmail = email,
                    SignedBy = SessionPersister.LoggedInUsers.UserId
                }, _accessToken);

                if (signPetition != null & signPetition.statusCode == 201)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            SessionPersister.setUserEmail("");
            SessionPersister.setUseAccessToken("");
            SessionPersister.SetAuthentication(null);
            TempData["Email"] = null;
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> PetitionDelete(int petitionId)
        {
            await _petitionService.DeletePetition(petitionId, _accessToken);
            return RedirectToAction(nameof(AllPetitions));
        }
    }
}