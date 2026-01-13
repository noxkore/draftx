using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAction : ActionBase
{
    [Header("Rotate Offset (Degrees)")]
    [SerializeField] protected float RotateZ;

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
        Quaternion start = transform.localRotation;
        Quaternion target = start * Quaternion.Euler(0f, 0f, RotateZ);

        PlayRotation(start, target);
    }

    protected void PlayRotation(Quaternion start, Quaternion target)
    {
        if (rotateRoutine != null)
            StopCoroutine(rotateRoutine);

        rotateRoutine = StartCoroutine(RotateRoutine(start, target));
    }

    protected IEnumerator RotateRoutine(Quaternion start, Quaternion target)
    {
        float time = 0f;

        while (time < Duration)
        {
            time += Time.deltaTime;
            float t = Duration > 0f ? Mathf.Clamp01(time / Duration) : 1f;

            float eased = Mathf.Pow(Ease.Evaluate(t), EaseStrength);
            transform.localRotation = Quaternion.Slerp(start, target, eased);

            yield return null;
        }

        transform.localRotation = target;
        rotateRoutine = null;
    }
}
