using System.Threading.Tasks;

namespace Calculator.HttpRest.Services.Interfaces
{
    public interface ICalculatorService
    {
        Task<string> calculate(string expression);
    }
}