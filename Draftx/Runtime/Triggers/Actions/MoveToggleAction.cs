using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToggleAction : MoveAction
{
    [Header("Open Offset")]
    [SerializeField] private float OpenMoveX;
    [SerializeField] private float OpenMoveY;

    [Header("Close Offset")]
    [SerializeField] private float CloseMoveX;
    [SerializeField] private float CloseMoveY;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen;

    private void Start()
    {
        closedPosition = transform.position;

        openPosition = closedPosition + new Vector3(
            OpenMoveX,
            OpenMoveY,
            0f
        );
    }

    public override void Execute(IContext context)
    {
        TriggerContext triggerContext = context as TriggerContext;
        if (triggerContext == null) return;

        isOpen = !isOpen;

        Vector3 startPos = transform.position;
        Vector3 targetPos = isOpen ? openPosition : closedPosition;

        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(
            MoveRoutine(startPos, targetPos)
        );
    }
}