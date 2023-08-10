using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ColorObject
{
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected LayerMask stairLayer;
    [SerializeField] protected CharactersBrick characterBrickPrefab;
    [SerializeField] protected Transform brickHolder;
    [SerializeField] protected Transform characterSkin;
    internal List<CharactersBrick> bricks = new List<CharactersBrick>();
    public Stage stage;
    public Animator anim;
    string currentAnim;

    /*protected virtual void Start()
    {
        ChangeColor(color);
    }*/
    public virtual void OnInit()
    {
        ClearBrick();
        ChangeColor(color);
        //ChangeAnim("Idle");
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            Brick brick = other.GetComponent<Brick>();
            if (brick.color.Equals(color))
            {
                brick.OnDespawn();
                AddBrick();
                SimplePool.Despawn(brick);
            }
        }
    }
    public Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;

        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 1.1f;
        }
        return transform.position;
    }
    public bool CanMoveOnBridge(Vector3 nextPoint)
    {
        bool canMove = true;
        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, stairLayer))
        {
            Stair s = hit.collider.GetComponent<Stair>();
            if (!s.color.Equals(color) && bricks.Count > 0 && characterSkin.forward.z > 0)
            {
                s.ChangeColor(color);
                RemoveBrick();
                stage.SpawnBrick(color);
            }
            if (!s.color.Equals(color) && bricks.Count == 0 && characterSkin.forward.z > 0)
            {
                canMove = false;
            }
        }

        return canMove;
    }
    public virtual void AddBrick()
    {
        CharactersBrick brick = Instantiate(characterBrickPrefab, brickHolder);
        brick.ChangeColor(color);
        brick.transform.localPosition = Vector3.up * 0.35f * bricks.Count;
        bricks.Add(brick);
    }
    public virtual void RemoveBrick()
    {
        if (bricks.Count > 0)
        {
            CharactersBrick brick = bricks[bricks.Count - 1];
            bricks.RemoveAt(bricks.Count - 1);
            Destroy(brick.gameObject);
        }
    }
    public void ClearBrick()
    {
        foreach (var brick in bricks)
        {
            SimplePool.Despawn(brick);
        }
        bricks.Clear();
    }
    public void ChangeAnim(string animName)
    {
        if(currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
}
