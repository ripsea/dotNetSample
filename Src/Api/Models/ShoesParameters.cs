namespace Api.Models
{
    public class ShoesParameters : QueryStringParameters
    {
        public ShoesParameters()
        {
            OrderBy = "Name desc";
        }

    }  
}
