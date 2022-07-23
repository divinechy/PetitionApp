using Microsoft.AspNetCore.Mvc;
using Petition.App.Authentication;
using Petition.App.Models;
using Petition.App.Models.Dtos;
using System.Diagnostics;

namespace Petition.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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
            var petitionDetails = SessionPersister.Petitions;
            if (petitionDetails != null)
            {
                var causeUserModel = SessionPersister.causeUserDtos;
                if (causeUserModel != null)
                {
                    foreach (var item in petitionDetails)
                    {
                        item.TotalCount = causeUserModel.Where(c => c.PetitionId == item.PetitionId).Count();
                    }
                }
                return View(petitionDetails);
            }
            else
            {
                return View();
            }

        }
        public IActionResult AllPetitions()
        {
            string email = SessionPersister.LoggedInEmail;
            var users = SessionPersister.LoggedInUsers;
            if (users.Where(c => c.Email == email).FirstOrDefault().RoleName == "admin")
            {
                TempData["Email"] = email;
                var petitionDetails = SessionPersister.Petitions;
                if (petitionDetails != null)
                {
                    var causeUserModel = SessionPersister.causeUserDtos;
                    if (causeUserModel != null)
                    {
                        foreach (var item in petitionDetails)
                        {
                            item.TotalCount = causeUserModel.Where(c => c.PetitionId == item.PetitionId).Count();
                        }
                    }
                    return View(petitionDetails);
                }
                else
                    return View();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int petiionId)
        {
            string email = SessionPersister.LoggedInEmail;
            TempData["Email"] = email;
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Account");

            var petitionDetails = SessionPersister.Petitions;
            var model = petitionDetails?.Where(c => c.PetitionId == petiionId)?.FirstOrDefault();
            if (model != null)
            {
                var causeUserModel = SessionPersister.causeUserDtos;
                if (causeUserModel != null)
                {
                    model.SignedName = causeUserModel.Where(c => c.PetitionId == petiionId).Select(x => x.Email).ToList();
                    model.TotalCount = causeUserModel.Where(c => c.PetitionId == petiionId).Count();
                }
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ActionName("Details")]
        public async Task<IActionResult> DetailsPost(PetitionDto model)
        {
            var email = SessionPersister.LoggedInEmail;
            CauseUserDto causeUser = new()
            {
                PetitionId = model.PetitionId,
                Email = email
            };
            var checkIfUserHasSigned = SessionPersister.causeUserDtos?.Where(c => c.Email == email && c.PetitionId == model.PetitionId)?.FirstOrDefault();
            if (checkIfUserHasSigned != null)
            {
                ViewBag.ErrorMsg = "Petition Already Signed By you";
                return View(model);
            }
            var causeUserModel = SessionPersister.causeUserDtos;
            if (causeUserModel != null)
            {
                causeUserModel.Add(causeUser);
                SessionPersister.SetUserCause(causeUserModel);
            }
            else
            {
                causeUserModel = new();
                causeUserModel.Add(causeUser);
                SessionPersister.SetUserCause(causeUserModel);

            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            SessionPersister.setUserEmail("");
            TempData["Email"] = null;
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> PetitionDelete(int petitionId)
        {
            var petitionDetails = SessionPersister.Petitions;
            var getItem = petitionDetails.Where(c=>c.PetitionId == petitionId).FirstOrDefault();
            if(getItem != null)
            {
                petitionDetails.Remove(getItem);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}