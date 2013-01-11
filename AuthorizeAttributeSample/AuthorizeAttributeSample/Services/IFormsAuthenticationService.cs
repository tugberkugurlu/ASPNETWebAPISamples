using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthorizeAttributeSample.Services {

    public interface IFormsAuthenticationService {

        void SignIn(string userName, bool createPersistentCookie);
        void SignIn(string userName, bool createPersistentCookie, string[] roles);
        void SignOut();
    }
}