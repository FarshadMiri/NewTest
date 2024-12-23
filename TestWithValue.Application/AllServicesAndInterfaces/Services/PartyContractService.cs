using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Contract;
using DropdownItem = TestWithValue.Domain.ViewModels.Contract.DropdownItem;

public class PartyContractService : IPartyContractService
{
    private readonly IPartyContractRepository _partyContractRepository;
    private readonly IContractTitleRepository _contractTitleRepository;
    private readonly IContractClauseRepository _contractClauseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IContractClauseMappingRepository _contractClauseMappingRepository;

    public PartyContractService(
        IPartyContractRepository partyContractRepository,
        IContractTitleRepository contractTitleRepository,
        IContractClauseRepository contractClauseRepository,
        IUserRepository userRepository,
        IUserInfoRepository userInfoRepository,
        IContractClauseMappingRepository contractClauseMappingRepository)
    {
        _partyContractRepository = partyContractRepository;
        _contractTitleRepository = contractTitleRepository;
        _contractClauseRepository = contractClauseRepository;
        _userRepository = userRepository;
        _userInfoRepository = userInfoRepository;
        _contractClauseMappingRepository = contractClauseMappingRepository;
    }

    // دریافت مدل برای ایجاد قرارداد
    public async Task<ContractCreateViewModel> GetContractCreateViewModelAsync()
    {
        var model = new ContractCreateViewModel
        {
            ContractTitles = (await _contractTitleRepository.GetAllContractTitlesAsync())
                             .Select(t => new TestWithValue.Domain.ViewModels.Contract.DropdownItem { Value = t.TitleId.ToString(), Text = t.TitleName })
                             .ToList(),
            ContractClauses = (await _contractClauseRepository.GetAllContractClausesAsync())
                              .Select(c => new DropdownItem { Value = c.ClauseId.ToString(), Text = c.ClauseText })
                              .ToList(),
            Users = (await _userRepository.GetAllUsersAsync())
                    .Join(await _userInfoRepository.GetAllUserInfosAsync(),
                          u => u.Id,
                          ui => ui.UserId,
                          (u, ui) => new { u.Id, ui.FullName })
                    .Select(x => new DropdownItem { Value = x.Id, Text = x.FullName })
                    .ToList()
        };

        return model;
    }

    // ایجاد قرارداد جدید
    public async Task CreateContractAsync(ContractCreateViewModel model)
    {
        var contract = new Tbl_PartyContract
        {
            TitleId = model.TitleId,
            PartyOneId = model.PartyOneId,
            PartyTwoId = model.PartyTwoId,
            ContractDate = DateTime.Now,
            Status = "Draft",
            PartyOneStatus = "Pending",
            PartyTwoStatus = "Pending",
            CreatedBy = "Agent", // شناسه پشتیبان که قرارداد را ایجاد کرده
            CreatedDate = DateTime.Now
        };

        // افزودن قرارداد از طریق Repository
        await _partyContractRepository.AddContractAsync(contract);

        // ایجاد ارتباط میان قرارداد و بندها از طریق Repository
        foreach (var clauseId in model.ClauseIds)
        {
            var mapping = new Tbl_ContractClauseMapping
            {
                ContractId = contract.ContractId,
                ClauseId = clauseId
            };

            await _contractClauseMappingRepository.AddAsync(mapping);
        }

        // ذخیره تغییرات در Repository
        await _contractClauseMappingRepository.SaveChangesAsync();
    }

    // دریافت قرارداد با شناسه
    public async Task<Tbl_PartyContract> GetContractByIdAsync(int contractId)
    {
        return await _partyContractRepository.GetContractByIdAsync(contractId);
    }

    // بروزرسانی قرارداد
    public async Task UpdateContractAsync(Tbl_PartyContract contract)
    {
        await _partyContractRepository.UpdateContractAsync(contract);
    }

    // حذف قرارداد
    public async Task DeleteContractAsync(int contractId)
    {
        await _partyContractRepository.DeleteContractAsync(contractId);
    }
}
