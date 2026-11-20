using System;
using UnityEngine;

public class FoggyAppearance : MonoBehaviour
{

    [SerializeField] RuntimeAnimatorController worldA, worldB;

    Animator animator;

    private void Start()
    {
        animator.runtimeAnimatorController = worldA;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        RealityManager.OnShift += HandleShift;
    }

    private void OnDisable()
    {
        RealityManager.OnShift -= HandleShift;
    }

    void HandleShift(bool isWorldB)
    {
        if (isWorldB)
            animator.runtimeAnimatorController = worldB;
        else
            animator.runtimeAnimatorController = worldA;
    }
}
