using UnityEngine;
using System.Collections;

public class Helper{

    public static bool Contain(Bounds b, Vector2 p)
    {
        if (b.center.x - b.extents.x < p.x &&
            b.center.x + b.extents.x > p.x &&
            b.center.y - b.extents.y < p.y &&
            b.center.y + b.extents.y > p.y)
            return true;
        return false;
    }
}
