using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crank : MonoBehaviour
{
    Animator animator;
    bool crankTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && crankTrigger)
            animator.SetBool("On", master.machineOn ^= true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        crankTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        crankTrigger = false;
    }
}
