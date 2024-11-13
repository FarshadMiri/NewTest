using Microsoft.AspNetCore.Mvc;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface.ActionMessage_s_Interface;

namespace TestWithValue.Web.ViewComponents.ActionMessageViewComponent
{
    public class ActionMessageViewComponent : ViewComponent
    {
        private readonly IActionMessageService _actionMessageService;

        public ActionMessageViewComponent(IActionMessageService actionMessageService)
        {
            _actionMessageService = actionMessageService;
        }

        public IViewComponentResult Invoke()
        {
            var messages = _actionMessageService.GetMessages();
            return View("/Views/Shared/Components/ActionMessage/Default.cshtml",messages);
        }
    }
}
