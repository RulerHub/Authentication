namespace Authentication.Core.DataTransferObjects.Responses;

public class ResponseDto<T>
{
    public T? Result { get; set; }
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
}
