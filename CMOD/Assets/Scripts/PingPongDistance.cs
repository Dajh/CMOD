using UnityEngine;
using System.Collections;

public class PingPongDistance : MonoBehaviour {


    //Direction to move
    public Vector3 MoveDir = Vector3.zero;

    //Speed to move - units per second
    public float Speed = 0.0f;

    //Distance to travel in world units (before inverting direction and turning back
    public float TravelDistance = 0.0f;

    //Cache Transform
    private Transform ThisTransform = null;

	// Use this for initialization
    //---------------------------------------------------------------------------------------------------------
	IEnumerator Start () {
        
        //Get cached transform
        ThisTransform = transform;

        //Loop forever
        while(true)
        {
            //Invert direction
            MoveDir = MoveDir * -1;

            //Start Movement
            yield return StartCoroutine(Travel());

        }
	}

    //Travel full distancce in direcion, from current position
    //---------------------------------------------------------------------------------------------------------
    IEnumerator Travel()
    {
        //Distance travelled so far
        float DistanceTravelled = 0;

        //Move
        while(DistanceTravelled < TravelDistance)
        {
            //Get new position based on speed and direction
            Vector3 DistToTravel = MoveDir * Speed * Time.deltaTime;

            //Update Position
            ThisTransform.position += DistToTravel;

            //Update distance to travelled so far
            DistanceTravelled += DistToTravel.magnitude;

            //Wait until next update
            yield return null;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
