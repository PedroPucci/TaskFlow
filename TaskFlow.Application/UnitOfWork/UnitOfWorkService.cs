namespace TaskFlow.Application.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IRepositoryUoW _repositoryUoW;
        private AccountUserService accountService;
        private DepositService depositService;
        private BalanceService balanceService;

        public UnitOfWorkService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public AccountUserService AccountService
        {
            get
            {
                if (accountService is null)
                {
                    accountService = new AccountUserService(_repositoryUoW);
                }
                return accountService;
            }
        }

        public DepositService DepositService
        {
            get
            {
                if (depositService is null)
                {
                    depositService = new DepositService(_repositoryUoW);
                }
                return depositService;
            }
        }

        public BalanceService BalanceService
        {
            get
            {
                if (balanceService is null)
                {
                    balanceService = new BalanceService(_repositoryUoW);
                }
                return balanceService;
            }
        }
    }
}
