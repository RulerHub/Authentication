namespace Authentication.Core.DataTransferObjects.Authorize;

public class SessionDto
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    //public List<> Role { get; set; }
}
