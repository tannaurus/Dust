using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform reachHand;
    public Transform hand;
    public Transform head;
    public int reachDistance = 5;
    public int throwForce = 3;

    private bool canHold = true;
    private GameObject inHold;

    // Update is called once per frame
    void Update()
    {
        InputListener();
        UpdateHoldItem();
    }

    void InputListener()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnHold();
        }
    }

    void UpdateHoldItem()
    {
        if (canHold) return;

        inHold.transform.position = Vector3.Lerp(inHold.transform.position, hand.position, 0.5f);
    }

    bool IsItem(Collider col)
    {
        return col.gameObject.tag == "Item";
    }

    void OnHold() 
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!canHold) {
            Drop();
        }
        else if (Physics.Raycast(ray, out hit, reachDistance))
        {
            if (IsItem(hit.collider))
            {
                Hold(hit.collider);
            }
        }
    }

    void Hold(Collider col)
    {
        inHold = col.gameObject;
        inHold.transform.parent = hand;
        Rigidbody holdRb = inHold.GetComponent<Rigidbody>();
        holdRb.useGravity = false;
        holdRb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        canHold = false;
    }

    void Drop()
    {
        inHold.transform.parent = null;
        Rigidbody holdRb = inHold.GetComponent<Rigidbody>();
        holdRb.useGravity = true;
        holdRb.constraints = RigidbodyConstraints.FreezePositionY & RigidbodyConstraints.FreezePositionX & RigidbodyConstraints.FreezePositionZ;
        holdRb.AddForce(transform.forward * throwForce);
        canHold = true;
    }
}
