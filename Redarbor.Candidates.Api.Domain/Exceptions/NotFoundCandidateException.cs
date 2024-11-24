namespace Redarbor.Candidates.Api.Domain.Exceptions;

public class NotFoundCandidateException : Exception
{
    public NotFoundCandidateException(string message) : base(message)
    {
    }
}