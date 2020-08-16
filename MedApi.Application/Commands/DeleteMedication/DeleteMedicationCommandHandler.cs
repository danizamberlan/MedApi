namespace MedApi.Application.Commands.DeleteMedication
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CrossCutting.Cqrs;
    using Ether.Outcomes;
    using Interfaces;

    public class DeleteMedicationCommandHandler :
        ICommandHandler<DeleteMedicationCommandParameters>
    {
        public DeleteMedicationCommandHandler(IMedicationRepository medicationRepository)
        {
            this.medicationRepository =
                medicationRepository ??
                    throw new NullReferenceException(nameof(medicationRepository));
        }

        private IMedicationRepository medicationRepository { get; }

        public async Task<IOutcome> Handle(
            DeleteMedicationCommandParameters request, 
            CancellationToken cancellationToken)
        {
            await medicationRepository.DeleteMedication(request.MedicationId);

            return Outcomes.Success();
        }
    }
}
