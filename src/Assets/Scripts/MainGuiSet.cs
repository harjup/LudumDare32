using UnityEngine.UI;

public interface IGuiSet
{
    void Disable();
    void Enable();
}

public class MainGuiSet : MonoBehaviourBase, IGuiSet
{
    private string _moneyTextTemplate;
    private Text _moneyText;
    private DollarStore _dollarStore;

    public void Start()
    {
        _moneyText = GetComponentInChildren<Text>();
        _moneyTextTemplate = _moneyText.text;
        _dollarStore = DollarStore.Instance;
    }

    public void Update()
    {
        _moneyText.text = string.Format(_moneyTextTemplate, _dollarStore.DollarCount);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
}