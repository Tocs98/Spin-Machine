using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiguresLogic : MonoBehaviour
{
    private GameObject YPosition; //Y position of where the object should be placed at first instance
    private GameObject firstYPosition; //Position where this GO should restart his lerp movement
    private GameObject lastYPosition; //Position of where this GO is gonna end his lerp movement
    private GameObject forceEndPosition; //Forcing the gameobject to stop on this position. It's going to be the closest one


    //LERP VARIABLES
    private float speed = 15;
    private bool lerping = false;
    private Vector3 currentPosition;
    private Vector3 endPosition;
    private float distance;
    private float StartTime;

    private bool forcePosition;
    public int position;
    // Start is called before the first frame update
    void Start()
    {
        YPosition = GameObject.FindGameObjectWithTag("YPos");
        firstYPosition = YPosition.transform.GetChild(transform.parent.childCount).gameObject;
        lastYPosition = YPosition.transform.GetChild(0).gameObject;
    }

    public void StartLerp()
    {
        currentPosition = transform.position;
        endPosition = new Vector3 (transform.position.x, lastYPosition.transform.position.y, 1);
        distance = Vector3.Distance(currentPosition, endPosition);
        StartTime = Time.time;
        lerping = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (lerping)
        {
            float distanceCovered = (Time.time - StartTime) * speed;

            float fractionOfDistance = distanceCovered / distance;

            transform.position = Vector3.Lerp(currentPosition, endPosition, fractionOfDistance);
            if(fractionOfDistance >= 1)
            {
               if(!forcePosition) EndLerp();
            }
        }

        if (forcePosition)
        {
            //https://chicounity3d.wordpress.com/2014/05/23/how-to-lerp-like-a-pro/
            float distanceCovered = (Time.time - StartTime) * speed;

            float t = distanceCovered / distance;
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            transform.position = Vector3.Lerp(currentPosition, endPosition, t);

            if(t > 1)
            {
                forcePosition = false;
                //ENDED, CHECK FOR COMBOS
            }
        }
    }
    public void ForcePositionToStop()
    {
        lerping = false;
        Vector3 positionToStop = new Vector3(-1000, -1000, 1);
        foreach(Transform child in YPosition.transform)
        {
            float childDistance = Mathf.Abs(Vector3.Distance(transform.position, child.transform.position)); //Using Absolute to make sure that the value is never negative
            float nearestChild = Mathf.Abs(Vector3.Distance(transform.position,positionToStop));

            if(childDistance < nearestChild)
            {
                positionToStop = child.transform.position;
            }
        }
        
        currentPosition = transform.position;
        endPosition = new Vector3(transform.position.x, positionToStop.y, 1);
        distance = Vector3.Distance(currentPosition, endPosition);
        StartTime = Time.time;
        forcePosition = true;
    }
    private void EndLerp()
    {
        lerping = false;
        transform.position = new Vector3(transform.position.x, firstYPosition.transform.position.y, 1);
        StartLerp();
    }
}
