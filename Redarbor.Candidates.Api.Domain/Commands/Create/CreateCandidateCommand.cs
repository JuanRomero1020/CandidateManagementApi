namespace Redarbor.Candidates.Api.Domain.Commands.Create
{
    public class CreateCandidateCommand : ICommand
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public List<CreateCandidateExperienceCommand> Experiences { get; set; }
    }
}