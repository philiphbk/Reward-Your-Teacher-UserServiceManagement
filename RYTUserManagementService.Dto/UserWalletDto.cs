using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYTUserManagementService.Dto
{
    public class UserWalletDto
    {
        public int? BankId { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public string Currency { get; set; }
        public bool Status { get; set; }

    }
}
