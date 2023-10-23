using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public List<Transform> Waypoints = new List<Transform>();

    public bool platformMoving = false;
    public int currentWaypoint = 0;
    public float speed = 5;

    public float moveDelay = 0;

    private bool triggered = false;

    public Transform DoorLeftOpenPosition;
    public Transform DoorLeftClosedPosition;

    public Transform DoorRightOpenPosition;
    public Transform DoorRightClosedPosition;

    public Transform DoorLeft;
    public Transform DoorRight;

    private bool doorsOpen = true;


    public void TriggerPlatform(bool activate)
    {
        if (activate && !triggered)
        {
            doorsOpen = false;
            SetDoors(doorsOpen);
            StartCoroutine(MoveDelay(activate));
        }
        else
        {
            platformMoving = false;
        }
    }

    IEnumerator MoveDelay(bool activate)
    {
        triggered = true;
        yield return new WaitForSeconds(moveDelay);
        platformMoving = true;
        triggered = false;
    }

    void Update()
    {

        if (!platformMoving)
            return;

        Vector3 direction = Waypoints[currentWaypoint].position - transform.position;

        if (direction.magnitude < 0.1f)
        {
            currentWaypoint++;
            if (currentWaypoint >= Waypoints.Count)
            {
                platformMoving = false;
                doorsOpen = true;
                currentWaypoint = 0;
                Waypoints.Reverse();
                SetDoors(doorsOpen);

            }
        }
        transform.Translate(direction.normalized * Time.deltaTime * speed);


    }


    void SetDoors(bool open)
    {

        if (DoorLeft == null || DoorRight == null)
            return;

        if (open)
        {

            LeanTween.value(DoorLeft.gameObject, (x) =>
            {
                DoorLeft.position = x;
            }, DoorLeft.position, DoorLeftOpenPosition.position, moveDelay).setEase(LeanTweenType.easeInOutSine);

            LeanTween.value(DoorRight.gameObject, (x) =>
            {
                DoorRight.position = x;
            }, DoorRight.position, DoorRightOpenPosition.position, moveDelay).setEase(LeanTweenType.easeInOutSine);

        }
        else
        {
            LeanTween.value(DoorLeft.gameObject, (x) =>
            {
                DoorLeft.position = x;
            }, DoorLeft.position, DoorLeftClosedPosition.position, moveDelay).setEase(LeanTweenType.easeInOutSine);

            LeanTween.value(DoorRight.gameObject, (x) =>
            {
                DoorRight.position = x;
            }, DoorRight.position, DoorRightClosedPosition.position, moveDelay).setEase(LeanTweenType.easeInOutSine);
        }
    }
}
