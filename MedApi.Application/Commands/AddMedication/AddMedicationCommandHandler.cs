namespace MedApi.Application.Commands.AddMedication
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CrossCutting.Cqrs;
    using Ether.Outcomes;
    using Interfaces;

    public class AddMedicationCommandHandler :
        ICommandHandler<AddMedicationCommandParameters>
    {
        public AddMedicationCommandHandler(IMedicationRepository medicationRepository)
        {
            this.medicationRepository =
                medicationRepository ??
                    throw new NullReferenceException(nameof(medicationRepository));
        }

        private IMedicationRepository medicationRepository { get; }

        public async Task<IOutcome> Handle(
            AddMedicationCommandParameters request, 
            CancellationToken cancellationToken)
        {
            var validation = request.Medication.ValidateQuantity();

            if (validation.Success)
            {
                await medicationRepository.AddMedication(request.Medication);

                return Outcomes.Success();
            }
            else
            {
                return validation;
            }
        }
    }
}
