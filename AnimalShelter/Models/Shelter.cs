namespace AnimalShelter.Models
{
  public class Shelter
  {
    public int ShelterId { get; set; }
    [Required]
    public string Name { get; set; }
  }
}