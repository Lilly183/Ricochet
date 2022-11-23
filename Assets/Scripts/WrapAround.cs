using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapAround : MonoBehaviour
{
    //====================
    // Member Variable(s):
    //====================

    public float xBounds = 10.1265f;
    public float yBounds = 6.235f;

    public float repositionBuffer = 0.1f;

    // Update is called once per frame
    void Update()
    {
        Transform t = gameObject.transform;

        /*
        ============
        Wrap Around:
        ============

        Because the camera is centered at the origin, the distance from the midpoint to 
        a point that's no longer visible to the camera is the same in both directions for 
        each axis. Flipping the sign will give us the other bound for the same axis: If 
        the left bound is at X = -10, the right bound is at X = 10. This means we only 
        need to store two values.

        To prevent an object from moving offscreen, its current position is checked 
        relative to the horizontal and the vertical bounds. For example, if an object's 
        current position on the X-axis is too far left (<= -xBounds), we change its 
        x-coordinate so that it reappears at the far righthand side of the screen (near 
        +xBounds, minus some padding). If the object's current position is too far to 
        the right (>= +xBounds), we change its x-coordinate so that it reappears on the 
        lefthand side of the screen (close to -xBounds. Again, minus some padding). For 
        yBounds, we manipulate the object's y-coordinate instead.
        */

        if (t.position.x <= -xBounds)
        {
            t.position = new Vector3(xBounds - repositionBuffer, t.position.y, t.position.z);
        }

        else if (t.position.x >= xBounds)
        {
            t.position = new Vector3(-xBounds + repositionBuffer, t.position.y, t.position.z);
        }

        if (t.position.y <= -yBounds)
        {
            t.position = new Vector3(t.position.x, yBounds - repositionBuffer, t.position.z);
        }

        else if (t.position.y >= yBounds)
        {
            t.position = new Vector3(t.position.x, -yBounds + repositionBuffer, t.position.z);
        }
    }
}
