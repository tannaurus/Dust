using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform reach;
    public Transform grab;
    public Canvas UI;

    public int reachDistance = 5;
    public int throwForce = 3;

    private bool canReach = true;
    private bool canGrab = true;
    private GameObject inReach;
    private GameObject inGrab;

    // Update is called once per frame
    void Update()
    {
        InputListener();
        UpdateItemsPos();
    }

    void InputListener()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnReach();
        }
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            OnGrab();
        }
    }

    void UpdateItemsPos()
    {
        if (!canReach)
        {
            inReach.transform.position = Vector3.Lerp(inReach.transform.position, reach.position, 0.5f);
        }
        if (!canGrab) 
        {
            inGrab.transform.position = Vector3.Lerp(inGrab.transform.position, grab.position, 0.5f);
            inGrab.transform.rotation = Quaternion.Lerp(inGrab.transform.rotation, grab.transform.rotation * Quaternion.identity, 10 * Time.deltaTime);
        }

    }

    bool IsItem(Collider col)
    {
        return col.gameObject.tag == "Item";
    }

    void OnGrab()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!canGrab)
        {
            DropGrab();
        }
        else if (Physics.Raycast(ray, out hit, reachDistance))
        {
            if (IsItem(hit.collider))
            {
                Grab(hit.collider);
            }
        }
    }

    void Grab(Collider col)
    {
        inGrab = col.gameObject;
        inGrab.transform.parent = grab;
        GrabbedItem itemScript = inGrab.AddComponent<GrabbedItem>();
        // itemScript.player = hand;
        itemScript.Init();
        Rigidbody grabRb = inGrab.GetComponent<Rigidbody>();
        grabRb.useGravity = false;
        grabRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        canGrab = false;
    }


    void OnReach() 
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!canReach) {
            DropReach();
        }
        else if (Physics.Raycast(ray, out hit, reachDistance))
        {
            if (IsItem(hit.collider))
            {
                Reach(hit.collider);
            }
        }
    }

    void Reach(Collider col)
    {
        inReach = col.gameObject;
        inReach.transform.parent = reach;
        Rigidbody holdRb = inReach.GetComponent<Rigidbody>();
        holdRb.useGravity = false;
        holdRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        canReach = false;
    }

    void DropReach()
    {
        inReach.transform.parent = null;
        Rigidbody holdRb = inReach.GetComponent<Rigidbody>();
        holdRb.useGravity = true;
        holdRb.constraints = RigidbodyConstraints.FreezePositionY & RigidbodyConstraints.FreezePositionX & RigidbodyConstraints.FreezePositionZ;
        holdRb.AddForce(transform.forward * throwForce);
        canReach = true;
        inReach = null;
    }

    void DropGrab()
    {
        inGrab.transform.parent = null;
        Rigidbody grabRb = inGrab.GetComponent<Rigidbody>();
        grabRb.useGravity = true;
        grabRb.constraints = RigidbodyConstraints.FreezePositionY & RigidbodyConstraints.FreezePositionX & RigidbodyConstraints.FreezePositionZ;
        grabRb.AddForce(transform.forward * throwForce);
        GrabbedItem itemScript = inGrab.GetComponent<GrabbedItem>();
        itemScript.Reset();
        Destroy(itemScript);
        canGrab = true;
        inGrab = null;
    }
}
