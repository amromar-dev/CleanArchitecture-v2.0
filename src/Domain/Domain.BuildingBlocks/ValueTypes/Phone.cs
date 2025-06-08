using CleanArchitectureTemplate.SharedKernels.Exceptions;
using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Domain.BuildingBlocks.ValueTypes
{
    public record Phone
    {
        private Phone()
        {
                
        }

        [JsonConstructor]
        public Phone(string CountryId, string Number)
        {
            if (string.IsNullOrWhiteSpace(CountryId))
                throw new FieldValidationException(nameof(CountryId));

            if (string.IsNullOrWhiteSpace(Number))
                throw new FieldValidationException(nameof(Number));

            //if (Number.IsMatchPhone() == false) // For Testing
            //    throw new FieldValidationException(nameof(Number), Localization.InvalidPhoneNumber);

            this.CountryId = CountryId;
            this.Number = Number;
        }

        public string CountryId { get; }

        public string Number { get; }

        public static Phone TryInitiate(string countryId, string number)
        {
            return string.IsNullOrWhiteSpace(countryId) || string.IsNullOrWhiteSpace(number) 
                ? null 
                : new Phone(countryId, number);
        }
    }
}
