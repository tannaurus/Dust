using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    private Vector3 lastKnown;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        lastKnown = transform.position;
    }

    void Update()
    {
        bool forward = Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.D);
        bool running = Input.GetKey(KeyCode.LeftShift);

        if (forward & running & !anim.GetBool("Running")) 
        {
            anim.SetBool("Running", true);
        }
        if ((!forward | !running) & anim.GetBool("Running")) 
        {
            anim.SetBool("Running", false);
        }

        if (forward & !anim.GetBool("Walking"))
        {
            anim.SetBool("Walking", true);
        }
        if (!forward & anim.GetBool("Walking")) 
        {
            anim.SetBool("Walking", false);
        }

        if (forward)
        {
            lastKnown = transform.position;
        }
    }
}
