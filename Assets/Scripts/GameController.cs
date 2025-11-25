using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool debug;
    [Header("Game Statistics")]
    public float money;
    public int citizens;

    [Header("References")]
    private GameObject newBuilding;
    public static GameController Instance { get; private set; }
    private InputAction debugAction;
    private InputAction attackAction;
    [HideInInspector] public UnityEvent newDayEvent;

    private float timer;
    private float dayInterval = 60;

    private void Awake()
    {
        Instance = this;
        newDayEvent = new();
    }

    private void Start()
    {
        debugAction = InputSystem.actions.FindAction("Debug");
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    private void Update()
    {
        if (debugAction.WasPressedThisFrame()) debug = !debug;

        timer += Time.deltaTime;
        if (timer > dayInterval)
        {
            timer = 0;
            newDayEvent.Invoke();
        }

        // Mouse Ray casting for building placing
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (newBuilding != null && Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.gameObject.layer != 3) return;

            newBuilding.transform.position = hitInfo.point;

            if (attackAction.WasPressedThisFrame())
            {
                newBuilding.GetComponent<Building>().Place();
                newBuilding = null;
            }
        }
    }

    public void AddMoney(float amount)
    {
        money += amount;
    }

    public void RemoveMoney(float amount)
    {
        money -= amount;
    }

    public void AddCitizens(int amount)
    {
        citizens += amount;
    }

    public void RemoveCitizens(int amount)
    {
        citizens -= amount;
    }

    public void BuildBuilding(GameObject building)
    {
        newBuilding = Instantiate(building);
    }

    private void OnGUI()
    {
        if (!debug) return;

        // Text
        GUI.Label(new Rect(10, 10, 100, 20), $"ms per frame: {System.Decimal.Round((decimal)(Time.deltaTime * 1000), 2)}");

        // Buttons
        if (GUI.Button(new Rect(10, 40, 100, 20), "Reload")) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (GUI.Button(new Rect(10, 70, 100, 20), "Exit")) Application.Quit(); ;
    }

    private void Reset()
    {
        transform.position = Vector3.zero;
        gameObject.tag = "GameController";
        gameObject.name = "--- GameController ---";
    }
}
