using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Gears gear;

    public enum Gears {
        Idle = 0,
        First = 1,
        Second = 2,
        Hyper = 3,
    }

    public void Start()
    {
        gear = Gears.Idle;
    }

    // Update is called once per frame
    public void Update()
    {
        Transmission();
    }

    private void Transmission() {
        switch (gear)
        {
            case Gears.Idle:
                Debug.Log("Idle");
                break;
            case Gears.First:
                Debug.Log("First");
                break;
            case Gears.Second:
                Debug.Log("Second");
                break;
            case Gears.Hyper:
                Debug.Log("Hyper");
                break;
            default:
            break;
        }
    }
}
