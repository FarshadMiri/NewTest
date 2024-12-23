using System.Collections.Generic;

namespace TestWithValue.Domain.ViewModels.Contract
{
    public class ContractCreateViewModel
    {
        // انتخاب عنوان قرارداد
        public int TitleId { get; set; }

        // طرف اول قرارداد (کاربر یا وکیل)
        public string PartyOneId { get; set; }

        // طرف دوم قرارداد (کاربر یا وکیل)
        public string PartyTwoId { get; set; }

        // لیست بندهای انتخابی قرارداد
        public List<int> ClauseIds { get; set; }

        // لیست کاربران برای طرف اول و دوم (کاربران و وکیل‌ها)
        public List<DropdownItem> Users { get; set; }

        // لیست وکیل‌ها برای طرف دوم (فقط برای انتخاب وکیل در طرف دوم)
        public List<DropdownItem> Lawyers { get; set; }

        // لیست عناوین قرارداد برای دراپ‌داون
        public List<DropdownItem> ContractTitles { get; set; }

        // لیست بندهای قرارداد برای دراپ‌داون (چک‌باکس‌ها برای انتخاب بندها)
        public List<DropdownItem> ContractClauses { get; set; }
    }

    // فرض بر این است که DropdownItem چیزی شبیه به این باشد:
    public class DropdownItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
