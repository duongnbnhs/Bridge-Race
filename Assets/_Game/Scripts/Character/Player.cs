using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Character
{
    [SerializeField] float speed;
    [SerializeField] AudioClip takeBrick;
    [SerializeField] AudioClip removeBrick;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsState(GameState.Play))
        {
            if (Input.GetMouseButton(0))
            {
                //rb.velocity = Joystick.direct * speed + rb.velocity.y * Vector3.up;
                Vector3 nextPoint = Joystick.direct * speed * Time.deltaTime + transform.position;
                if (CanMoveOnBridge(nextPoint))
                {
                    transform.position = CheckGround(nextPoint);
                    characterSkin.forward = Joystick.direct;
                    ChangeAnim("Run");
                }
            }
            else
            {
                ChangeAnim("Idle");
            }

        }

    }
    public override void AddBrick()
    {
        base.AddBrick();
        SoundManager.Instance.PlaySound(takeBrick);
    }
    public override void RemoveBrick()
    {
        base.RemoveBrick();
        SoundManager.Instance.PlaySound(removeBrick);
    }
}
