namespace MedApi.Application.Commands.AddMedication
{
    using CrossCutting.Cqrs;
    using Domain;

    public class AddMedicationCommandParameters : ICommand
    {
        public AddMedicationCommandParameters(Medication medication)
        {
            Medication = medication;
        }

        public Medication Medication { get; }
    }
}
