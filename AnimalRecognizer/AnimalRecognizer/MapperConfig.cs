using AutoMapper;
using AnimalRecognizer.Model;
using AnimalRecognizer.DTOs;

namespace AnimalRecognizer
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Pet, PetDTO>();
            CreateMap<Contact, ContactDTO>();
            CreateMap<Image, ImageDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}