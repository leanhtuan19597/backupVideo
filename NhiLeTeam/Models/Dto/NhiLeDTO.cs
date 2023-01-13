using System.ComponentModel.DataAnnotations;

namespace NhiLeTeam.Models.Dto
{
    public class NhiLeDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
