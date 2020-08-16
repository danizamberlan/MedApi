namespace MedApi.Application.Queries.GetMedications
{
    using System.Collections.Generic;
    using Domain;

    public class GetMedicationQueryResult
    {
        public GetMedicationQueryResult(IReadOnlyList<Medication> medications)
        {
            Medications = medications;
        }
        public IReadOnlyList<Medication> Medications { get; }
    }
}
