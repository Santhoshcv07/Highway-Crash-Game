using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowCar : MonoBehaviour
{
    private Transform playercarTransform;
    private Transform cameraPointTransform;

    private Vector3 velocity = Vector3.zero;
// Start is called before the first frame update
void Start()
    {
        playercarTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cameraPointTransform = playercarTransform.Find("CameraPoint").GetComponent<Transform>();
    }
// Update is called once per frame
 void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            cameraPointTransform.position,
            ref velocity,
            0.15f
        );

        transform.rotation = playercarTransform.rotation;
    }
}