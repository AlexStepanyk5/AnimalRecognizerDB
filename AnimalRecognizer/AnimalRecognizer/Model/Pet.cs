﻿namespace AnimalRecognizer.Model
{
    public class Pet
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
        public string Breed { get; set; }
        public bool Sterilized { get; set; }
        public bool Passport { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
        public int CurrentShelterId { get; set; }
        public Shelter CurrentShelter { get; set; }

    }
}
