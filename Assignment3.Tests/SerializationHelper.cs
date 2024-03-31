using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Assignment3.Tests
{
    // SerializationHelper class provides methods for serializing and deserializing lists of User objects
    public static class SerializationHelper
    {
        // SerializeUsers method serializes a list of User objects to a file
        public static void SerializeUsers(List<User> users, string fileName)
        {
            // Create a DataContractSerializer for serializing the list of users
            DataContractSerializer serializer = new DataContractSerializer(typeof(List<User>));
            // Open a file stream for writing
            using (FileStream stream = File.Create(fileName))
            {
                // Serialize the list of users to the file stream
                serializer.WriteObject(stream, users);
            }
        }

        // DeserializeUsers method deserializes a list of User objects from a file
        public static List<User> DeserializeUsers(string fileName)
        {
            // Create a DataContractSerializer for deserializing the list of users
            DataContractSerializer serializer = new DataContractSerializer(typeof(List<User>));
            // Open a file stream for reading
            using (FileStream stream = File.OpenRead(fileName))
            {
                // Deserialize the list of users from the file stream and return it
                return (List<User>)serializer.ReadObject(stream);
            }
        }
    }
}
