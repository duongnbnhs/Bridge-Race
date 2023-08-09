using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshManager : Singleton<NavmeshManager>
{
    public List<NavMeshSurface> surfaces => LevelManager.Instance.currentLevel.surfaces;
    public void Baker()
    {
        Debug.Log("Bake navmesh");
        Debug.Log(surfaces.Count + " sur");
        //NavMesh.RemoveAllNavMeshData();
        UnityEditor.AI.NavMeshBuilder.ClearAllNavMeshes();
        foreach (NavMeshSurface surface in surfaces)
        {
            surface.BuildNavMesh();            
        }
        
    }
    
}
