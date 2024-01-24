using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Customer.Portal.Api.Controllers
{
    public class MathsDelegatesController : Controller
    {
        delegate int Operate(int number1, int number2);

        public MathsDelegatesController()
        { 
        }


        #region PUBLIC METHODS
        [HttpPost]
        [Route("AddTwoNumbers")]
        public IActionResult AddTwoNumbers(int Number1, int number2)
        {

                Operate  AddTwoNumbers= AddNumbers;
                return Ok(AddTwoNumbers(Number1, number2));
           

        }

        [HttpPost]
        [Route("MultiplyTwoNumbers")]
        public IActionResult MultiplyTwoNumbers(int Number1, int number2)
        {
            Operate MultiplyTwoNumbers = Multiply;
            return Ok(MultiplyTwoNumbers(Number1, number2));

        }


        [HttpPost]
        [Route("SubstractTwoNumbers")]
        public IActionResult SubstractTwoNumbers(int Number1, int number2)
        {
            Operate SubstractNumber = Substract;
            return Ok(Substract(Number1, number2));


        }
        #endregion

        #region PUBLIC METHODS

        private int AddNumbers (int Number1, int number2)
        {
            return Number1 + number2;
        }
        private int Substract(int Number1, int number2)
        {
            return Number1 - number2;
        }
        private int Multiply(int Number1, int number2)
        {
            return Number1 * number2;
        }

        #endregion
    }
}
