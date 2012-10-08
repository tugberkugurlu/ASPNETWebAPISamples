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
                Password = "12345678" 
            }
        };

        public bool Authorize(string userName, string password) {

            var user = users.FirstOrDefault(x => x.UserName == userName);

            if (user == null)
                return false;

            return string.Equals(password, user.Password);
        }
    }
}