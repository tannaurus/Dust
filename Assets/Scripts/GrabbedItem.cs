using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbedItem : MonoBehaviour
{
    public Collider player;

    public void Init()
    {
        Physics.IgnoreCollision(player, GetComponent<Collider>(), true);
    }

    public void Reset() 
    {
        Physics.IgnoreCollision(player, GetComponent<Collider>(), false);
    }
}
