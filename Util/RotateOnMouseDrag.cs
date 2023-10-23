using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateOnMouseDrag : MonoBehaviour, IDragHandler
{

    public bool x, y;
    public float speed = 1;

    public Transform Item;

    private float veclocityX = 0;
    private float veclocityY = 0;

    public void OnDrag(PointerEventData eventData)
    {

        veclocityX = eventData.delta.x * speed;
        veclocityY = eventData.delta.y * speed;
        Item.eulerAngles += new Vector3(x ? -eventData.delta.y : 0, y ? -eventData.delta.x : 0) * speed;
    }

    void Update()
    {

        if (veclocityX > 0.25f)
        {
            veclocityX -= 1 * Time.deltaTime;
        }
        else if (veclocityX < 1)
        {
            veclocityX += 1 * Time.deltaTime;
        }

        if (veclocityY > 0.25f)
        {
            veclocityY -= 1 * Time.deltaTime;
        }
        else if (veclocityY < 0.25f)
        {
            veclocityY += 1 * Time.deltaTime;
        }

        if (veclocityX < 0.1f && veclocityX > -0.1f)
        {
            veclocityX = 0;
        }

        if (veclocityY < 0.1f && veclocityY > -0.1f)
        {
            veclocityY = 0;
        }

        Item.eulerAngles += new Vector3(x ? veclocityX : 0, y ? veclocityY : 0) * speed;
    }
}
