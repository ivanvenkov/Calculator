using System.Data;
using System.Text.RegularExpressions;
using TheCalculator.Application.Models;

namespace TheCalculator.Application.Interfaces
{
    public class SimpleCalculatorManager : ISimpleCalculatorManager
    {
        public CalculationResponse Calcualte(CalculationRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = request.Request;
            var trimmedInput = Regex.Replace(input, @"\s+", String.Empty);
            string exp = null;

            if (char.IsDigit(trimmedInput.Last()) && !trimmedInput.Contains('.'))
                exp = string.Format($"{trimmedInput}.0");

            var calculation = new DataTable().Compute(exp ?? trimmedInput, null);
            decimal.TryParse(calculation.ToString(), out var result);

            return new CalculationResponse { Result = decimal.Round(result, 2) };
        }
    }
}