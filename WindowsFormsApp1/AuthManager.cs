using System;
using System.IO;
using System.Linq;

namespace WindowsFormsApp1
{
    public enum UserRole
    {
        Administrator,
        Manager,
        Employee,
        Guest
    }

    public class AuthManager
    {
        public bool Authenticate(string username, string password, string userFile = "user.txt")
        {
            if (!File.Exists(userFile))
                throw new FileNotFoundException($"Файл пользователей {userFile} не найден");

            var lines = File.ReadAllLines(userFile)
                .Where(line => line.Trim().StartsWith("#"))
                .Select(line => line.Trim().Substring(1).Trim());

            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2 && parts[0] == username && parts[1] == password)
                {
                    return true;
                }
            }
            return false;
        }

        public UserRole GetUserRole(string username)
        {
            switch (username.ToLower())
            {
                case "admin": return UserRole.Administrator;
                case "root": return UserRole.Manager;
                case "employee": return UserRole.Employee;
                default: return UserRole.Guest;
            }
        }
    }
}