using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType { Default, Blue, Yellow, Orange, Red, Green, Pink, Cyan }
public class Stage : MonoBehaviour
{
    public Transform[] brickPoints;
    public List<Vector3> brickEmptyPoints = new List<Vector3>();
    public List<Brick> bricks = new List<Brick>();

    [SerializeField] Brick brickPrefab;
    private void Start()
    {

    }
    public void InitColor(ColorType color)
    {
        for (int i = 0; i < brickPoints.Length / LevelManager.Instance.amountCharacter; i++)
        {
            SpawnBrick(color);
        }
    }
    public void SpawnBrick(ColorType color)
    {
        if (brickEmptyPoints.Count > 0)
        {
            int rand = Random.Range(0, brickEmptyPoints.Count);
            //Brick brick = Instantiate(brickPrefab, brickEmptyPoints[rand], Quaternion.identity);
            Brick brick = SimplePool.Spawn<Brick>(brickPrefab, brickEmptyPoints[rand], Quaternion.identity);
            brick.ChangeColor(color);
            brick.stage = this;
            brickEmptyPoints.RemoveAt(rand);
            bricks.Add(brick);
        }
    }
    public Brick FindFirstBrickByColor(ColorType color)
    {
        Brick brick = null;
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].color.Equals(color))
            {
                brick = bricks[i];
            }
        }

        return brick;
    }
    public void AddEmptyPoint(Brick brick)
    {
        brickEmptyPoints.Add(brick.transform.position);
        bricks.Remove(brick);
    }

    internal void OnInit()
    {
        for (int i = 0; i < brickPoints.Length; i++)
        {
            brickEmptyPoints.Add(brickPoints[i].position);
        }
    }
}
