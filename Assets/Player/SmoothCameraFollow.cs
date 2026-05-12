using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmoothCameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;

    public GameObject target;

    private Vector3 Vel = Vector3.zero;

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position + offset;
            targetPosition.z = transform.position.z;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref Vel, damping);
        }

    }
    void Update()
    {
        if (target == null)
        {
            target = GameObject.Find("playerV2(Clone)");
        }
    }
}
