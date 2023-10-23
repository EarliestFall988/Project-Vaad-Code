using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedRemove : MonoBehaviour
{

    public float timeToDestroy = 5;
    public bool destroyOnStart = true;

    // Start is called before the first frame update
    void Start()
    {
        if (destroyOnStart)
        {
            DeleteItem();
        }
    }


    void DeleteItem()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
