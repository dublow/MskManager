using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MskManager.Common.Nancy.HandleError.Models
{
    public enum ServiceErrorCode
    {
        GeneralError = 0,
        NotFound = 10,
        InternalServerError = 20
    }
}
