using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MeshCollider))]
public class Building : MonoBehaviour
{
    public string label = "Unnamed Building";
    [TextArea] public string description = "Description Missing!";

    public Mesh[] levels;

    public int level = 1;
    public float value = 100;
    private InputAction attackAction;
    protected virtual void OnStart()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
        GameController.Instance.newDayEvent.AddListener(NewDay);
    }
    private void Start()
    {
        OnStart();
    }

    public virtual void Place()
    {
        GetComponent<Collider>().enabled = true;
    }

    public virtual void Upgrade()
    {
        if (GameController.Instance.money < value) return;

        GameController.Instance.RemoveMoney(value);

        level++;
        value = Mathf.Ceil(value * 1.4f);

        if (level >= levels.Length) return;
        GetComponent<MeshFilter>().mesh = levels[level];

        GetComponent<MeshCollider>().sharedMesh = null;
        GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
    }

    public virtual void Sell()
    {
        GameController.Instance.AddMoney(value);
        GameController.Instance.newDayEvent.RemoveListener(NewDay);
        Destroy(gameObject);
    }

    protected virtual void NewDay()
    {

    }
    private void OnMouseOver()
    {
        if (attackAction.WasPressedThisFrame()) CanvasController.Instance.InstantiateMenu("BuildingMenu").GetComponent<BuildingMenu>().building = this;
    }
}
