using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : ActionBase
{
    [Header("Move Offset")]
    [SerializeField] protected float MoveX;
    [SerializeField] protected float MoveY;

    [Header("Duration")]
    [SerializeField] protected float DurationX = 0.5f;
    [SerializeField] protected float DurationY = 0.5f;

    [Header("Ease Curve")]
    [SerializeField] protected AnimationCurve EaseX = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] protected AnimationCurve EaseY = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Ease Strength")]
    [SerializeField] protected float EaseStrengthX = 1f;
    [SerializeField] protected float EaseStrengthY = 1f;

    protected Coroutine moveRoutine;

    public override void Execute(IContext context)
    {
        TriggerContext triggerContext = context as TriggerContext;
        if (triggerContext == null) return;

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + new Vector3(MoveX, MoveY, 0f);

        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(MoveRoutine(startPos, targetPos));
    }

    protected IEnumerator MoveRoutine(Vector3 start, Vector3 target)
    {
        float timeX = 0f;
        float timeY = 0f;

        while (timeX < DurationX || timeY < DurationY)
        {
            if (timeX < DurationX)
                timeX += Time.deltaTime;

            if (timeY < DurationY)
                timeY += Time.deltaTime;

            float tX = DurationX > 0f ? Mathf.Clamp01(timeX / DurationX) : 1f;
            float tY = DurationY > 0f ? Mathf.Clamp01(timeY / DurationY) : 1f;

            float easedX = Mathf.Pow(EaseX.Evaluate(tX), EaseStrengthX);
            float easedY = Mathf.Pow(EaseY.Evaluate(tY), EaseStrengthY);

            float x = Mathf.Lerp(start.x, target.x, easedX);
            float y = Mathf.Lerp(start.y, target.y, easedY);

            transform.position = new Vector3(x, y, start.z);

            yield return null;
        }

        transform.position = target;
        moveRoutine = null;
    }
}

