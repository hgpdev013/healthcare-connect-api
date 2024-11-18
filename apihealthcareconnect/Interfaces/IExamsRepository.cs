﻿using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IExamsRepository
    {
        Task<List<Exams>> GetAll();

        Task<Exams> GetById(int id);

        Task<Exams> Add(Exams exam);

        Task<Exams> Update(Exams exam);

        Task Delete(int id);
    }
}
