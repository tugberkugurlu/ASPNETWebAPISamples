using System;
using System.Web.Security;

namespace AuthorizeAttributeSample.Services {

    public class FormsAuthenticationService : IFormsAuthenticationService {

        public void SignIn(string userName, bool createPersistentCookie) {

            if (String.IsNullOrEmpty(userName)) 
                throw new ArgumentNullException("userName");

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut() {
            
            FormsAuthentication.SignOut();
        }
    }
}