using Microsoft.EntityFrameworkCore;
using Redarbor.Candidates.Api.Domain.Entities;
using Redarbor.Candidates.Api.Infrastructure.DbContext;
using Redarbor.Candidates.Api.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Redarbor.Candidates.Api.Domain.Exceptions;
using Serilog;

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
            try
            {
                Log.Information("add candidate from repository.");
                await _context.Candidates.AddAsync(candidate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Log.Error(dbEx, "Error adding candidate.");
                throw new RepositoryException("An error occurred while adding the candidate to the database.", dbEx);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unknown error occurred while adding candidate.");
                throw new RepositoryException("An unknown error occurred while adding the candidate.", ex);
            }
        }


        public async Task<Candidate> GetByIdAsync(int id)
        {
            try
            {
                Log.Information("getting bd id");
                return await _context.Candidates
                    .Include(c => c.CandidateExperiences)
                    .FirstOrDefaultAsync(c => c.IdCandidate == id);
            }
            catch (DbUpdateException dbEx)
            {
                Log.Error(dbEx, "Error retrieving candidates by id.");
                throw new RepositoryException("An error occurred while retrieving candidates from the database.", dbEx);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unknown error occurred while retrieving all candidates.");
                throw new RepositoryException("An unknown error occurred while retrieving candidates.", ex);
            }
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            try
            {
                Log.Information("Getting all candidates from repository.");
                return await _context.Candidates
                    .Include(c => c.CandidateExperiences)
                    .ToListAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Log.Error(dbEx, "Error retrieving all candidates.");
                throw new RepositoryException("An error occurred while retrieving candidates from the database.", dbEx);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unknown error occurred while retrieving all candidates.");
                throw new RepositoryException("An unknown error occurred while retrieving candidates.", ex);
            }
        }


        public async Task UpdateAsync(Candidate candidate)
        {
            try
            {
                Log.Information("Updating candidate from repository.");
                _context.Candidates.Update(candidate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Log.Error(dbEx, "Error updating candidate.");
                throw new RepositoryException("An error occurred while updating the candidate in the database.", dbEx);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unknown error occurred while updating candidate.");
                throw new RepositoryException("An unknown error occurred while updating the candidate.", ex);
            }
        }


        public async Task DeleteAsync(Candidate candidate)
        {
            try
            {
                Log.Information("Deleting candidate from repository.");
                _context.Candidates.Remove(candidate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Log.Error(dbEx, "Error deleting candidate.");
                throw new RepositoryException("An error occurred while deleting the candidate from the database.",
                    dbEx);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unknown error occurred while deleting candidate.");
                throw new RepositoryException("An unknown error occurred while deleting the candidate.", ex);
            }
        }

        public async Task AddCandidateExperienceAsync(CandidateExperience experience)
        {
            try
            {
                await _context.Experiences.AddAsync(experience);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Log.Error(dbEx, "Error adding candidate experience.");
                throw new RepositoryException(
                    "An error occurred while adding the candidate experience to the database.", dbEx);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unknown error occurred while adding candidate experience.");
                throw new RepositoryException("An unknown error occurred while adding the candidate experience.", ex);
            }
        }
    }
}