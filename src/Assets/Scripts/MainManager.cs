public class MainManager : Singleton<MainManager>
{
    private DollarStore _dollarStore;
    private GuiManager _guiManager;
    private CivilianManager _civilianManager;

    void Start()
    {
        // Initialize
        _dollarStore = DollarStore.Instance;
        _guiManager = GuiManager.Instance;
        _civilianManager = CivilianManager.Instance;

        InitRound();
    }

    public void InitRound()
    {
        ResetDollarCount();

        _guiManager.InitRound();

        // Clean up any civilians
        // Spawn npcs into scene
        _civilianManager.SpawnCivilians();


        // Lets' not care about this for now
        // Clean up any ducks
        // Spawn ducks into scene
    }

    public void EndRound()
    {
        _guiManager.RoundOver();
    }

    private void ResetDollarCount()
    {
        _dollarStore.Reset();   
    }
}
