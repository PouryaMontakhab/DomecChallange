using DomecChallange.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomecChallange.Dtos.Codes
{
    public class StatusDto
    {
        public string Message { get; set; }
        public dynamic ReturnId { get; set; }
        public dynamic ReturnModel { get; set; }
        public StatusEnum Status { get; set; }
    }
}
