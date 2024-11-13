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
    public class TopicRepository : ITopicRepository
    {
        private readonly TestWithValueDbContext _context;

        public TopicRepository(TestWithValueDbContext context)
        {
                _context = context;
        }
        public async Task<IEnumerable< Tbl_Topic>> GetTopicByTestId(int testId,int totalScore)
        {
            var topic=  _context.tbl_Topics.Where(t=>t.TestId == testId && t.Value >= totalScore).ToList();
            return topic;
           
            
        }
    }
}
