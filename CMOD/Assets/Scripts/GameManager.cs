using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NotificationManager))] //Component for sending and reveiving notification

public class GameManager : MonoBehaviour {
    //C# property to retrieve currently active instance of object, if any
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                //Create Game Manager object if required
                instance = new GameObject("GameManager").AddComponent<GameManager>();
                return instance;
            }
            return instance;
        }
    }

    //C# property to retrieve notifications manager
    public static NotificationManager Notifications
    {
        get
        {
            if(notifications == null)
            {
                notifications = instance.GetComponent<NotificationManager>();
                return notifications;
            }
            return notifications;
        }
    }

    //Internal reference to notification object
    private static NotificationManager notifications = null;


    //Internal reference to single active instance of object - for singleton behaviour
    private static GameManager instance = null;

    //Calledd before start on object creation
    void Awake()
    {
        //Check if there is an existing instance of this object
        if((instance) && (instance.GetInstanceID() != GetInstanceID()))
        {
            //Delete duplicate
            DestroyImmediate(gameObject);
        }
        else
        {
            
            instance = this; //Make this objet the only instance
            DontDestroyOnLoad(gameObject); //Set as do not destroy
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
