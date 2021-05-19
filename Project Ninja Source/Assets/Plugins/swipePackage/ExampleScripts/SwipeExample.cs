using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeExample : MonoBehaviour
{
    // ***** To use swipe controls, add the below line to your script and check for the boolean values for true as done in the Update method*****
    public swipe swipeControls;
    //***********************************************************************
    public float moveSensitivity = 0;
    Vector3 desiredPosition;
    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (swipeControls.SwipeLeft)
        {
            desiredPosition += Vector3.left;                    // What to do when swiped left comes here
        }
        if (swipeControls.SwipeRight)
        {
            desiredPosition += Vector3.right;                   // What to do when swiped right comes here
        }
        if (swipeControls.SwipeUp)
        {
            desiredPosition += Vector3.forward;                 // What to do when swiped up comes here
        }
        if (swipeControls.SwipeDown)
        {
            desiredPosition += Vector3.back;                    // What to do when swiped down comes here
        }
        if (swipeControls.SwipeUpLeft)
        {
            desiredPosition += (Vector3.left + Vector3.forward);                   // What to do when swiped up left comes here
        }
        if (swipeControls.SwipeUpRight)
        {
            desiredPosition += (Vector3.right + Vector3.forward);                 // What to do when swiped up right comes here
        }
        if (swipeControls.SwipeDownLeft)
        {
            desiredPosition += ( Vector3.back + Vector3.left);                    // What to do when swiped down left comes here
        }
        if (swipeControls.SwipeDownRight)
        {
            desiredPosition += (Vector3.back + Vector3.right);                    // What to do when swiped down right comes here
        }

        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, moveSensitivity * Time.deltaTime);
    }
}
