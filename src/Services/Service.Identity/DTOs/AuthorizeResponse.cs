namespace Service.Identity.DTOs
{
    public record AuthorizeResponse(string Token, UserInforResponse UserInfo, bool IsSuccess = true);
}
