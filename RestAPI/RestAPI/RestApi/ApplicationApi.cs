using System;
using System.Collections.Generic;
using System.IO;

namespace RestAPI
{
    public class ApplicationApi
    {
        private const string TEST_USER_PATH = @"Data/testuser.json";

        public  bool IsSortedAscending(List<Post> listPosts)
        {
            for (int i = 0; i < listPosts.Count - 1; i++)
            {
                if (listPosts[i].Id > listPosts[i + 1].Id)
                    return false;
            }
            return true;
        }

        public User GetUserById(int id, List<User> users)
        {
            foreach (var user in users)
            {
                if (user.Id == id)
                    return user;
            }
            throw new Exception("User is not found");
        }

        public string GetRandomUserJson(string title, string body)
        {
            return "{\"userId\": 1, \"id\": 1, \"title\": \"" + title + "\", \"body\": \"" + body + "\" }";
        }

        public void WriteUsersFile(User user)
        {
            var jsonString = DeserializeUtil.Serialize<User>(user);
            FileUtil.WriteFile(TEST_USER_PATH, jsonString);
        }

        public User GetPreviousUser()
        {
            var text = File.ReadAllText(TEST_USER_PATH);
            return DeserializeUtil.GetData<User>(text);
        }

        public void DeleteUserFile() => FileUtil.DeleteFile(TEST_USER_PATH);
    }
}
