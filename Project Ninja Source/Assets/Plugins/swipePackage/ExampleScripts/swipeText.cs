using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swipeText : MonoBehaviour
{
    public swipe swipeAction;
    public Text txt;

    void Update()
    {
        if (swipeAction.SwipeLeft)
        {
            txt.text = "Left Swipe!";                 // What to do when swiped left comes here
        }
        if (swipeAction.SwipeRight)
        {
            txt.text = "Right Swipe!";                // What to do when swiped right comes here
        }
        if (swipeAction.SwipeUp)
        {
            txt.text = "Up Swipe!";                   // What to do when swiped up comes here
        }
        if (swipeAction.SwipeDown)
        {
            txt.text = "Down Swipe!";                 // What to do when swiped down comes here
        }
        if (swipeAction.SwipeUpLeft)
        {
            txt.text = "Up Left Swipe!";              // What to do when swiped up left comes here
        }
        if (swipeAction.SwipeUpRight)
        {
            txt.text = "Up Right Swipe!";             // What to do when swiped up right comes here
        }
        if (swipeAction.SwipeDownLeft)
        {
            txt.text = "Down Left Swipe!";            // What to do when swiped down left comes here
        }
        if (swipeAction.SwipeDownRight)
        {
            txt.text = "Down Right Swipe!";           // What to do when swiped down right comes here
        }
        if (swipeAction.LeftClick)
        {
            txt.text = "Left Click!";                // What to do when mouse left clicked
        }
        if (swipeAction.DoubleLeftClick)
        {
            txt.text = "Double Left Click!";         // What to do when mouse double left clicked
        }
        if (swipeAction.RightClick)
        {
            txt.text = "Right Click!";                // What to do when mouse right clicked
        }
        if (swipeAction.DoubleRightClick)
        {
            txt.text = "Double Right Click!";         // What to do when mouse double right clicked
        }
        if (swipeAction.Tap)
        {
            txt.text = "Tap!";                        // What to do when tapped in mobile
        }
        if (swipeAction.DoubleTap)
        {
            txt.text = "Double Tap!";                 // What to do when double tapped in mobile
        }
    }
}
