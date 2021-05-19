using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swipe : MonoBehaviour
{
    private bool leftClick, doubleLeftClick, rightClick, doubleRightClick, tap, doubleTap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool swipeUpLeft, swipeUpRight, swipeDownLeft, swipeDownRight;
    private Vector2 startTouch, swipeDelta;
    private bool isDragging = false;
    public float SwipeDeadzone = 0;
    private float lastClick = 0;
    public float doubleTapInterval = 0.4f;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool LeftClick { get { return leftClick; } }
    public bool RightClick { get { return rightClick; } }
    public bool DoubleLeftClick { get { return doubleLeftClick; } }
    public bool DoubleRightClick { get { return doubleRightClick; } }
    public bool Tap { get { return tap; } }
    public bool DoubleTap { get { return doubleTap; } }

    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    public bool SwipeUpLeft { get { return swipeUpLeft; } }
    public bool SwipeUpRight { get { return swipeUpRight; } }
    public bool SwipeDownLeft { get { return swipeDownLeft; } }
    public bool SwipeDownRight { get { return swipeDownRight; } }

    public enum SwipeType {four_directional, eight_directional};
    public SwipeType swipetype;

    private void Update()
    {
        leftClick = doubleLeftClick = rightClick = doubleRightClick = tap = doubleTap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
        swipeUpLeft = swipeUpRight = swipeDownLeft = swipeDownRight = false;

        // For mouse drag inputs (works in editor and standalone build)
        #region STANDALONE_INPUT    

        // For left clicks
        if (Input.GetMouseButtonDown(0))
        {
            if ((lastClick + doubleTapInterval) > Time.time)
            {
                doubleLeftClick = true;
            }
            else
            {
                lastClick = Time.time;
                leftClick = true;
            }
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }

        // For right clicks
        if (Input.GetMouseButtonDown(1))
        {
            if ((lastClick + doubleTapInterval) > Time.time)
            {
                doubleRightClick = true;
            }
            else
            {
                lastClick = Time.time;
                rightClick = true;
            }
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Reset();
        }
        #endregion

        // For mobile touch inputs
        #region MOBILE_INPUT        
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                if ((lastClick + doubleTapInterval) > Time.time)
                {
                    doubleTap = true;
                }
                else
                {
                    lastClick = Time.time;
                    tap = true;
                }
                isDragging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                Reset();
            }
        }
        #endregion

        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        if (swipeDelta.magnitude > SwipeDeadzone)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            // FOR 4 - DIRECTIONAL SWIPE
            #region 4_DIRECTIONAL_SWIPE
            if (swipetype == SwipeType.four_directional)
            {
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                    {
                        swipeLeft = true;
                    }
                    else
                    {
                        swipeRight = true;
                    }
                }
                else
                {
                    if (y < 0)
                    {
                        swipeDown = true;
                    }
                    else
                    {
                        swipeUp = true;
                    }
                }
            }
            #endregion

            //FOR 8 - DIRECTIONAL SWIPE
            #region 8_DIRECTIONAL_SWIPE
            if (swipetype == SwipeType.eight_directional)
            {
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                    {
                        if (Mathf.Abs(y) < (Mathf.Abs(x) / 2))
                        {
                            swipeLeft = true;
                        }
                        else
                        {
                            if (y > 0)
                            {
                                swipeUpLeft = true;
                            }
                            else
                            {
                                swipeDownLeft = true;
                            }
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(y) < (Mathf.Abs(x) / 2))
                        {
                            swipeRight = true;
                        }
                        else
                        {
                            if (y > 0)
                            {
                                swipeUpRight = true;
                            }
                            else
                            {
                                swipeDownRight = true;
                            }
                        }
                    }
                }
                else
                {
                    if (y < 0)
                    {
                        if (Mathf.Abs(x) < (Mathf.Abs(y) / 2))
                        {
                            swipeDown = true;
                        }
                        else
                        {
                            if (x > 0)
                            {
                                swipeDownRight = true;
                            }
                            else
                            {
                                swipeDownLeft = true;
                            }
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(x) < (Mathf.Abs(y) / 2))
                        {
                            swipeUp = true;
                        }
                        else
                        {
                            if (x > 0)
                            {
                                swipeUpRight = true;
                            }
                            else
                            {
                                swipeUpLeft = true;
                            }
                        }
                    }
                }
            }
            #endregion

            Reset();
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }
}
