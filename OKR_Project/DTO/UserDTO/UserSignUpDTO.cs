﻿namespace API.DTO.UserDTO
{
    public class UserSignUpDTO
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public Guid RoleId { get; set; }

        //public int TeamId { get; set; }
    }
}
