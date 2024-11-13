using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.ActionMessages
{
    public class ActionMessage
    {
        public string Message { get; set; }
        public string Type { get; set; } // success, error, info, warning

        public ActionMessage(string message, string type)
        {
            Message = message;
            Type = type;
        }
    }

}
