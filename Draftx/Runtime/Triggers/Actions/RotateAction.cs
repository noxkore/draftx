using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAction : ActionBase
{
    [Header("Rotate Offset (Degrees)")]
    [SerializeField] protected float RotateZ;

    [Header("Pivot (Local Space)")]
    [SerializeField] protected Vector2 Pivot;

    [Header("Duration")]
    [SerializeField] protected float Duration = 0.25f;

    [Header("Ease Curve")]
    [SerializeField]
    protected AnimationCurve Ease =
        AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Ease Strength")]
    [SerializeField] protected float EaseStrength = 1f;

    protected Coroutine rotateRoutine;

    public override void Execute(IContext context)
    {
        Quaternion startRot = transform.localRotation;
        Quaternion targetRot = startRot * Quaternion.Euler(0f, 0f, RotateZ);

        Vector3 startPos = transform.localPosition;

        PlayRotation(startRot, targetRot, startPos);
    }

    protected void PlayRotation(
        Quaternion startRot,
        Quaternion targetRot,
        Vector3 startPos
    )
    {
        if (rotateRoutine != null)
            StopCoroutine(rotateRoutine);

        rotateRoutine = StartCoroutine(
            RotateRoutine(startRot, targetRot, startPos)
        );
    }

    protected IEnumerator RotateRoutine(
        Quaternion startRot,
        Quaternion targetRot,
        Vector3 startPos
    )
    {
        float time = 0f;

        while (time < Duration)
        {
            time += Time.deltaTime;
            float t = Duration > 0f ? Mathf.Clamp01(time / Duration) : 1f;
            float eased = Mathf.Pow(Ease.Evaluate(t), EaseStrength);

            Quaternion currentRot = Quaternion.Slerp(startRot, targetRot, eased);

            Vector3 rotatedOffset =
                currentRot * new Vector3(-Pivot.x, -Pivot.y, 0f)
                - startRot * new Vector3(-Pivot.x, -Pivot.y, 0f);

            transform.localRotation = currentRot;
            transform.localPosition = startPos + rotatedOffset;

            yield return null;
        }

        transform.localRotation = targetRot;
        transform.localPosition =
            startPos +
            (
                targetRot * new Vector3(-Pivot.x, -Pivot.y, 0f)
                - startRot * new Vector3(-Pivot.x, -Pivot.y, 0f)
            );

        rotateRoutine = null;
    }
}
