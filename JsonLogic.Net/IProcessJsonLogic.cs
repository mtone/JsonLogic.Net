namespace JsonLogic.Net
{
    public interface IProcessJsonLogic 
    {
        object Apply(object rule, object data);
    }
}
