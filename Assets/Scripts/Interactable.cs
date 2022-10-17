using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    // set the transform of the interactable spot, by default its own transform
    public Transform interactionTransform;

    private bool isFocus = false;
    private Transform player;

    bool hasInteracted = false;

    public virtual void Interact()
    {
        // ToDo. This may not be what we want
        // maybe gives it a perioud of time to interact
        if (hasInteracted)
            return;

        hasInteracted = true;
        Debug.Log("Interacting with" + transform.name);
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void Start()
    {
        if (interactionTransform == null)
            interactionTransform = transform;
    }

    void Update()
    {
        if(isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius)
            {
                Interact();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
