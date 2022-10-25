using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

// leave
public class TaskPartrol : Node
{
    private Transform[] _waypoints;
    private SimpleCharacterMovement _characterMovement;

    private int _currentWayPointIndex = 0;

    private float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;
    private bool _moving = false;

    public TaskPartrol(SimpleCharacterMovement characterMovement, Transform[] waypoints)
    {
        _characterMovement = characterMovement;
        _waypoints = waypoints;
    }

    public override NodeState Evalute()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
                _waiting = false;
        }
        else
        {
            Transform wp = _waypoints[_currentWayPointIndex];
            if (!_moving)
            {
                _characterMovement.MoveTo(wp.position);
                _moving = true;
            }
            else
            {
                if( _characterMovement.agent.velocity == Vector3.zero )
                {
                    _waitCounter = 0;
                    _waiting = true;
                    _moving = false;
                    _currentWayPointIndex = (_currentWayPointIndex + 1) % _waypoints.Length;
                }
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}

/*
    if (Vector3.Distance(_transform.position, wp.position) < 0.01f)
    {
        _transform.position = wp.position;
        _waitCounter = 0;
        _waiting = true;

        _currentWayPointIndex = (_currentWayPointIndex + 1) % _waypoints.Length;
    }
    else
    {
        _transform.position = Vector3.MoveTowards(_transform.position, wp.position, GuardBT.speed * Time.deltaTime);
        _transform.LookAt(wp.position);
    }
*/