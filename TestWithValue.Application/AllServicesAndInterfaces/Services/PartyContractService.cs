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
                             .Select(t => new DropdownItem { Value = t.TitleId.ToString(), Text = t.TitleName })
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
                    .ToList(),
            Lawyers= (await _userRepository.GetAllUsersAsync())
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
        var nameTitle = await _contractTitleRepository.GetContractTitleByIdAsync(model.TitleId);
        var partyOneName = await _userInfoRepository.GetUserInfoByUserIdAsync(model.PartyOneId);
        var partyTwoName = await _userInfoRepository.GetUserInfoByUserIdAsync(model.PartyTwoId);


        var contract = new Tbl_PartyContract
        {
            TitleName=nameTitle.TitleName,
            PartyOneName=partyOneName.FullName,
            PartyTwoName=partyTwoName.FullName,
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
    public async Task<ContractDetailsViewModel> GetContractDetailsAsync(int contractId)
    {
        var contract = await _partyContractRepository.GetContractWithClausesAsync(contractId);

        if (contract == null)
            return null; // یا یک ViewModel خالی

        // ساخت ViewModel برای نمایش داده‌ها
        var model = new ContractDetailsViewModel
        {
            ContractId = contract.ContractId,
            Title = contract.TitleName,
            PartyOneName = contract.PartyOneName,
            PartyTwoName = contract.PartyTwoName,
            ContractDate = contract.ContractDate,
            Clauses = contract.ContractClauseMappings.Select(mapping => new ContractClauseViewModel
            {
                ClauseText = mapping.Clause.ClauseText // فرض بر این است که در Tbl_ContractClause یک ویژگی برای متن بند داریم
            }).ToList()
        };

        return model;
    }
    public async Task<IEnumerable<ContractListViewModel>> GetAllContractsForIndexAsync()
    {
        var contracts = await _partyContractRepository.GetAllContractsAsync();

        // تبدیل داده‌ها به مدل مورد نیاز برای ویو
        return contracts.Select(contract => new ContractListViewModel
        {
            ContractId = contract.ContractId,
            Title = contract.TitleName,
            PartyOneName = contract.PartyOneName,
            PartyTwoName = contract.PartyTwoName,
            ContractDate = contract.ContractDate
        }).ToList();
    }
    public async Task<IEnumerable<ContractListViewModel>> GetContractsForUserAsync(string userId)
    {
        var contracts = await _partyContractRepository.GetContractsForUserAsync(userId);

        return contracts.Select(contract => new ContractListViewModel
        {
            ContractId = contract.ContractId,
            Title = contract.TitleName,
            PartyOneName = contract.PartyOneName,
            PartyTwoName = contract.PartyTwoName,
            ContractDate = contract.ContractDate,
            Status = contract.Status, // استفاده از ویژگی جدید
            UserStatus = contract.PartyOneId == userId ? contract.PartyOneStatus : contract.PartyTwoStatus // وضعیت خاص کاربر جاری
        }).ToList();
    }
    public async Task<ContractDetailsViewModel> GetContractDetailsForUserAsync(int contractId, string userId)
    {
        var contract = await _partyContractRepository.GetContractByIdAsync(contractId);

        if (contract == null || (contract.PartyOneId != userId && contract.PartyTwoId != userId))
            return null;

        return new ContractDetailsViewModel
        {
            ContractId = contract.ContractId,
            Title = contract.TitleName,
            PartyOneName = contract.PartyOneName,
            PartyTwoName = contract.PartyTwoName,
            ContractDate = contract.ContractDate,
            Clauses = contract.ContractClauseMappings.Select(mapping => new ContractClauseViewModel
            {
                ClauseText = mapping.Clause.ClauseText
            }).ToList(),
            UserStatus = contract.PartyOneId == userId ? contract.PartyOneStatus : contract.PartyTwoStatus // استفاده از ویژگی جدید
        };
    }
    public async Task<bool> ApproveOrRejectContractAsync(int contractId, string userId, string action)
    {
        var contract = await _partyContractRepository.GetContractByIdAsync(contractId);

        if (contract == null || (contract.PartyOneId != userId && contract.PartyTwoId != userId))
            return false;

        if (contract.PartyOneId == userId)
            contract.PartyOneStatus = action == "approve" ? "Approved" : "Rejected";
        else if (contract.PartyTwoId == userId)
            contract.PartyTwoStatus = action == "approve" ? "Approved" : "Rejected";

        if (contract.PartyOneStatus == "Approved" && contract.PartyTwoStatus == "Approved")
            contract.Status = "Approved";
        else if (contract.PartyOneStatus == "Rejected" || contract.PartyTwoStatus == "Rejected")
            contract.Status = "Rejected";

        await _partyContractRepository.UpdateContractAsync(contract);
        return true;
    }





}
