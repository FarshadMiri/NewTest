using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.Enitities
{
    public class Tbl_Location
    {
        [Key]
        public int LocationId { get; set; }
        public string Name { get; set; }

        // پراپرتی ناویگیشن به پرونده‌ها
        public ICollection<Tbl_Case> Cases { get; set; }
        public ICollection<Tbl_Task> Tasks { get; set; }
    }
}
