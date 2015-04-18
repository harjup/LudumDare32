using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FinalGuiSet : MonoBehaviourBase, IGuiSet
{
    private string _moneyTextTemplate;
    private Text _moneyText;
    private DollarStore _dollarStore;
    private MainManager _manager;

    public void Start()
    {
        _moneyText = GetComponentsInChildren<Text>().First(c => c.name.Contains("Dollar"));
        _moneyTextTemplate = _moneyText.text;
        _manager = MainManager.Instance;
        _dollarStore = DollarStore.Instance;
    }

    public void OnTryAgainClick()
    {
        _manager.InitRound();
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