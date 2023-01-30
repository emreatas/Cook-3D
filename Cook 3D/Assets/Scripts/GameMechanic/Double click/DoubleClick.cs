using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;

namespace DoubleClick
{
    public class DoubleClick : MonoBehaviour
    {
        [SerializeField] InputAction _mouseClick;
        [SerializeField] InputAction _touch;
        private Camera _mainCamera;
        private void Awake()
        {
            _mainCamera = Camera.main;
        }
        private void OnEnable()
        {
            _mouseClick.Enable();
            _mouseClick.performed += DoubleClickOnMouse;

            _touch.Enable();
            _touch.performed += DoubleClickOnTouch;
        }
        private void OnDisable()
        {
            _mouseClick.Disable();
            _mouseClick.performed -= DoubleClickOnMouse;

            _touch.Disable();
            _touch.performed -= DoubleClickOnTouch;
        }
        private void DoubleClickOnMouse(InputAction.CallbackContext context)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject.GetComponent<IDoubleClick>() != null)
                {
                    GameManager.instance.OnDoubleClick(hit.collider.gameObject);
                }
            }
        }
        private void DoubleClickOnTouch(InputAction.CallbackContext context)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Touchscreen.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject.GetComponent<IDoubleClick>() != null)
                {
                    GameManager.instance.OnDoubleClick(hit.collider.gameObject);
                }
            }
        }
    }
}