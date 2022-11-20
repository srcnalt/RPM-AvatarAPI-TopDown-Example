using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    private Vector3 velocity;
    private Vector3 initialPos = new Vector3(0, 7, -3);

    private void Start()
    {
        target = AvatarManager.Instance.LowAvatar.transform;
    }

    // Update is called once per frame
    void Update()
    {
        var targetPos = target.position;
        var currentPos = transform.position;
        
        transform.position = Vector3.SmoothDamp(currentPos, targetPos + initialPos, ref velocity, 0.3f);
    }
}
