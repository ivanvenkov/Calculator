using FluentValidation;
using TheCalculator.Application.Models;

namespace TheCalculatorAPI.Application.Validators
{
    public class CalculationRequestValidator : AbstractValidator<CalculationRequest>
    {
        public CalculationRequestValidator()
        {
            RuleFor(c => c.Request).NotEmpty();
            RuleFor(c => c.Request)
                .Matches("(?:(?:((?:(?:[ \\t]+))))|(?:((?:(?:\\/\\/.*?$))))|(?:((?:(?:(?<![\\d.])[0-9]+(?![\\d.])))))|(?:((?:(?:[0-9]+\\.(?:[0-9]+\\b)?|\\.[0-9]+))))|(?:((?:(?:(?:\\+)))))|(?:((?:(?:(?:\\-)))))|(?:((?:(?:(?:\\*)))))|(?:((?:(?:(?:\\/)))))|(?:((?:(?:(?:%)))))|(?:((?:(?:(?:\\()))))|(?:((?:(?:(?:\\)))))))");
        }
    }
}