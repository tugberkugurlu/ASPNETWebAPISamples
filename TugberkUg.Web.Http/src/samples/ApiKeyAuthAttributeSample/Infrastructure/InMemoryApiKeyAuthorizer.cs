using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using TugberkUg.Web.Http;

namespace ApiKeyAuthAttributeSample.Infrastructure {

    public class InMemoryApiKeyAuthorizer : IApiKeyAuthorizer {

        private static IList<User> _validApiUsers = new List<User> { 

            new User { ApiKey = "d9c99318-53b6-4846-8613-e5aecb473066", Roles = new List<Role>() { new Role { Name = "Admin" } }},
            new User { ApiKey = "dd97a5aa-704e-4c9e-9bd5-5e2828392eee", Roles = new List<Role>() { new Role { Name = "Customer" } }},
            new User { ApiKey = "b2e684d7-8807-4232-b5fc-1a6e80c175c0", Roles = new List<Role>() { new Role { Name = "Admin" } }},
            new User { ApiKey = "36171dc0-4925-4b12-a162-0d6d193acb75" },
            new User { ApiKey = "c8028fae-4887-4e91-8fa5-9655adae6ec1" },
            new User { ApiKey = "c4bdb227-095a-4fde-8db5-1c96d86e897a" },
            new User { ApiKey = "ff10e537-44d5-49b3-add2-6011f54de996" },
            new User { ApiKey = "3dcd18cf-e373-4436-9171-aa7f20dae23c" },
            new User { ApiKey = "17b2663d-df81-4f63-b10e-5ed918a920cf" },
            new User { ApiKey = "44fffbf2-8b32-4c4c-834a-518dd0279efa" },
            new User { ApiKey = "b9c6b441-fe08-4bb9-960b-3c626054e50f" },
            new User { ApiKey = "1f79f1ca-b38b-4b0c-b2c7-52b316d22459" },
            new User { ApiKey = "966cb4cb-501f-49c4-be2c-fbd6bc54b4b2" },
            new User { ApiKey = "5cc8d944-1daf-44ae-87ed-5d5bc980ed4a" },
            new User { ApiKey = "a159f186-e75f-4cb8-bd9d-9b26a2b88343" },
            new User { ApiKey = "3135a252-7120-4274-93f1-cb44e819659d" },
            new User { ApiKey = "a9a77bc2-0f2a-49b5-a9e7-cbba1a6395e4" },
            new User { ApiKey = "03449aa9-f185-4932-8155-1242c5bc5d8b" },
            new User { ApiKey = "55a1f3f7-35a3-4b9f-a794-fea9ca3be0a6" },
            new User { ApiKey = "28e9fb16-da6a-41dc-adcf-a06c6eb9c2d4" }
        };

        public bool IsAuthorized(string apiKey) {

            return
                _validApiUsers.Any(x => x.ApiKey == apiKey);
        }

        public bool IsAuthorized(string apiKey, string[] roles) {

            if(_validApiUsers.Any(x => 
                x.ApiKey == apiKey && x.Roles != null && x.Roles.Any(r => roles.Contains(r.Name)))) {

                return true;
            }

            return false;
        }
    }
}