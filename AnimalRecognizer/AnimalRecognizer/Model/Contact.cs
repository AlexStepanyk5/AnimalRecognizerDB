﻿namespace AnimalRecognizer.Model
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public ICollection<Pet> Pets { get; set; }

    }
}