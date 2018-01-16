using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBapp
{
    public class User
    {
        public long UserId { get; set; }

        public string UserKey { get; set; }

        public int UserTypeId { get; set; }

        public int Status { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserIdentity { get; set; }

        public DateTime Registered { get; set; }

        public string InternalKey { get; set; }
    }
}
