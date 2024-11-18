﻿using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect.Repositories
{
    public class ExamsRepository : IExamsRepository
    {
        private readonly ConnectionContext _context;

        public ExamsRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<List<Exams>> GetAll()
        {
            return await _context.Exams.OrderBy(s => s.nm_exam_file).ToListAsync();
        }

        public async Task<Exams> GetById(int id)
        {
            return await _context.Exams.FirstOrDefaultAsync(x => x.cd_exam == id);
        }

        public async Task<Exams> Add(Exams exam)
        {
            var examCreated = await _context.AddAsync(exam);
            await _context.SaveChangesAsync();
            return examCreated.Entity;
        }
        public async Task<Exams> Update(Exams exam)
        {
            var examUpdated = _context.Update(exam);
            await _context.SaveChangesAsync();
            return examUpdated.Entity;
        }

        public async Task Delete(Exams exam)
        {
            _context.Remove(exam);
            await _context.SaveChangesAsync();
        }
    }
}