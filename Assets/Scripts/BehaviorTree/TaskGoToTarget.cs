using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskGoToTarget : Node
{
    private SimpleCharacterMovement _characterMovement;
    private bool _moving = false;

    public TaskGoToTarget(SimpleCharacterMovement simpleCharacterMovement)
    {
        _characterMovement = simpleCharacterMovement;
    }

    public override NodeState Evalute()
    {
        Transform target = (Transform)GetData("target");

        if(!_moving)
        {
            _characterMovement.MoveTo(target.position);
            _moving = true;
        }
        else if (_characterMovement.agent.velocity == Vector3.zero)
        {
            _moving = false;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
