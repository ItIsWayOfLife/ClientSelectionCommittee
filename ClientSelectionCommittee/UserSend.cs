using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientSelectionCommittee
{
    [Serializable]
    public class UserSend
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Patronymic { get; set; }
        public string AdminRights { get; set; }

      

    }
}
