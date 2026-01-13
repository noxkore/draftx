using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AreaInputTrigger : MonoBehaviour, ITrigger
{
    public event Action<IContext> OnTriggered;

    [SerializeField] private KeyCode key = KeyCode.E;
    [SerializeField] private string requiredTag = "Player";

    private bool isInside = false;
    private Collider2D currentCollider;

    private void Update()
    {
        if (!isInside)
            return;

        if (Input.GetKeyDown(key))
        {
            TriggerContext context = new TriggerContext(
                currentCollider.gameObject,
                currentCollider
            );

            OnTriggered?.Invoke(context);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!string.IsNullOrEmpty(requiredTag) &&
            !collision.CompareTag(requiredTag))
            return;

        isInside = true;
        currentCollider = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != currentCollider)
            return;

        isInside = false;
        currentCollider = null;
    }
}
