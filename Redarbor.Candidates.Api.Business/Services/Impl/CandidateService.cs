using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Redarbor.Candidates.Api.Business.Commands.Interfaces;
using Redarbor.Candidates.Api.Business.Services.Interfaces;
using Redarbor.Candidates.Api.Domain.Commands.Create;
using Redarbor.Candidates.Api.Domain.Commands.Delete;
using Redarbor.Candidates.Api.Domain.Commands.Update;
using Redarbor.Candidates.Api.Domain.Dtos;
using Redarbor.Candidates.Api.Domain.Utils;
using Redarbor.Candidates.Api.Infrastructure.Repositories.Interfaces;
using Serilog;

namespace Redarbor.Candidates.Api.Business.Services.Impl
{
    public class CandidateService : ICandidateService
    {
        private readonly ICommandHandler<CreateCandidateCommand> _createCandidateCommandHandler;
        private readonly ICommandHandler<UpdateCandidateCommand> _updateCandidateCommandHandler;
        private readonly ICommandHandler<DeleteCandidateCommand> _deleteCandidateCommandHandler;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public CandidateService(
            ICommandHandler<CreateCandidateCommand> createCandidateCommandHandler,
            ICommandHandler<UpdateCandidateCommand> updateCandidateCommandHandler,
            ICommandHandler<DeleteCandidateCommand> deleteCandidateCommandHandler,
            IMapper mapper, ICandidateRepository candidateRepository)
        {
            _createCandidateCommandHandler = createCandidateCommandHandler;
            _updateCandidateCommandHandler = updateCandidateCommandHandler;
            _deleteCandidateCommandHandler = deleteCandidateCommandHandler;
            _mapper = mapper;
            _candidateRepository = candidateRepository;
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        public async Task CreateCandidateAsync(CreateCandidateCommand command)
        {
            await _createCandidateCommandHandler.Handle(command);
        }

        public async Task<IEnumerable<CandidateDto>> GetAllAsync()
        {
            var candidates = await _candidateRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CandidateDto>>(candidates);
        }

        public async Task<CandidateDto?> GetByIdAsync(int id)
        {
            var isExistsInCache =
                _memoryCache.TryGetValue(id,
                    out CandidateDto? candidateById);
            var cacheAliveTime = TimeSpan.FromSeconds(CandidateUtils.TimeToRefreshCacheInSeconds);
            if (isExistsInCache) return candidateById;
            candidateById = _mapper.Map<CandidateDto>(await _candidateRepository.GetByIdAsync(id));
            var cacheEntryPointsChannelRuleByMessage =
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(cacheAliveTime);
            _memoryCache.Set(id, candidateById,
                cacheEntryPointsChannelRuleByMessage);
            Log.Warning("Refreshed Cache to candidate {id}", id);

            return candidateById;
        }

        public async Task UpdateAsync(UpdateCandidateCommand command)
        {
            await _updateCandidateCommandHandler.Handle(command);
        }

        public async Task DeleteAsync(int id)
        {
            var command = new DeleteCandidateCommand { Id = id };
            await _deleteCandidateCommandHandler.Handle(command);
        }
    }
}