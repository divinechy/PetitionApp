using System;
using System.Linq;
using PetitionApi.DTOs;
using PetitionApi.Interfaces;
using PetitionApi.Models;

namespace PetitionApi.Repositories
{
    public class PetitionRepository : IPetitionRepository
    {
        private readonly PetitionDbContext _context;

        public PetitionRepository(PetitionDbContext context)
        {
            _context = context;
        }

        public object GetPetitions()
        {
            dynamic results;
           
                var query = (from petition in _context.Petitions
                             join user in _context.Users on petition.CreatedBy equals user.UserId
                             join uPetition in _context.UserPetitions on petition.PetitionId equals uPetition.PetitionId
                             into temp
                             from subPetition in temp.DefaultIfEmpty()
                             select new
                             {
                                 petition,
                                 user,
                                 subPetition
                             }).ToList();

                results = query.GroupBy(n => n.petition.PetitionId)
                             .Select(g => new {
                                 PetitionId = g.First().petition.PetitionId,
                                 Name = g.First().petition.Name,
                                 Details = g.First().petition.Details,
                                 CreatedDate = g.First().petition.CreatedDate,
                                 CreatedBy = g.First().user.FirstName + ' ' + g.First().user.LastName,
                                 CreatedByEmail = g.First().user.Email,
                                 UserPetitions = g.Where(c => c.subPetition != null).
                                             Select(p => p.subPetition).ToList()
                             }
                             ).ToList();


            
            return results;
        }


        public object GetPetitionsByPetitionId(int PetitionId)
        {
            dynamic results;
          
                var query = (from petition in _context.Petitions.Where(x => x.PetitionId == PetitionId)
                             join user in _context.Users on petition.CreatedBy equals user.UserId
                             join uPetition in _context.UserPetitions on petition.PetitionId equals uPetition.PetitionId
                             into temp
                             from subPetition in temp.DefaultIfEmpty()
                             select new
                             {
                                 petition,
                                 user,
                                 subPetition
                             }).ToList();

                results = query.GroupBy(n => n.petition.PetitionId)
                             .Select(g => new {
                                 PetitionId = g.First().petition.PetitionId,
                                 Name = g.First().petition.Name,
                                 Details = g.First().petition.Details,
                                 CreatedDate = g.First().petition.CreatedDate,
                                 CreatedBy = g.First().user.FirstName + ' ' + g.First().user.LastName,
                                 CreatedByEmail = g.First().user.Email,
                                 UserPetitions = g.Where(c => c.subPetition != null).
                                             Select(p => p.subPetition).ToList()
                             }
                             ).ToList();
            return results;
        }


        public object GetPetitionsByUserId(int UserId)
        {
            dynamic results;

            var query = (from petition in _context.Petitions.Where(x => x.CreatedBy == UserId)
                         join user in _context.Users on petition.CreatedBy equals user.UserId
                         join uPetition in _context.UserPetitions on petition.PetitionId equals uPetition.PetitionId
                         into temp
                         from subPetition in temp.DefaultIfEmpty()
                         select new
                         {
                             petition,
                             user,
                             subPetition
                         }).ToList();

            results = query.GroupBy(n => n.petition.PetitionId)
                         .Select(g => new {
                             PetitionId = g.First().petition.PetitionId,
                             Name = g.First().petition.Name,
                             Details = g.First().petition.Details,
                             CreatedDate = g.First().petition.CreatedDate,
                             CreatedBy = g.First().user.FirstName + ' ' + g.First().user.LastName,
                             CreatedByEmail = g.First().user.Email,
                             UserPetitions = g.Where(c => c.subPetition != null).
                                         Select(p => p.subPetition).ToList()
                         }
                         ).ToList();
            return results;
        }


        public Petition CreatePetition(PetitionDTO petitionDTO)
        {
            Petition petition = new Petition
            {
               CreatedBy = petitionDTO.CreatedBy,
               Name = petitionDTO.Name,
               Details = petitionDTO.Details
            };

            _context.Add(petition);

            _context.SaveChanges();

            return _context.Petitions.Last();
        }

        public UserPetition? SignPetition(SignPetitionDTO signDTO)
        {
            var checkIfSigned = _context.UserPetitions.Any(x => x.PetitionId == signDTO.PetitionId
                              && x.SignedEmail == signDTO.SignedEmail);

            if (checkIfSigned == true)
            {
                //user already signed
                return null;
            }
            else
            {
                UserPetition userPetition = new UserPetition
                {
                    SignedBy = signDTO.SignedBy,
                    SignedEmail = signDTO.SignedEmail,
                    Remarks = signDTO.Remarks,
                    PetitionId = signDTO.PetitionId
                };
                _context.Add(userPetition);

                _context.SaveChanges();

                return _context.UserPetitions.Last();
            }
           
        }

        public void DeletePetition(int PetitionId)
        {
            var petition = _context.Petitions.First(k => k.PetitionId == PetitionId);

            _context.Remove(petition);

            var signedPetitions = _context.UserPetitions.Where(g => g.PetitionId == PetitionId).ToList();

            _context.RemoveRange(signedPetitions);

            _context.SaveChanges();

            return;
        }
    }
}

