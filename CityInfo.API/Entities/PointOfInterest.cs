using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// pointofinterest entity for storing pointofinterest in the db 
namespace CityInfo.API.Entities
{
    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [ForeignKey("CityId")]
        public City? City { get; set; }
        public int CityId { get; set; }
        public PointOfInterest(string name)
        {
            Name = name;
        }
    }
}