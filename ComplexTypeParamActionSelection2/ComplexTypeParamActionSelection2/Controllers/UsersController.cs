using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ComplexTypeParamActionSelection2.Controllers {

    public class User {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Bithdate { get; set; }
    }

    public class UsersByNameRequestCommand {

        public string Name { get; set; }
    }

    public class UsersByNameBirthDateRequestCommand : UsersByNameRequestCommand {

        public DateTime Birthdate { get; set; }
    }

    public class UsersController : ApiController {

        List<User> _users = new List<User> { 
            new User { Id = 1, Name = "Foo", Surname = "Bar", Bithdate = DateTime.Parse("1987-12-07") },
            new User { Id = 1, Name = "Foo", Surname = "BarFoo", Bithdate = DateTime.Parse("1989-01-07") },
            new User { Id = 2, Name = "Bar", Surname = "Foo", Bithdate = DateTime.Parse("1978-02-09") },
            new User { Id = 2, Name = "FooBar", Surname = "BarFoo", Bithdate = DateTime.Parse("1988-06-09") }
        };

        public IEnumerable<User> GetUsers() {

            return _users;
        }

        public IEnumerable<User> GetUsersByName([FromUri]UsersByNameRequestCommand cmd) {

            return _users.Where(x => x.Name.Equals(
                cmd.Name, 
                StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<User> GetUsersByNameAndBirthData([FromUri]UsersByNameBirthDateRequestCommand cmd) {

            return _users.Where(x => x.Name.Equals(
                cmd.Name,
                StringComparison.InvariantCultureIgnoreCase) 
                && x.Bithdate.Equals(cmd.Birthdate));
        }
    }
}