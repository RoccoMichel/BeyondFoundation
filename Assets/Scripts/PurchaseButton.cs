using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseButton : MonoBehaviour
{
    public int price = 100;
    [SerializeField] private TMP_Text priceDisplay;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        priceDisplay.text = $"${price}";
    }

    private void Update()
    {
        button.interactable = GameController.Instance.money >= price;
    }
    public void Buy()
    {
        GameController.Instance.RemoveMoney(price);
    }
}
