namespace Consumer.API.Models.Dtos
{
    public class BaseDto
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
