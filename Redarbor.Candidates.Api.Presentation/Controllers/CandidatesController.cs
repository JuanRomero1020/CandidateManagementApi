﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Redarbor.Candidates.Api.Business.Services.Interfaces;
using Redarbor.Candidates.Api.Domain.Commands.Create;
using Redarbor.Candidates.Api.Domain.Commands.Update;
using Redarbor.Candidates.Api.Domain.Dtos;
using Redarbor.Candidates.Api.Presentation.Filters;

namespace Redarbor.Candidates.Api.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CandidateExceptionFilter))]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        private readonly IMapper _mapper;

        public CandidatesController(ICandidateService candidateService, IMapper mapper)
        {
            _candidateService = candidateService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCandidate([FromBody] CandidateDto candidateDto)
        {
            var createCandidateCommand = _mapper.Map<CreateCandidateCommand>(candidateDto);

            await _candidateService.CreateCandidateAsync(createCandidateCommand);

            return Ok("Candidate created successfully");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateDto>>> GetAll()
        {
            var candidates = await _candidateService.GetAllAsync();
            return Ok(candidates);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CandidateDto>> GetById(int id)
        {
            var candidate = await _candidateService.GetByIdAsync(id);

            return Ok(candidate);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] CandidateDto candidateDto)
        {
            var updateCandidateCommand = _mapper.Map<UpdateCandidateCommand>(candidateDto);
            await _candidateService.UpdateAsync(updateCandidateCommand);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _candidateService.DeleteAsync(id);
            return NoContent();
        }
    }
}