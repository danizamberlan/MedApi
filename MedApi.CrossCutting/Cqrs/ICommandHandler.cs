namespace MedApi.CrossCutting.Cqrs
{
    using Ether.Outcomes;
    using MediatR;

    public interface ICommandHandler<in TRequest> : 
        IRequestHandler<TRequest, IOutcome> where TRequest : IRequest<IOutcome>
    {
    }
}
