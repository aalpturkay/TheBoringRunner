using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
    private bool _tap, _swipeUp, _swipeDown, _swipeLeft, _swipeRight;
    private bool _isDragging = false;
    private Vector2 _startTouch, _swipeDelta;

    public Vector2 SwipeDelta => _swipeDelta;

    public bool SwipeUp => _swipeUp;

    public bool SwipeDown => _swipeDown;

    public bool SwipeRight => _swipeRight;

    public bool SwipeLeft => _swipeLeft;


    private void Update()
    {
        _tap = _swipeDown = _swipeUp = _swipeLeft = _swipeRight = false;
        
        #region Standalone Inputs

        if (Input.GetMouseButtonDown(0))
        {
            _tap = true;
            _isDragging = true;
            _startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            Reset();
        }

        #endregion

        #region Mobile Inputs

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _tap = true;
                _isDragging = true;
                _startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                _isDragging = false;
                Reset();
            }
        }

        #endregion

        // Calculate the distance
        _swipeDelta = Vector2.zero;
        if (_isDragging)
        {
            
            if (Input.touches.Length > 0)
                _swipeDelta = Input.touches[0].position - _startTouch;
            else if (Input.GetMouseButton(0))
                _swipeDelta = (Vector2) Input.mousePosition - _startTouch;
        }

        // Deadzone
        if (_swipeDelta.magnitude > 125)
        {
            // Direction
            float x = _swipeDelta.x;
            float y = _swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Left or right
                if (x < 0)
                    _swipeLeft = true;
                else
                    _swipeRight = true;
            }
            else
            {
                // Up or Down
                if (y < 0)
                    _swipeDown = true;
                else
                    _swipeUp = true;
            }
        }
    }

    private void Reset()
    {
        _startTouch = _swipeDelta = Vector2.zero;
        _isDragging = false;
    }
}