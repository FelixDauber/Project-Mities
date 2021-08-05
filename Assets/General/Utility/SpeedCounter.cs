using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCounter : MonoBehaviour
{
    [SerializeField]
    private float currentSpeed;
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, Vector3.Distance(transform.position, lastPosition) / Time.deltaTime, 0.1f);
        if (currentSpeed < 0.08f)
            currentSpeed = 0;
        lastPosition = transform.position;
    }
}
