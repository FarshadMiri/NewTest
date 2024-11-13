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
    public class TaskRepository : ITaskRepository
    {
        private readonly TestWithValueDbContext _context;

        public TaskRepository(TestWithValueDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tbl_Task>> GetTasksByDateAsync(DateOnly date)
        {
            try
            {
                // مقایسه تاریخ‌ها به صورت مستقیم بدون تبدیل به رشته
                var tasks = await _context.tbl_Tasks
                    .Where(t => t.TaskDate == date)  // مقایسه تاریخ به صورت DateOnly
                    .ToListAsync();

                if (tasks == null || !tasks.Any())
                {
                    // اگر تسک‌ها پیدا نشدند
                    throw new Exception("هیچ تسکی برای این تاریخ پیدا نشد.");
                }

                return tasks;
            }
            catch (Exception ex)
            {
                // در صورت بروز خطا، پیام خطا را لاگ کنید
                Console.WriteLine($"Error: {ex.Message}");

                // اگر خطا رخ داد، پیام خطا را برمی‌گردانیم
                return Enumerable.Empty<Tbl_Task>(); // برگرداندن یک مجموعه خالی در صورت بروز خطا
            }
        }


        public async Task AddTaskAsync(Tbl_Task task)
        {
            try
            {
                // بررسی اینکه تاریخ به درستی وارد شده باشد
                if (task.TaskDate != default)
                {
                    // تبدیل تاریخ به فرمت 'dd/MM/yyyy' قبل از ذخیره
                    string formattedDate = task.TaskDate.ToString("yyyy/MM/dd");

                    // اگر می‌خواهید تاریخ را به عنوان رشته ذخیره کنید (که توصیه نمی‌شود)
                    task.TaskDateString = formattedDate;  // فرض می‌کنیم که یک فیلد متنی به نام TaskDateString برای ذخیره تاریخ به صورت رشته داریم
                }
                else
                {
                    throw new ArgumentException("تاریخ تسک باید وارد شود.");
                }

                // اضافه کردن تسک به پایگاه داده
                await _context.tbl_Tasks.AddAsync(task);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // لاگ خطا در صورت بروز استثنا
                Console.WriteLine($"Error: {ex.Message}");
                // شما می‌توانید خطاها را مدیریت کنید و به کاربر اطلاعات مناسبی دهید
            }
        }

        public async Task UpdateTaskAsync(Tbl_Task task)
        {
            _context.tbl_Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
