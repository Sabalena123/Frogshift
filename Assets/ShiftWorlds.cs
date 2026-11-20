using UnityEngine;

public class ShiftWorlds : MonoBehaviour
{
    [SerializeField] GameObject worldA, worldB;

    private void Start()
    {
        worldA.SetActive(true);
    }

    private void OnEnable()
    {
        RealityManager.OnShift += HandleShift;
    }

    private void OnDisable()
    {
        RealityManager.OnShift -= HandleShift;
    }

    private void HandleShift(bool isWorldB)
    {
        worldA.SetActive(!isWorldB);
        worldB.SetActive(isWorldB);
    }


}
