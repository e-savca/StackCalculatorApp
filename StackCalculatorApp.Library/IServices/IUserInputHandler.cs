using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculatorApp.Library.IServices
{
    public interface IUserInputHandler
    {
        public string GetExpression();
    }
}
