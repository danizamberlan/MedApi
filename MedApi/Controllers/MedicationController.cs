namespace MedApi.API.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Commands.AddMedication;
    using Application.Commands.DeleteMedication;
    using Application.Queries.GetMedications;
    using Domain;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Parameters;

    [Route("api/medication")]
    [ApiController]
    public class MedicationController : ControllerBase
    {
        private readonly IMediator mediator;

        public MedicationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMedications()
        {
            var medications = await mediator.Send(new GetMedicationsQueryParameters());

            return Ok(medications.Value.Medications);
        }

        [HttpDelete]
        [Route("{medicationId}")]
        public async Task<IActionResult> DeleteMedication(Guid medicationId)
        {
            await mediator.Send(new DeleteMedicationCommandParameters(medicationId));

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddMedication(MedicationInputModel parameters)
        {
            var medication = Medication.CreateNewMedication(parameters.Name, parameters.Quantity);
            
            var result = await mediator.Send(new AddMedicationCommandParameters(medication));

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Messages.First());
            }

        }
    }
}