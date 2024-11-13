using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Collections.Generic;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface.ActionMessage_s_Interface;
using TestWithValue.Domain.ActionMessages;

public class TempDataActionMessageService : IActionMessageService
{
    private readonly ITempDataDictionaryFactory _tempDataFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TempDataActionMessageService(ITempDataDictionaryFactory tempDataFactory, IHttpContextAccessor httpContextAccessor)
    {
        _tempDataFactory = tempDataFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    public void AddMessage(string message, string type = "info")
    {
        var tempData = _tempDataFactory.GetTempData(_httpContextAccessor.HttpContext);

        // دریافت لیست پیام‌ها از TempData و دی‌سریالایز کردن آن
        var messagesJson = tempData["ActionMessages"] as string;
        var messages = string.IsNullOrEmpty(messagesJson)
            ? new List<ActionMessage>()
            : JsonConvert.DeserializeObject<List<ActionMessage>>(messagesJson);

        // افزودن پیام جدید
        messages.Add(new ActionMessage(message, type));

        // سریالایز کردن لیست پیام‌ها و ذخیره آن در TempData
        tempData["ActionMessages"] = JsonConvert.SerializeObject(messages);
    }

    public IEnumerable<ActionMessage> GetMessages()
    {
        var tempData = _tempDataFactory.GetTempData(_httpContextAccessor.HttpContext);

        // دریافت پیام‌ها از TempData و دی‌سریالایز کردن آن
        var messagesJson = tempData["ActionMessages"] as string;
        return string.IsNullOrEmpty(messagesJson)
            ? new List<ActionMessage>()
            : JsonConvert.DeserializeObject<List<ActionMessage>>(messagesJson);
    }
}

