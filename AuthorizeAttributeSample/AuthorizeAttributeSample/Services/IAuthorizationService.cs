using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthorizeAttributeSample.Services {

    public interface IAuthorizationService {

        bool Authorize(string userName, string password);
    }
}