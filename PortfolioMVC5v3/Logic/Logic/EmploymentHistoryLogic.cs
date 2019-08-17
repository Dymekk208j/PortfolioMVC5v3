using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Logic.Logic
{
    public class EmploymentHistoryLogic : IEmploymentHistoryLogic
    {
        private readonly IEmploymentHistoryRepository _repository;

        public EmploymentHistoryLogic(IEmploymentHistoryRepository repository)
        {
            _repository = repository;
        }

        public Task<List<EmploymentHistory>> GetAllEmploymentsHistories()
        {
            return _repository.GetAllEmploymentsHistories();
        }

        public Task<EmploymentHistory> GetEmploymentHistory(int employmentHistoryId)
        {
            return _repository.GetEmploymentHistory(employmentHistoryId);
        }

        public Task<int> InsertEmploymentHistory(EmploymentHistory employmentHistory)
        {
            return _repository.InsertEmploymentHistory(employmentHistory);
        }

        public Task<bool> UpdateEmploymentHistory(EmploymentHistory employmentHistory)
        {
            return _repository.UpdateEmploymentHistory(employmentHistory);
        }

        public Task<bool> RemoveEmploymentHistory(int employmentHistoryId)
        {
            return _repository.RemoveEmploymentHistory(employmentHistoryId);
        }

        public Task<List<EmploymentHistory>> GetEmploymentHistoriesToShowInCvAsync()
        {
            return _repository.GetEmploymentHistoriesToShowInCvAsync();
        }
    }
}
