using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions;

public class EmployeePatchDocBadRequestException : BadRequestException
{
    public EmployeePatchDocBadRequestException() : base("patchDoc object sent from client is null.")
    {
    }
}
