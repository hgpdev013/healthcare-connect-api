﻿using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect.Repositories
{
    public class PacientRepository : IPacientRepository
    {
        private readonly ConnectionContext _context;

        public PacientRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<Pacients> GetByUserId(int userId)
        {
            var pacient = await _context.Pacients
                .Include(i => i.Users.userType).ThenInclude(i => i.permissions)
                .Include(i => i.Allergies)
                .Include(i => i.exams)
                .Include(i => i.prescriptions)
                .FirstOrDefaultAsync(x => x.cd_user == userId);

            return pacient;
        }

        public async Task<Pacients> Add(Pacients pacients)
        {
            var pacient = await _context.AddAsync(pacients);
            await _context.SaveChangesAsync();
            return pacient.Entity;
        }

        public async Task<Pacients> Update(Pacients pacients)
        {
            var updatedPacient = _context.Update(pacients);
            await _context.SaveChangesAsync();
            return updatedPacient.Entity;
        }
    }
}
