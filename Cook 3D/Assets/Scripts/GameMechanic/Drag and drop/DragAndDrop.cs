using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] InputAction _mouseClick;
    [SerializeField] private float _mouseDragPhysicsSpeed = 10f;
    [SerializeField] private float _mouseDragSpeed = .1f;

    [SerializeField] InputAction _touch;

    private Camera _mainCamera;
    private Vector3 _velocity = Vector3.zero;
    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _mouseClick.Enable();
        _mouseClick.performed += MousePressed;
        _touch.Enable();
        _touch.performed += TouchPressed;


    }
    private void OnDisable()
    {
        _mouseClick.performed -= MousePressed;
        _mouseClick.Disable();

        _touch.Disable();
        _touch.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Ray ray = _mainCamera.ScreenPointToRay(Touchscreen.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.gameObject.GetComponent<IDrag>() != null)
            {
                StartCoroutine(TouchDragUpdate(hit.collider.gameObject));
            }
        }
    }

    private IEnumerator TouchDragUpdate(GameObject touchedObject)
    {


        touchedObject.TryGetComponent<Rigidbody>(out var rb);
        touchedObject.TryGetComponent<IDrag>(out var iDragComponent);
        iDragComponent?.onStartDrag();
        float initialDistance = Vector3.Distance(touchedObject.transform.position, _mainCamera.transform.position);

        while (_touch.ReadValue<float>() != 0)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Touchscreen.current.position.ReadValue());

            if (rb != null)
            {
                Vector3 direction = ray.GetPoint(initialDistance) - touchedObject.transform.position;
                rb.velocity = direction * _mouseDragPhysicsSpeed;
                yield return _waitForFixedUpdate;
            }
            else
            {
                touchedObject.transform.position = Vector3.SmoothDamp(touchedObject.transform.position, ray.GetPoint(initialDistance),
                    ref _velocity, _mouseDragSpeed);
                yield return null;
            }

        }
        iDragComponent?.onEndDrag();
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.gameObject.GetComponent<IDrag>() != null)
            {
                StartCoroutine(DragUpdated(hit.collider.gameObject));
            }
        }
    }

    private IEnumerator DragUpdated(GameObject clickedObject)
    {
        clickedObject.TryGetComponent<Rigidbody>(out var rb);
        clickedObject.TryGetComponent<IDrag>(out var iDragComponent);
        iDragComponent?.onStartDrag();
        float initialDistance = Vector3.Distance(clickedObject.transform.position, _mainCamera.transform.position);


        while (_mouseClick.ReadValue<float>() != 0)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (rb != null)
            {
                Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                rb.velocity = direction * _mouseDragPhysicsSpeed;
                yield return _waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(initialDistance),
                    ref _velocity, _mouseDragSpeed);
                yield return null;
            }

        }

        iDragComponent.onEndDrag();
    }
}
