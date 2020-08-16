namespace MedApi.CrossCutting.Cqrs
{
    using Ether.Outcomes;
    using MediatR;

    public interface ICommand : IRequest<IOutcome>
    {
    }
}
