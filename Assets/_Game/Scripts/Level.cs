using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    public Transform startPoint;
    public Transform finishPoint;
    public int amountBot;
    public Stage[] stages;

    public List<NavMeshSurface> surfaces = new ();

    public void OnInit()
    {
        for(int i = 0; i < stages.Length; i++)
        {
            stages[i].OnInit();            
        }
    }
}
