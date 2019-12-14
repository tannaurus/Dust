using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbedItem : MonoBehaviour
{
    public GameObject UI;

    public void Init()
    {
        // To-do: reinclude below lines once player grab animation is in place
        // Physics.IgnoreCollision(player, GetComponent<Collider>(), true);
    }

    public void Reset() 
    {
        // Physics.IgnoreCollision(player, GetComponent<Collider>(), false);
    }
}
