﻿using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Repositories.Interfaces
{
    public interface IEducationRepository
    {
        Task<List<Education>> GetAllEducations();
        Task<Education> GetEducation(int educationId);
        Task<int> InsertEducation(Education education);
        Task<bool> UpdateEducation(Education education);
        Task<bool> RemoveEducation(int educationId);
        Task<List<Education>> GetEducationsToShowInCvAsync();
        Task<bool> ReorderEducationsPositionsInCv(int oldPositionEducationId, int newPositionEducationId);
        Task<bool> SetEducationShowInCvState(bool state, int educationId);
        Task<List<Education>> GetEducationsNotShowInCvAsync();
    }
}
