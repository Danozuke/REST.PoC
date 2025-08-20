using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Models
{
    public class UserTokens
    {
        public string Token { get; set; }
        public string EMail { get; set; }
        public TimeSpan Validity { get; set; }
        public string RefreshToken { get; set; }
        public Guid GuidId { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
