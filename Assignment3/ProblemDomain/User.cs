using System;
using System.Collections.Generic;

namespace Assignment3
{
    [Serializable]
    public class User
    {
        // Properties
        public int Id { get; set; } // Unique identifier for the user
        public string Name { get; set; } // Name of the user
        public string Email { get; set; } // Email address of the user
        public string Password; // Password of the user

        // Constructor
        public User(int id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

        // Retrieves the user's ID
        public int getId()
        {
            return Id;
        }

        // Retrieves the user's name
        public string getName()
        {
            return Name;
        }

        // Retrieves the user's email
        public string getEmail()
        {
            return Email;
        }

        // Checks if the passed password matches the user's password
        public bool isCorrectPassword(string password)
        {
            if (Password == null && password == null)
                return true;
            else if (Password == null || password == null)
                return false;
            else
                return Password.Equals(password);
        }

        // Checks if the current user object is equal to another object
        public bool equals(Object obj)
        {
            if (!(obj is User))
                return false;

            User other = (User)obj;

            // Compares the IDs, names, and emails of the two user objects
            return Id == other.Id && Name.Equals(other.Name) && Email.Equals(other.Email);
        }
    }
}
