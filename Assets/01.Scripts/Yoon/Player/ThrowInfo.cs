using UnityEngine;

public struct ThrowInfo
{
    public float throwForce;

    // click pos
    public Vector3 startPosition;
    public Vector3 currentPosition;

    // can fire only forward direction
    public Vector2 direction
    {
        get
        {
            Vector2 dir = (currentPosition - startPosition);
            dir *= -1;
            dir.x = Mathf.Clamp(dir.x, 0.15f, 1.5f);
            dir.y = Mathf.Clamp(dir.y, 0.2f, 1.9f);
            return dir;
        }
    }

    public float dragPosDistance
    {
        get
        {
            float distance = Vector2.Distance(currentPosition, startPosition);
            distance = Mathf.Clamp(distance, 0, 3.5f);
            return distance;
        }
    }

    public Vector2 force
    {
        get
        {
            return direction * dragPosDistance;
        }
    }
}
