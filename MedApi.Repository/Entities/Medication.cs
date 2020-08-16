namespace MedApi.Repository.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Medication")]
    public class Medication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MedicationId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public DateTime CreationDate { get; set; }

        public Domain.Medication ToDomainModel()
        {
            return new Domain.Medication
            {
                MedicationId = this.MedicationId,
                Name = this.Name,
                Quantity = this.Quantity,
                CreationDate = this.CreationDate
            };
        }
    }
}
