using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHC.Portal.Models
{
    public class RegistSetting
    {
        public bool AgentExtendCodeIsVisible { get; set; }
        public bool BirthdayIsRequire { get; set; }
        public bool BirthdayIsVisible { get; set; }
        public bool EmailIsRequire { get; set; }
        public bool EmailIsVisible { get; set; }
        public bool IsReceiveEmailIsRequire { get; set; }
        public bool IsReceiveEmailIsVisible { get; set; }
        public bool IsReceiveSMSIsRequire { get; set; }
        public bool IsReceiveSMSIsVisible { get; set; }
        public bool PhoneIsRequire { get; set; }
        public bool PhoneIsVisible { get; set; }
        public bool QQIsRequire { get; set; }
        public bool QQIsVisible { get; set; }
        public bool TrueNameIsRequire { get; set; }
        public bool TrueNameIsVisible { get; set; }
    }
}
