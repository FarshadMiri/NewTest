using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Data.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly TestWithValueDbContext _context;
        public AnswerRepository(TestWithValueDbContext context)
        {
            _context = context;
        }
        public async Task<Tbl_Answer> GetAnswerByQuestionId(int questionId)
        {
            var answer = await _context.tbl_Answers.FirstOrDefaultAsync(a => a.QuestionId == questionId);

            return answer;

        }

        public async Task<IEnumerable<Tbl_Answer>> GetAnswerByTestIdAndUserId(int testId, int UserId)
        {
            var answers=await _context.tbl_Answers.Where(a=>a.TestId==testId && a.UserId==UserId).ToListAsync(); 
            return answers;

        }

        public async Task<bool> SaveAnswer(Tbl_Answer answer)
        {
            try
            {
                await _context.AddAsync(answer);
                _context.SaveChanges();
                return true;


            }
            catch (Exception ex)
            {
                var inner = ex.InnerException.Message;
                return false;
            }
        }

        public async Task<bool> UpdateAnswer(Tbl_Answer answer)
        {
            try
            {
                // بررسی اینکه آیا این موجودیت از قبل در حافظه محلی کانتکست ردیابی می‌شود یا خیر
                var local = _context.tbl_Answers
                    .Local
                    .FirstOrDefault(entry => entry.AnswerId == answer.AnswerId);

                if (local != null)
                {
                    // جدا کردن موجودیت محلی (در حافظه) از کانتکست
                    _context.Entry(local).State = EntityState.Detached;
                }

                // حالا می‌توانیم موجودیت جدید را آپدیت کنیم
                _context.tbl_Answers.Update(answer);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                var stackTrace = ex.StackTrace;
                return false;
            }
        }
    }
}
