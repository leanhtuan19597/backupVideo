namespace NhiLeTeam.Models.Dto
{
    public class UserDTO
    {
        public int Id { get; set; }

        public IFormFile files { get; set; }
        public string Name { get; set; }
    }
}
