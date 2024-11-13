using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.ActionMessages;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface.ActionMessage_s_Interface
{
    public interface IActionMessageService
    {
        void AddMessage(string message, string type = "info");
        IEnumerable<ActionMessage> GetMessages();

    }
}
