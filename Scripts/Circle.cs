using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public static float ORDER_RADIUS = 0.022f;
    public static float BIAS_TO_ARC_COEF = 8;
    public static float ORDER_TO_SHIFT_INFLUENCE = 0.02f;

    private int order;
    private float radius;
    private float rotation;

    public void setOrder(int newOrder)
    {
        order = newOrder;
        radius = ORDER_RADIUS * order;
        Transform sprite = transform.Find("Sprite");
        sprite.position = new Vector3(0, ORDER_RADIUS * order, 0);
        rotation = 0;
    }

    public void shift(float bias)
    {
        rotation += bias * (BIAS_TO_ARC_COEF + (ORDER_TO_SHIFT_INFLUENCE * order));
        transform.rotation = Quaternion.Euler(Vector3.forward * rotation);
    }

    private float getArcCast()
    {
        if (transform.position.x > 0 && transform.position.y > 0)
        {
            return 0;
        }
        if (transform.position.x < 0 && transform.position.y > 0)
        {
            return (float)(Math.PI / 2);
        }
        if (transform.position.x < 0 && transform.position.y < 0)
        {
            return (float)(Math.PI);
        }
        return (float)(Math.PI * 3 / 2);
    }
}
