public class DollarStore : Singleton<DollarStore>
{
    public int DollarCount { get; private set; }

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        DollarCount = 0;
    }

    public void AddDollars(int amount)
    {
        DollarCount += amount;
    }

}