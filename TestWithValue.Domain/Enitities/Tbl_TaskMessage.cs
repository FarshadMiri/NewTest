using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.Enitities
{
    public class Tbl_TaskMessage
    {
        [Key]
        public int Id { get; set; }
       
        [ForeignKey("Task")]
        public int TaskId { get; set; }
        public string SenderId { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public Tbl_Task Task { get; set; }
    }
}
