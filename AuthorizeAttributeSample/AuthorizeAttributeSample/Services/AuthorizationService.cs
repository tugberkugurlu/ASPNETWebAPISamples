using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorizeAttributeSample.Models;

namespace AuthorizeAttributeSample.Services {

    public class AuthorizationService : IAuthorizationService {

        private static List<User> users = new List<User>() { 
            new User { 
                UserName = "tugberk", 
                Email = "tugberk@example.com", 
                Password = "12345678",
                Roles = new List<Role>() { 
                    new Role { Name = "Admin" },
                    new Role { Name = "Employee" },
                }
            }
        };

        public Tuple<bool, User> Authorize(string userName, string password) {

            var user = users.FirstOrDefault(x => x.UserName == userName);

            if (user == null)
                return new Tuple<bool, User>(false, null);

            return new Tuple<bool, User>(string.Equals(password, user.Password), user);
        }
    }
}