namespace MedApi.Application.Commands.DeleteMedication
{
    using System;
    using CrossCutting.Cqrs;

    public class DeleteMedicationCommandParameters : ICommand
    {
        public DeleteMedicationCommandParameters(Guid medicationId)
        {
            MedicationId = medicationId;
        }
        public Guid MedicationId { get; }
    }
}
