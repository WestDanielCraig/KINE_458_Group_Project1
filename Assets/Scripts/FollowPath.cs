﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;

public class FollowPath : MonoBehaviour
{
    #region Enums
    public enum MovementType  //Type of Movement
    {
        MoveTowards,
        LerpTowards
    }
    #endregion //Enums

    #region Public Variables
    public MovementType Type = MovementType.MoveTowards; // Movement type used
    public MovementPath MyPath; // Reference to Movement Path Used
    public float Speed = 1; // Speed object is moving
    public float MaxDistanceToGoal = .1f; // How close does it have to be to the point to be considered at point
    #endregion //Public Variables

    #region Private Variables
    private IEnumerator<Transform> pointInPath; //Used to reference points returned from MyPath.GetNextPathPoint
    #endregion //Private Variables

    // (Unity Named Methods)
    #region Main Methods
    public void Start()
    {
        //Make sure there is a path assigned
        if (MyPath == null)
        {
            Debug.LogError("Movement Path cannot be null, I must have a path to follow.", gameObject);
            return;
        }

        //Sets up a reference to an instance of the coroutine GetNextPathPoint
        pointInPath = MyPath.GetNextPathPoint();
        Debug.Log(pointInPath.Current);
        //Get the next point in the path to move to (Gets the Default 1st value)
        pointInPath.MoveNext();
        Debug.Log(pointInPath.Current);
        
        //Make sure there is a point to move to
        if (pointInPath.Current == null)
        {
            Debug.LogError("A path must have points in it to follow", gameObject);
            return; //Exit Start() if there is no point to move to
        }

        //Set the position of this object to the position of our starting point
        transform.position = pointInPath.Current.position;
    }
    // Angular speed in radians per sec.
    public float speed = 1.0f;

    //Update is called by Unity every frame
    public void Update()
    {
        //Validate there is a path with a point in it
        if (pointInPath == null || pointInPath.Current == null)
        {
            return; //Exit if no path is found
        }

        if (Type == MovementType.MoveTowards) //If you are using MoveTowards movement type
        {
            //Move to the next point in path using MoveTowards
            transform.position =
                Vector3.MoveTowards(transform.position,
                                    pointInPath.Current.position,
                                    Time.deltaTime * Speed);
            
            // Determine which direction to rotate towards
            Vector3 targetDirection = pointInPath.Current.position - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = speed * Time.deltaTime;
            
            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            
            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);
            
            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else if (Type == MovementType.LerpTowards) //If you are using LerpTowards movement type
        {
            //Move towards the next point in path using Lerp
            transform.position = Vector3.Lerp(transform.position,
                                                pointInPath.Current.position,
                                                Time.deltaTime * Speed);
        }

        //Check to see if you are close enough to the next point to start moving to the following one
        //Using Pythagorean Theorem
        //per unity suaring a number is faster than the square root of a number
        //Using .sqrMagnitude 
        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal) //If you are close enough
        {
            pointInPath.MoveNext(); //Get next point in MovementPath
        }
        //The version below uses Vector3.Distance same as Vector3.Magnitude which includes (square root)
        /*
        var distanceSquared = Vector3.Distance(transform.position, pointInPath.Current.position);
        if (distanceSquared < MaxDistanceToGoal) //If you are close enough
        {
            pointInPath.MoveNext(); //Get next point in MovementPath
        }
        */
    }
    #endregion //Main Methods

    //(Custom Named Methods)
    #region Utility Methods 

    #endregion //Utility Methods

    //Coroutines run parallel to other fucntions
    #region Coroutines

    #endregion //Coroutines
}
