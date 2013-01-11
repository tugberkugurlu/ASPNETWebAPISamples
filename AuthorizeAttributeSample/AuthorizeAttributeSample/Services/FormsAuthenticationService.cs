using System;
using System.Web;
using System.Web.Security;

namespace AuthorizeAttributeSample.Services {

    public class FormsAuthenticationService : IFormsAuthenticationService {

        public void SignIn(string userName, bool createPersistentCookie) {

            if (string.IsNullOrEmpty(userName)) 
                throw new ArgumentNullException("userName");

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        // ref: http://stackoverflow.com/questions/1822548/mvc-how-to-store-assign-roles-of-authenticated-users
        public void SignIn(string userName, bool createPersistentCookie, string[] roles) {

            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");

            HttpContext current = HttpContext.Current;
            HttpCookie authCookie = GetAuthCookie(userName, createPersistentCookie, roles);
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }

        public void SignOut() {
            
            FormsAuthentication.SignOut();
        }

        private HttpCookie GetAuthCookie(string userName, bool createPersistentCookie, string[] roles) {

            string strCookiePath = FormsAuthentication.FormsCookiePath;
            DateTime now = DateTime.Now;
            DateTime expiration = now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes);
            FormsAuthenticationTicket formsAuthenticationTicket =
                new FormsAuthenticationTicket(
                    2, 
                    userName, 
                    now.ToLocalTime(),
                    expiration.ToLocalTime(), 
                    createPersistentCookie, 
                    string.Join(",", roles),
                    strCookiePath);

            string text = FormsAuthentication.Encrypt(formsAuthenticationTicket);
            HttpCookie httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, text);
            httpCookie.HttpOnly = true;
            httpCookie.Path = strCookiePath;
            httpCookie.Secure = FormsAuthentication.RequireSSL;
            if (FormsAuthentication.CookieDomain != null) {

                httpCookie.Domain = FormsAuthentication.CookieDomain;
            }
            if (formsAuthenticationTicket.IsPersistent) {

                httpCookie.Expires = formsAuthenticationTicket.Expiration;
            }
            return httpCookie;
        }
    }
}