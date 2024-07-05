using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions;

public class EmployeeForUpdateBadRequestException : BadRequestException
{
    public EmployeeForUpdateBadRequestException() : base("EmployeeForUpdate object is null")
    {
    }
}
