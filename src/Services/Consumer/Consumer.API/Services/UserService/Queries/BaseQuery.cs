namespace Consumer.API.Services.UserService.Queries
{
    public class BaseQuery
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
