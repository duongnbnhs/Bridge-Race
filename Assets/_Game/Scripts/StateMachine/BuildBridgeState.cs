using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridgeState : IState<BOT>
{
    public void OnEnter(BOT t)
    {
        t.SetDestination(LevelManager.Instance.finishPoint);
    }

    public void OnExecute(BOT t)
    {
        if(t.bricks.Count == 0)
        {
            t.ChangeState(new FindBrickState());
        }
    }

    public void OnExit(BOT t)
    {
        
    }
}
