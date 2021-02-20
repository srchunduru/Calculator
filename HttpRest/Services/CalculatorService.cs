using System.Threading.Tasks;
using Calculator.HttpRest.Services.Interfaces;
using System.Data;

namespace Calculator.HttpRest.Services
{
    public class CalculatorService : ICalculatorService {

        public async Task<string> calculate(string expression)
        {
            if (expression == null) {
                return "N/A";
            }

            string result = new DataTable().Compute(expression, "").ToString();

            result = result.Replace(",", ".");

            return result;
        }
    }
}