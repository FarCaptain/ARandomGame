using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack : Node
{
    private Animator _playerAnimator;
    private float _respawnCounter = 0;

    public TaskAttack(Transform transform)
    {
    }

    public override NodeState Evalute()
    {
        Transform target = (Transform)GetData("target");
        _playerAnimator = target.GetComponent<Animator>();
        _playerAnimator.SetTrigger("Clicked");

        _respawnCounter += Time.deltaTime;
        if (_respawnCounter >= 5f)
        {
            target.transform.GetComponent<CharacterBehavior>().Respawn();
            _respawnCounter = 0f;
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
