using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Core.DataTransferObjects.Responses;

public class ResponseDto<T>
{
    public T? Result { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}
