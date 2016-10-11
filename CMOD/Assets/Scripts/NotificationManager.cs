using UnityEngine;
using System.Collections;
using System.Collections.Generic;   


public class NotificationManager : MonoBehaviour
{
    //Internal reference to all listeners for notifications
    private Dictionary<string, List<Component>> Listeners = new Dictionary<string, List<Component>>();

    //N is an event name: such as "OnHealthChange" or "OnLevelComplete"
    //List MyListenersList = Listeners[N];

    //Function to add a listener for a notification to the listeners list
    public void AddListener(Component _Listener, string _NotificationName)
    {
        //Add listener to dictionary
        if(!Listeners.ContainsKey(_NotificationName))
        {
            Listeners.Add(_NotificationName, new List<Component>());
        }
        //Add object to listener list for this notification
        Listeners[_NotificationName].Add(_Listener);
    }

    //Function to post a notification to a listener
    public void PostNotification(Component _Listener, string _NotificationName)
    {
        if(!Listeners.ContainsKey(_NotificationName))
        {
            return;
        }
        //Else post notification to all matching listeners
        else
        {
            foreach(Component Listener in Listeners[_NotificationName])
            {
                Listener.SendMessage(_NotificationName, _Listener, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public void RemoveListener(Component _Sender, string _NotificationName)
    {
        //If no key in dictionary exists, then exit
        if(!Listeners.ContainsKey(_NotificationName))
        {
            return;
        }
        //Cycle through listeners and identify component, and then remove
        else
        {
            for(int i = Listeners[_NotificationName].Count-1; i>=0; i--)
            {
                //Check instance ID
                if(Listeners[_NotificationName][i].GetInstanceID() == _Sender.GetInstanceID())
                {
                    Listeners[_NotificationName].RemoveAt(i); //Matched remove from list
                }
            }
        }
    }

    //Function to remove redundant listeners - deleted and removed listeners
    public void RemoveRedundancies()
    {
        //Create new dictionary
        Dictionary<string, List<Component>> TmpListeners = new Dictionary<string, List<Component>>();

        //Cycle throught all dictionary entries
        foreach(KeyValuePair<string, List<Component>> Item in Listeners)
        {
            //Cycle through all listener objects in list, remove null objects
            for(int i = Item.Value.Count-1; i>=0; i--)
            {
                //If null, then remove item
                if(Item.Value[i] == null)
                {
                    Item.Value.RemoveAt(i);
                }
            }
            //If items remain in list for this notification, then add  this to tmp dictionary 
            if(Item.Value.Count > 0)
            {
                TmpListeners.Add(Item.Key, Item.Value);
            }
        }

        //replace listeners objects with new, optimized dictionary
        Listeners = TmpListeners;
    }
}
