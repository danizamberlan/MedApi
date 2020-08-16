namespace MedApi.Application.Queries.GetMedications
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CrossCutting.Cqrs;
    using Ether.Outcomes;
    using Interfaces;

    public class GetMedicationsQueryHandler :
        IQueryHandler<
            GetMedicationsQueryParameters,
            GetMedicationQueryResult>
    {
        public GetMedicationsQueryHandler(IMedicationRepository medicationRepository)
        {
            this.medicationRepository =
                medicationRepository ??
                    throw new NullReferenceException(nameof(medicationRepository));
        }
        private IMedicationRepository medicationRepository { get; }
        
        public async Task<IOutcome<GetMedicationQueryResult>> Handle(
            GetMedicationsQueryParameters request, 
            CancellationToken cancellationToken)
        {
            var medications = await medicationRepository.GetAllMedications();
            var result = new GetMedicationQueryResult(medications);

            return Outcomes.Success(result);
        }
    }
}
