namespace MedApi.Domain
{
    using System;
    using Ether.Outcomes;

    public class Medication
    {
        public Medication()
        {

        }

        public static Medication CreateNewMedication(
            string name,
            int quantity)
        {
            return new Medication
            {
                Name = name,
                Quantity = quantity,
                CreationDate = DateTime.Now
            };
        }

        public Guid MedicationId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public DateTime CreationDate { get; set; }

        public IOutcome ValidateQuantity()
        {
            if (Quantity <= 0)
            {
                return Outcomes.Failure().WithMessage("Quantity must be greater than zero.");
            }

            return Outcomes.Success();
        }
    }
}
