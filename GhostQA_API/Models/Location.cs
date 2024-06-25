using GhostQA_API.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GhostQA_API.Models
{
    public class Location : BaseModel
    {
        [Key]
        public int Id { get; set; }

        public string CountryName { get; set; }
        public int LocationId { get; set; }
    }
}