using System.Text.Json.Serialization;

namespace AnimalRecognizer.DTOs
{
    public class PetDTO
    {
        public enum PetType 
        {
            Cat,
            Dog 
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public PetType Type { get; set; }
        public bool Sterilized { get; set; }
        public bool Passport { get; set; }
        public ImageDTO Image { get; set; }
        public ShelterDTO CurrentShelter { get; set; }

    }
}
