using UnityEditor.Rendering.Canvas.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class Building : MonoBehaviour
{
    public string label = "Unnamed Building";
    [TextArea] public string description = "Description Missing!";

    public int level = 1;
    public float value = 100;
    private InputAction attackAction;
    protected virtual void OnStart()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
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
        level++;
        value = Mathf.Ceil(value * 1.4f);
    }

    public virtual void Sell()
    {
        GameController.instance.AddMoney(value);
        Destroy(gameObject);
    }

    private void OnMouseOver()
    {
        if (attackAction.WasPressedThisFrame()) CanvasController.instance.InstantiateMenu("BuildingMenu").GetComponent<BuildingMenu>().building = this;
    }
}
