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
    [SerializeField] private Slider dayProgress;

    private GameObject currentMenu;

    public static CanvasController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        if (GetComponent<CanvasScaler>().uiScaleMode == CanvasScaler.ScaleMode.ConstantPixelSize)
            Debug.LogWarning($"{gameObject.name} is currently set to 'Constant Pixel Size', this is usually undesired!");
        if (FindAnyObjectByType<EventSystem>() == null)
            Debug.LogWarning("No Event System in Scene!");

        GameController.Instance.newDayEvent.AddListener(NewDay);
    }


    private void Update()
    {
        if (moneyDisplay != null) moneyDisplay.text = $"$ {GameController.Instance.money}";
        if (citizenDisplay != null) citizenDisplay.text = $"Citizens: {GameController.Instance.citizens}";
        if (dayProgress != null) dayProgress.value = Mathf.InverseLerp(0, GameController.Instance.dayInterval, GameController.Instance.timer);
    }

    /// <summary>
    /// Instantiate GameObject onto Canvas from ResourceFolder
    /// </summary>
    /// <param name="resourceName">Prefab path within "Resources/UI/"</param>
    public GameObject InstantiateMenu(string resourceName)
    {
        if (currentMenu != null) Destroy(currentMenu);
        currentMenu = Instantiate((GameObject)Resources.Load($"UI/{resourceName}"), transform);

        return currentMenu;
    }

    private void NewDay()
    {
        Destroy(Instantiate((GameObject)Resources.Load($"UI/New Day"), transform), 2);
    }

    private void Reset()
    {
        gameObject.name = "--- Canvas ---";
    }
}
