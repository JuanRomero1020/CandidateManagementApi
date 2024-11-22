﻿using Redarbor.Candidates.Api.Business.Commands.Interfaces;
using Redarbor.Candidates.Api.Domain.Commands.Delete;
using Redarbor.Candidates.Api.Infrastructure.Repositories.Interfaces;

namespace Redarbor.Candidates.Api.Business.Commands.Handlers
{
    public class DeleteCandidateCommandHandler : ICommandHandler<DeleteCandidateCommand>
    {
        private readonly ICandidateRepository _candidateRepository;

        public DeleteCandidateCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task Handle(DeleteCandidateCommand command)
        {
        }
    }
}