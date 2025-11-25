using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class CanvasController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text moneyDisplay;
    [SerializeField] private TMP_Text citizenDisplay;
    public static CanvasController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (GetComponent<CanvasScaler>().uiScaleMode == CanvasScaler.ScaleMode.ConstantPixelSize)
            Debug.LogWarning($"{gameObject.name} is currently set to 'Constant Pixel Size', this is usually undesired!");
        if (FindAnyObjectByType<EventSystem>() == null)
            Debug.LogWarning("No Event System in Scene!");
    }


    private void Update()
    {
        if (moneyDisplay != null) moneyDisplay.text = $"$ {GameController.Instance.money}";
        if (citizenDisplay != null) citizenDisplay.text = $"Citizens: {GameController.Instance.citizens}";
    }

    /// <summary>
    /// Instantiate GameObject onto Canvas from ResourceFolder
    /// </summary>
    /// <param name="resourceName">Prefab path within "Resources/UI/"</param>
    public GameObject InstantiateMenu(string resourceName)
    {
        return Instantiate((GameObject)Resources.Load($"UI/{resourceName}"), transform);
    }

    private void Reset()
    {
        gameObject.name = "--- Canvas ---";
    }
}
