public class DollarStore : Singleton<DollarStore>
{
    public float DollarCount { get; private set; }

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        DollarCount = 0;
    }

    public void AddDollars(float amount)
    {
        DollarCount += amount;
    }

}