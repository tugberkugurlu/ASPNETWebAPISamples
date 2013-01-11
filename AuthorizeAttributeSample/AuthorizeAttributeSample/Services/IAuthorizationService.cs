using AuthorizeAttributeSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthorizeAttributeSample.Services {

    public interface IAuthorizationService {

        Tuple<bool, User> Authorize(string userName, string password);
    }
}