using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class possum : MonoBehaviour
{
    float dir = -1f;
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if (master.machineOn)
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed*dir, 0);
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        dir *= -1f;
        GetComponent<SpriteRenderer>().flipX ^= true;
    }
}
