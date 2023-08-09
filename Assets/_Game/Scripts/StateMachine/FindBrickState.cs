using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBrickState : IState<BOT>
{
    int num;
    public void OnEnter(BOT t)
    {
        //num = Random.Range(1, 5);
        num = 6;
        t.ChangeAnim("Run");
        Move(t);
    }
    void Move(BOT t)
    {
        if(t.stage != null)
        {
            //Brick brick = t.FindNearestBrick();
            Brick brick = t.stage.FindFirstBrickByColor(t.color);
            if (brick == null)
            {
                t.ChangeState(new BuildBridgeState());
            }
            else
            {
                t.SetDestination(brick.transform.position);
            }
        }
        else
        {
            t.SetDestination(t.transform.position);
        }
    }
    public void OnExecute(BOT t)
    {
        if (t.IsDestination)
        {
            if(t.bricks.Count < num)
            {
                Move(t);
            }
            else
            {
                t.ChangeState(new BuildBridgeState());
            }
        }
    }

    public void OnExit(BOT t)
    {
        
    }
}
