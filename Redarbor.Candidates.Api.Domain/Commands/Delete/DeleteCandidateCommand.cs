namespace Redarbor.Candidates.Api.Domain.Commands.Delete;

public class DeleteCandidateCommand : ICommand
{
    public int Id { get; set; }
}