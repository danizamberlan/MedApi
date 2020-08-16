namespace MedApi.Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MedApi.Domain;

    public interface IMedicationRepository
    {
        Task<IReadOnlyList<Medication>> GetAllMedications();

        Task DeleteMedication(Guid medicationId);

        Task AddMedication(Medication medication);
    }
}
