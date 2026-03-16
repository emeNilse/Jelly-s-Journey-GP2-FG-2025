using UnityEngine;

public class TaskIsDying : Node
{
    public TaskIsDying(BehaviourTree aTree) : base() { myTree = aTree; }
    BehaviourTree myTree;

    public override NodeState Evaluate()
    {
        EnemyBT eBT = myTree as EnemyBT;

        if (eBT.enemy.IsAlive())
        {
            return NodeState.Success;
        }
        else
        {
            return NodeState.Failure;
        }
    }
}
