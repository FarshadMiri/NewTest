using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.ViewModels.Option
{
    public class ShowOptionViewModel
    {
        public int OptionId { get; set; } // شناسه گزینه
        public string OptionText { get; set; } // متن گزینه
        public bool IsSelected { get; set; } // 
        public int Value {  get; set; } 



    }
}
