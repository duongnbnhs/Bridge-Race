using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class BOT : Character
{
    public Transform finishBox;
    public NavMeshAgent agent;
    IState<BOT> currentState;
    
    public void ChangeState(IState<BOT> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangeAnim("Idle");
    }
    Vector3 destination;
    //public bool IsDestination => Vector3.Distance(destination, transform.position) < 0.1f;
    public bool IsDestination => Vector3.Distance(destination, new Vector3(transform.position.x, 0, transform.position.z)) < 0.1f;
    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destination = position;
        destination.y = 0;
        agent.SetDestination(position);
    }
    public Brick FindNearestBrick()
    {
        Brick brick = null;
        List<Brick> sameColorBricks = new List<Brick>();
        foreach (var b in stage.bricks)
        {
            if (b.color.Equals(color))
            {
                sameColorBricks.Add(b);
            }
        }
        float minDistance = Vector3.Distance(transform.position, sameColorBricks[0].transform.position);
        brick = sameColorBricks[0];
        for (int i = 1; i < sameColorBricks.Count; i++)
        {
            var dis = Vector3.Distance(transform.position, sameColorBricks[i].transform.position);
            if (minDistance > dis)
            {
                minDistance = dis;
                brick = sameColorBricks[i];
            }
        }

        return brick;
    }
    public void StopMoving()
    {
        agent.enabled = false;
    }
    /*protected override void Start()
    {
        base.Start();
        ChangeState(new FindBrickState());
    }*/
    private void Update()
    {
        if(GameManager.Instance.IsState(GameState.Play))
        {
            if (currentState != null)
            {
                currentState.OnExecute(this);
                CanMoveOnBridge(transform.position);
            }
        }      
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FinishBox>())
        {
            Debug.Log("Bing");
            GameManager.Instance.ChangeState(GameState.Finish);
            LevelManager.Instance.OnFinishGame();
            var lose = UIManager.Instance.OpenUI<Lose>();
            lose.textMeshPro.text = "Level " + LevelManager.Instance.lvIndex + 1;
            ChangeAnim("Dance");
            transform.eulerAngles = Vector3.up * 180;
            OnInit();
            
        }
    }*/
}
