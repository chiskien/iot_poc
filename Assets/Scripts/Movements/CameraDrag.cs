using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 _origin;
    private Vector3 _difference;
    private Camera _camera;
    private bool _isDragging;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void OnDrag(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            _origin = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        _isDragging = callbackContext.started || callbackContext.performed;
    }

    private void LateUpdate()
    {
        if (_isDragging) return;
        
    }
}