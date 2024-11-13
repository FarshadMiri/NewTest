using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.ViewModels.Option;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services
{
    public class OptionService : IOptionService
    {
        private readonly IOptionRepository _optionRepository;
        private readonly IMapper _mapper;
        public OptionService(IOptionRepository optionRepository,IMapper mapper)
        {
            _optionRepository = optionRepository;
            _mapper = mapper;
                
        }
        public async Task<IEnumerable<ShowOptionViewModel>> GetOptionByQuestionIdAndTestId(int questionId, int testId)
        {
            var options=await _optionRepository.GetOptionByQuestionIdAndTestId( questionId,testId);
            var optionVM=_mapper.Map<IEnumerable<ShowOptionViewModel>>(options);
            return optionVM;
        }
    }
}
