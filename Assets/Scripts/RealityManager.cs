using UnityEngine;
using UnityEngine.InputSystem;

public class RealityManager : MonoBehaviour
{
    public static event System.Action<bool> OnShift;

    InputAction shiftAction;
    bool isWorldB;

    private void Awake()
    {
        shiftAction = InputSystem.actions.FindAction("Shift");
    }

    void Update()
    {
        if (shiftAction.WasPressedThisFrame())
        {
            isWorldB = !isWorldB;
            OnShift?.Invoke(isWorldB);
        }
    }
}
