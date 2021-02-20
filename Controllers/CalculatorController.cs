using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Calculator.HttpRest.Services.Interfaces;
using Calculator.HttpRest.Models;
using System.Collections.Generic;

namespace Calculator.Controllers
{
    [Route("/api/[controller]")]
    public class CalculatorController : Controller
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;    
  
        }
        [HttpPost]
        public async Task<List<string>> calculate(string expression)
        {
            string IpAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            AccessLog user = new AccessLog();
            user.IpAddress = IpAddress;
            user.dateOfAccess = System.DateTime.Today;
            AccessLogController logData = new AccessLogController();
            string message=logData.LogUser(user);
            System.Diagnostics.Debug.WriteLine(message);
            List<string> response = new List<string>();
            string result = await _calculatorService.calculate(expression);
            response.Add(result);
            response.Add(message);
            return response;
        }
    }
}