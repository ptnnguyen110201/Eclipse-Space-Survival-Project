using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : FuncManager
{
    private static InputManager instance;
    public static InputManager Instance => instance;

    [SerializeField] protected Vector3 movementInput;
    [SerializeField] protected FixedJoystick joystick;
    protected override void Awake()
    {
        if (InputManager.instance != null) Debug.LogError("Only 1 InputManager allow to exist");
        InputManager.instance = this;
        this.LoadJoystick();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadJoystick();
    }
    protected virtual void LoadJoystick()
    {
        if (this.joystick != null) return;
        this.joystick = GameObject.FindGameObjectWithTag("FixedJoystick")?.transform?.GetComponent<FixedJoystick>();
        Debug.Log(transform.name + " LoadJoystick ", gameObject);
    }
    private void Update()
    {
        this.SetMousePos();
    }

    private void SetMousePos()
    {
        if (this.joystick == null) return;

        this.movementInput.x = this.joystick.Horizontal;
        this.movementInput.y = this.joystick.Vertical;
        this.movementInput.z = 0;
    }

    public Vector3 GetMousePos() => this.movementInput;
   
}
