using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SimpleCharacterMovement : MonoBehaviour
{
    public bool movementEnabled = true;

    public Animator animator;
    public NavMeshAgent agent;
    public float inputHoldDelay = 0.5f;
    public float turnSpeedThreshold = 0.5f;
    public float speedDampTime = 0.1f;
    public float slowingSpeed = 0.175f;
    public float turnSmoothing = 15f;
    public Interactable focus;

    private Vector3 destinationPosition;
    private const float stopDistanceProportion = 0.1f;
    private const float navMeshSampleDistance = 4f;
    private readonly int hashSpeedPara = Animator.StringToHash("Speed");

    [Space]
    [Header("KeyboardMovement")]
    [SerializeField]
    private bool useKeyboard = true;

    [DrawIf("useKeyboard", true)]
    [SerializeField]
    private InputActionAsset InputActions;
    private InputActionMap PlayerActionMap;
    private InputAction Movement;
    private Vector3 MovementVector;

    [DrawIf("useKeyboard", true)]
    [Range(0f, 1f)]
    [SerializeField]
    private float stopSmoothing = 0.5f;

    // uses mouse or keyboard/gamepad
    private enum ControlType { Point, Controllers }
    private ControlType currentControl = ControlType.Point;


    private void Start()
    {
        if (useKeyboard)
        {
            PlayerActionMap = InputActions.FindActionMap("Player");
            Movement = PlayerActionMap.FindAction("Move");
            Movement.started += HandleMovementAction;
            Movement.canceled += HandleMovementAction; // for keyboard released
            Movement.performed += HandleMovementAction; // for gamepad
            Movement.Enable();
            PlayerActionMap.Enable();
            InputActions.Enable();
        }

        agent.updateRotation = false;

        agent.Warp(transform.position);

        destinationPosition = transform.position;
    }

    private void HandleMovementAction(InputAction.CallbackContext obj)
    {
        if (!movementEnabled)
            return;

        currentControl = ControlType.Controllers;

        RemoveFocus();

        // stop pathfinding
        if (!agent.isStopped)
            agent.isStopped = true;

        Vector2 input = obj.ReadValue<Vector2>();
        // find correct right/forward directions based on main camera rotation
        Vector3 up = Vector3.up;
        Vector3 right = Camera.main.transform.right;
        Vector3 forward = Vector3.Cross(right, up);
        MovementVector = forward * input.y + right * input.x;
    }

    //private void OnAnimatorMove()
    //{
    //    agent.velocity = animator.deltaPosition / Time.deltaTime;
    //}

    private void Update()
    {
        float speed = 0f;
        // uses keyboardmovement
        if (useKeyboard && currentControl == ControlType.Controllers)
        {
            MovementVector.Normalize();
            if (MovementVector != Vector3.zero)
            {
                agent.Move(MovementVector * agent.speed * Time.deltaTime);
                Quaternion targetRotation = Quaternion.LookRotation(MovementVector);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
            {
                //damp the speed after stop walking
                agent.Move(transform.forward * agent.speed * stopSmoothing * Time.deltaTime);
            }

            //destinationPosition = transform.position;

            speed = (MovementVector == Vector3.zero) ? 0f : agent.speed;
        }
        else
        {

            if (agent.pathPending)
            {
                return;
            }

            speed = agent.desiredVelocity.magnitude;

            if (agent.remainingDistance <= agent.stoppingDistance * stopDistanceProportion)
            {
                Stopping(out speed);
            }
            else if (agent.remainingDistance <= agent.stoppingDistance)
            {
                Slowing(out speed, agent.remainingDistance);
            }
            else if (speed > turnSpeedThreshold)
            {
                Moving();
            }
        }

        animator.SetFloat(hashSpeedPara, speed, speedDampTime, Time.deltaTime);
        
        //UpdateLineRenderer();
    }

    private void Stopping(out float speed)
    {
        agent.isStopped = true;
        //snap to the position
        transform.position = destinationPosition;
        speed = 0f;

        if(focus)
        {
            transform.rotation = focus.interactionTransform.rotation;
            // use Radius or use position
            //focus.Interact();
            focus = null;

            //ToDo.Block the input
        }
    }

    private void Slowing(out float speed, float distanceToDestination)
    {
        agent.isStopped = true;

        float proportionalDistance = 1f - distanceToDestination / agent.stoppingDistance;

        Quaternion targetRotation = focus ? focus.interactionTransform.rotation : transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, proportionalDistance);
        transform.position = Vector3.MoveTowards(transform.position, destinationPosition, slowingSpeed * Time.deltaTime);
        speed = Mathf.Lerp(slowingSpeed, 0f, proportionalDistance);

    }

    private void Moving()
    {
        Quaternion targetRotation = Quaternion.LookRotation(agent.desiredVelocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);
    }

    private void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
        }

        newFocus.OnFocused(transform);
    }

    private void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
    }

    #region FunctionCalls
    public void OnGroundClick(BaseEventData data)
    {
        if (!movementEnabled)
            return;

        currentControl = ControlType.Point;
        RemoveFocus();

        PointerEventData pData = (PointerEventData)data;
        MoveTo(pData.pointerCurrentRaycast.worldPosition);
    }

    public void OnInteractableClick(Interactable interactable)
    {
        if (!movementEnabled)
            return;

        currentControl = ControlType.Point;
        SetFocus(interactable);
        destinationPosition = focus.interactionTransform.position;

        agent.SetDestination(destinationPosition);
        agent.isStopped = false;
    }

    public void MoveTo(Vector3 position)
    {
        if (!movementEnabled)
            return;

        // Player shouldn't use this function outside this script
        RemoveFocus();

        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, navMeshSampleDistance, NavMesh.AllAreas))
        {
            destinationPosition = hit.position;
        }
        else
        {
            // donesn't hit mesh, try finding something nearby
            destinationPosition = position;
        }

        //// only trigger pathfinding once, then the speed is mine to change!
        agent.SetDestination(destinationPosition);
        agent.isStopped = false;
    }
    
    public void SetPosition(Vector3 position)
    {
        GetComponent<Transform>().position = position;
    }

    public void StopMovement()
    {
        movementEnabled = false;
    }

    public void ResumeMovement()
    {
        movementEnabled = true;
    }

    #endregion

}
