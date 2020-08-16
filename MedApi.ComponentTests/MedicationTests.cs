namespace MedApi.ComponentTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using API.Parameters;
    using FluentAssertions;
    using MedApi.Repository.Entities;
    using Newtonsoft.Json;
    using Xunit;

    public class MedicationTests : IClassFixture<MedicationTestFixture>
    {
        private readonly MedicationTestFixture fixture;

        public MedicationTests(MedicationTestFixture fixture)
        {
            this.fixture = fixture;
        }
        
        [Fact]
        public async Task Get_HasMedication_ReturnsMedication()
        {
            // Arrange
            var expected = new Medication()
            {
                Name = "TestMedication",
                Quantity = 100,
                CreationDate = DateTime.Now
            };

            fixture.DbContext.Add(expected);
            await fixture.DbContext.SaveChangesAsync();

            // Act
            var path = @"/api/medication";
            var response = await fixture.Client.GetAsync(path);

            // Assert
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Domain.Medication>>(content);

            result
                .Count
                .Should()
                .Be(1);

            result
                .First()
                .Should()
                .BeEquivalentTo(expected);

            // CleanUp
            fixture.DbContext.Remove(expected);
            await fixture.DbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task Get_WithoutMedications_ReturnsEmptyList()
        {   
            // Act
            var path = @"/api/medication";
            var response = await fixture.Client.GetAsync(path);

            // Assert
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Domain.Medication>>(content);

            result
                .Count
                .Should()
                .Be(0);
        }

        [Fact]
        public async Task Add_MedicationWithQuantity_Success()
        {
            // Arrange
            var medication = new MedicationInputModel()
            {
                Name = "TestMedication" + Guid.NewGuid(),
                Quantity = 100
            };

            // Act
            var path = @"/api/medication";
            var content = new StringContent(
                JsonConvert.SerializeObject(medication), Encoding.UTF8, "application/json");

            var response = await fixture.Client.PostAsync(path, content);

            // Assert
            response.EnsureSuccessStatusCode();

            var insertedMedication =
                fixture
                    .DbContext
                    .Medications
                    .FirstOrDefault(med => med.Name == medication.Name);

            insertedMedication
                .Should()
                .NotBeNull();

            insertedMedication
                .Should()
                .BeEquivalentTo(medication, opt => opt.ExcludingMissingMembers());

            // CleanUp
            fixture.DbContext.Remove(insertedMedication);
            await fixture.DbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task Add_MedicationWithZeroQuantity_BadRequest()
        {
            // Arrange
            var medication = new MedicationInputModel()
            {
                Name = "TestMedication" + Guid.NewGuid(),
                Quantity = 0
            };

            // Act
            var path = @"/api/medication";
            var content = new StringContent(
            JsonConvert.SerializeObject(medication), Encoding.UTF8, "application/json");

            var response = await fixture.Client.PostAsync(path, content);

            // Assert
            response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.BadRequest);

            var insertedMedication =
                fixture
                    .DbContext
                    .Medications
                    .FirstOrDefault(med => med.Name == medication.Name);

            insertedMedication
                .Should()
                .BeNull();
        }

        [Fact]
        public async Task Delete_ExistingMedication_Success()
        {
            var medication = new Medication()
            {
                Name = "TestMedication",
                Quantity = 100,
                CreationDate = DateTime.Now
            };

            fixture.DbContext.Add(medication);
            await fixture.DbContext.SaveChangesAsync();

            // Act
            var path = $@"/api/medication/{medication.MedicationId}";

            var response = await fixture.Client.DeleteAsync(path);

            // Assert
            response.EnsureSuccessStatusCode();

            var databaseMedication =
                fixture
                    .DbContext
                    .Medications
                    .FirstOrDefault(med => med.MedicationId == medication.MedicationId);

            databaseMedication
                .Should()
                .BeNull();
        }
    }
}
