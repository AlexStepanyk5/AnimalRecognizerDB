namespace AnimalRecognizer.DTOs
{
    public class UserDTO
    {
        public enum UserType
        {
            User,
            Admin
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
    }
}
