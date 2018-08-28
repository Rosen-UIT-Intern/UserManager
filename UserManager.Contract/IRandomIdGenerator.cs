namespace UserManager.Contract
{
    public interface IRandomIdGenerator
    {
        string GetId(int length);
    }
}
