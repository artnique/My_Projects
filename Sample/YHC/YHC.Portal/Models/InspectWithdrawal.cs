using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    public class InspectWithdrawal
    {
        public string Message { get; set; }

        public bool AllowWithdrawal { get; set; }

        public decimal TransFee { get; set; }

        public IList<InspectWithdrawalItem> InspectList { get; set; }
    }
}