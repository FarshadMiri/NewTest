using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.ViewModels.Topic;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services
{
    public class TopicSevice : ITopicSevice
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;
        public TopicSevice(ITopicRepository topicRepository,IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
                
        }
        public async Task<IEnumerable<ShowTopicViewModel>> GetTopicByTestId(int testId, int totalScore)
        {
            var topics=await _topicRepository.GetTopicByTestId(testId, totalScore);
            var topicVM=_mapper.Map<IEnumerable<ShowTopicViewModel>>(topics);   
            return topicVM;
        }
    }
}
