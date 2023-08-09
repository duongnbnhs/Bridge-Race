using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform TF;
    public Transform playerTF;

    [SerializeField] Vector3 offset;
    //dung fixed update khi player di chuyen bang velocity;
    /*private void FixedUpdate()
    {
        TF.position = Vector3.Lerp(TF.position, playerTF.position + offset, Time.fixedDeltaTime * 5f);
    }*/
    //neu khong thi su dung late update
    private void LateUpdate()
    {
        TF.position = Vector3.Lerp(TF.position, playerTF.position + offset, Time.deltaTime * 5f);
    }
}
