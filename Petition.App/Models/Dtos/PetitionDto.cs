namespace Petition.App.Models.Dtos
{
    public class PetitionDto
    {
        public int PetitionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedBy { get; set; }
        public List<string> SignedName { get; set; }
        public string SignedMsg { get; set; }
        public int TotalCount { get; set; }
    }
}
