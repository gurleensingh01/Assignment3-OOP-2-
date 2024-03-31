using Assignment3;

namespace Assignment3.Tests
{
   
    [TestFixture]
    public class SerializationTests
    {
        // A list of User objects to be used in the tests
        private List<User> users = new List<User>();
        // File path for the serialized data
        private readonly string testFileName = @"..\..\test_users.bin";

        // Setup method to initialize test data before each test
        [SetUp]
        public void Setup()
        {
            // Populating the list with User objects
            users.Add(new User(1, "Joe Blow", "jblow@gmail.com", "password"));
            users.Add(new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef"));
            users.Add(new User(3, "Colonel Sanders", "chickenlover1890@gmail.com", "kfc5555"));
            users.Add(new User(4, "Ronald McDonald", "burgers4life63@outlook.com", "mcdonalds999"));
        }

        // TearDown method to clean up after each test
        [TearDown]
        public void TearDown()
        {
            // Clearing the list to reset state for next test
            this.users.Clear();
        }

        // Test for verifying the serialization process
        [Test]
        public void TestSerialization()
        {
            // Serialize the list of users to a file
            SerializationHelper.SerializeUsers(users, testFileName);
            // Assert that the file was created
            Assert.IsTrue(File.Exists(testFileName));
        }

        // Test for verifying the deserialization process
        [Test]
        public void TestDeSerialization()
        {
            // First serialize the users to ensure there is data to deserialize
            SerializationHelper.SerializeUsers(users, testFileName);
            // Deserialize the users from the file
            List<User> deserializedUsers = SerializationHelper.DeserializeUsers(testFileName);
            // Assert that the deserialized list has the same count as the original
            Assert.AreEqual(users.Count, deserializedUsers.Count);

            // Verify each user's details are correctly deserialized
            for (int i = 0; i < users.Count; i++)
            {
                Assert.AreEqual(users[i].Id, deserializedUsers[i].Id);
                Assert.AreEqual(users[i].Name, deserializedUsers[i].Name);
                Assert.AreEqual(users[i].Email, deserializedUsers[i].Email);
                Assert.AreEqual(users[i].Password, deserializedUsers[i].Password);
            }
        }
    }
}