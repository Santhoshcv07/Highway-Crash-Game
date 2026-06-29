using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform playerCarTransform;
    [SerializeField] float offSet = -3;
    // Start is called before the first frame update
void Start()
    {
        
    }
    public void SetTransform(Transform transform)
    {
        playerCarTransform = transform;
    }
    void LateUpdate()
    {
        if (playerCarTransform == null)
        {
            return;
        }
        Vector3 cameraPos = transform.position;
        cameraPos.z = playerCarTransform.position.z + offSet;
        transform.position = cameraPos;
    }
}


    
