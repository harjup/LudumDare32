public class MainManager : Singleton<MainManager>
{
    private DollarStore _dollarStore;
    private GuiManager _guiManager;


    void Start()
    {
        // Initialize
        _dollarStore = DollarStore.Instance;
        _guiManager = GuiManager.Instance;

        InitRound();
    }

    public void InitRound()
    {
        ResetDollarCount();

        _guiManager.InitRound();

        // Clean up any civilians
        // Spawn npcs into scene

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

    private void CleanUpCivilians()
    {


        // Find all civilians in scene
        // Remove them from scene
    }

    private void SpawnCivilians()
    {
        // Spawn 10 civilians
        // Within random x range
        // At specific y
    }




}
