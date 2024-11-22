using Microsoft.EntityFrameworkCore;
using Redarbor.Candidates.Api.Domain.Entities;
using Redarbor.Candidates.Api.Infrastructure.DbContext;
using Redarbor.Candidates.Api.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redarbor.Candidates.Api.Infrastructure.Repositories.Impl
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ApplicationDbContext _context;

        public CandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task<Candidate> GetByIdAsync(int id)
        {
            return await _context.Candidates
                .Include(c => c.CandidateExperiences)
                .FirstOrDefaultAsync(c => c.IdCandidate == id);
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            return await _context.Candidates
                .Include(c => c.CandidateExperiences)
                .ToListAsync();
        }

        public async Task UpdateAsync(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Candidate candidate)
        {
            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
        }
        public async Task AddCandidateExperienceAsync(CandidateExperience experience)
        {
            await _context.Experiences.AddAsync(experience);
            await _context.SaveChangesAsync();
        }
    }
}