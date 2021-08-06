using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [Header("Movement")]

    public Vector3 focusPoint;
    float wantedFocusPointHeight;
    public float baseMovementSpeed = 15;
    public float movementSpeed;
    public float extraSpeedPerDistance = 0.2f;
    public float maxCameraDistanceFromCenter = 1000;

    [Header("Zoom")]
    public float zoomSpeed = 1;
    public float minDistance = 1, maxDistance = 50;
    public float currentDistance;
    public float wantedDistance;
    public float zoomSoftening = 0.3f;

    [Header("Rotation")]
    public float rotationSpeed;
    public float minAngle = 10, maxAngle = 85;
    bool isRotating;

    private void OnValidate()
    {
        if(maxDistance < minDistance)
        {
            maxDistance = minDistance;
        }
        UpdatePosition();
    }

    void LateUpdate()
    {
        UpdateMovementSpeed();
        UpdateRotation();
        UpdateZoom();
        UpdatePosition();
        SecureCameraDistance();
        SecureFocusHeight();
    }

    void UpdateMovementSpeed()
    {
        movementSpeed = baseMovementSpeed + (extraSpeedPerDistance * wantedDistance);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed *= 2.5f;
        }
    }

    void UpdatePosition()
    {
        //Get forwards direction and flatten the height.
        Vector3 forwards = transform.forward;
        forwards.y = 0;
        forwards.Normalize();

        //Get right direction and flatten the height.
        Vector3 sideways = transform.right;
        forwards.y = 0;
        forwards.Normalize();

        //Assemble movement direction
        Vector3 movementDirection = (forwards * Input.GetAxis("Vertical")) + (sideways * Input.GetAxis("Horizontal"));
        movementDirection.Normalize();

        //Move camera's focus point
        focusPoint += movementDirection * Time.deltaTime * movementSpeed;
        focusPoint = Vector3.Lerp(focusPoint, new Vector3(focusPoint.x, wantedFocusPointHeight, focusPoint.z), 0.05f);

        //Limit camera's movement field
        float distanceFromCenter = Vector3.Distance(new Vector3(focusPoint.x, 0, focusPoint.z), Vector3.zero);
        if (distanceFromCenter > maxCameraDistanceFromCenter) focusPoint += (-new Vector3(focusPoint.x, 0, focusPoint.z).normalized * (distanceFromCenter - maxCameraDistanceFromCenter));

        //Move camera to look at the focus point
        transform.position = focusPoint - (transform.forward * currentDistance);

    }

    void UpdateZoom()
    {
        //Change the wanted zoom if the mouse isn't over the UI
        if (!EventSystem.current.IsPointerOverGameObject())
            wantedDistance = Mathf.Clamp(wantedDistance -= Input.mouseScrollDelta.y * zoomSpeed, minDistance, maxDistance);

        //Lerp the zoom's movement.
        currentDistance = Mathf.Lerp(currentDistance, wantedDistance, zoomSoftening * Time.deltaTime);
    }

    void UpdateRotation()
    {
        //Check when MMB is pressed and it's not over any UI
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetKeyDown(KeyCode.Mouse2))
        {
            isRotating = true;
        }
        //Require MMB to be held down
        if (!isRotating || !Input.GetKey(KeyCode.Mouse2))
        {
            isRotating = false;
            return;
        }

        //Get current angle
        Vector3 rotation = transform.eulerAngles;

        //Add mouse movements
        rotation.x -= Input.GetAxis("Mouse Y");
        rotation.y += Input.GetAxis("Mouse X");

        //Clamp up/down rotation
        rotation.x = Mathf.Clamp(rotation.x, minAngle, maxAngle);

        //Apply rotation
        transform.rotation = Quaternion.Euler(rotation);
    }

    void SecureFocusHeight()
    {
        if (Physics.Raycast(focusPoint + Vector3.up * 200, -Vector3.up, out RaycastHit hit))
        {
            if(hit.collider.gameObject.tag == "Terrain")
            wantedFocusPointHeight = hit.point.y;
        }
    }

    void SecureCameraDistance()
    {
        if(Physics.Raycast(transform.position + Vector3.up * 200, -Vector3.up, out RaycastHit hit, 201))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + 1, transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(focusPoint, 0.5f);
        Gizmos.DrawLine(transform.position, focusPoint);
    }
}
