using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.Enitities
{
    public class Tbl_Answer
    {
        [Key]
        public int AnswerId { get; set; }
        public int AnswerValue { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Question")]

        public int QuestionId { get; set; }

        [ForeignKey("Option")]

        public int OptionId { get; set; }

        [ForeignKey("Test")]

        public int TestId { get; set; }

        public Tbl_User User { get; set; }
        public Tbl_Question Question { get; set; }
        public Tbl_Option Option { get; set; }
        public Tbl_Test Test { get; set; }


    }
}
