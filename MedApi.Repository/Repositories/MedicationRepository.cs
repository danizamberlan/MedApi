namespace MedApi.Repository.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class MedicationRepository : IMedicationRepository
    {
        private readonly MedApiDbContext dbContext;

        public MedicationRepository(MedApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Medication>> GetAllMedications()
        {
            var medications = await dbContext.Medications.ToListAsync();

            return medications.Select(m => m.ToDomainModel()).ToList();
        }

        public async Task DeleteMedication(Guid medicationId)
        {
            var medication = await dbContext.Medications.FindAsync(medicationId);

            if (medication != null)
            {
                dbContext.Remove(medication);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AddMedication(Medication medication)
        {
            var entity = new Entities.Medication
            {
                Name = medication.Name,
                CreationDate = medication.CreationDate,
                Quantity = medication.Quantity
            };

            dbContext.Add(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
