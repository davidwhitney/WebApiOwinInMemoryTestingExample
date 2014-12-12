namespace Api.ApplicationServices
{
    public class ValuesService : IGetValues
    {
        public int GetAValue(int id)
        {
            return id;
        }
    }
}