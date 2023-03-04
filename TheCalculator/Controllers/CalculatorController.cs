using Microsoft.AspNetCore.Mvc;
using TheCalculator.Application.Interfaces;
using TheCalculator.Application.Models;

namespace TheCalculator.Controllers
{
    public class CalculatorController : BaseApiController
    {
        private readonly ISimpleCalculatorManager calculatorManager;

        public CalculatorController(ISimpleCalculatorManager manager) => (this.calculatorManager) = manager;

        [HttpPost("calculate")]
        public ActionResult<CalculationResponse> Calculate([FromBody] CalculationRequest request)
        {          
            var result = this.calculatorManager.Calcualte(request);
            return result;
        }
    }
}