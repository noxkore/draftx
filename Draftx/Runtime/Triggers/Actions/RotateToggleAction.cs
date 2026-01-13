using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRotateAction : RotateAction
{
    [Header("Toggle Settings")]
    [SerializeField] private float ClosedAngle = 0f;
    [SerializeField] private float OpenAngle = 90f;

    private bool isOpen;

    public override void Execute(IContext context)
    {
        isOpen = !isOpen;

        Quaternion startRot = transform.localRotation;
        Quaternion targetRot = Quaternion.Euler(
            0f,
            0f,
            isOpen ? OpenAngle : ClosedAngle
        );

        Vector3 startPos = transform.localPosition;

        PlayRotation(startRot, targetRot, startPos);
    }
}