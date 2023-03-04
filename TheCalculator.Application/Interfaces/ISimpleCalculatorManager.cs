using TheCalculator.Application.Models;

namespace TheCalculator.Application.Interfaces
{
    public interface ISimpleCalculatorManager
    {
        CalculationResponse Calcualte(CalculationRequest request);
    }
}