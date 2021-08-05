using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractor : MonoBehaviour
{
    private Camera playerCamera;

    private RaycastHit hitInfo;
    public RaycastHit HitInfo
    {
        get => hitInfo;
    }

    public UnityEvent<RaycastHit> NewHit;
    public UnityEvent<RaycastHit> ClickedObject;

    private bool overlapsUI;
    public UnityEvent<bool> NowOverlapsUI;

    private void Awake()
    {
        playerCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Physics.Raycast(playerCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            //Invoke new object if not hitting the same object
            if (hitInfo.collider != hit.collider)
            {
                NewHit.Invoke(hit);
            }
            hitInfo = hit;

            //Interact if clicked mouse button
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Interact();
                ClickedObject.Invoke(hit);
            }
        }
        else if(hitInfo.collider != null)
        {
            hitInfo = new RaycastHit();
            NewHit.Invoke(hitInfo);
        }

        if(overlapsUI != UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            overlapsUI = !overlapsUI;
            NowOverlapsUI.Invoke(overlapsUI);
        }
    }

    void Interact()
    {
        IInteractable interactable = hitInfo.collider.gameObject.GetComponent<IInteractable>();
        if(interactable != null)
        {
            interactable.Interact();
        }
    }

    void OnDrawGizmos()
    {
        if (hitInfo.collider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, hitInfo.point);
        }
    }
}