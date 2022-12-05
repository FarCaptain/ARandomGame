using System.Collections;
using System.Collections.Generic;

using BehaviorTree;

public class GuardBT : Tree
{
    public UnityEngine.Transform[] waypoints;
    public SimpleCharacterMovement simpleCharacterMovement;
    public static float speed = 2f;
    public static float fovRange = 6f;
    public static float attackRange = 2.3f;
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform),
                new TaskAttack(transform)
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(simpleCharacterMovement)
            }),
            new TaskPartrol(simpleCharacterMovement, waypoints)
        });
        
        return root;
    }

    public void ClearTarget()
    {
        if(_root != null)
        {
            _root.ClearData("target");
        }
    }
}
