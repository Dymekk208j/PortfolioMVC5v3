﻿using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Repositories.Interfaces
{
    public interface IEmploymentHistoryRepository
    {
        Task<List<EmploymentHistory>> GetAllEmploymentsHistories();
        Task<EmploymentHistory> GetEmploymentHistory(int employmentHistoryId);
        Task<int> InsertEmploymentHistory(EmploymentHistory employmentHistory);
        Task<bool> UpdateEmploymentHistory(EmploymentHistory employmentHistory);
        Task<bool> RemoveEmploymentHistory(int employmentHistoryId);
    }
}
