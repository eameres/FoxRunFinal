using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class possum : MonoBehaviour
{
    float dir = -1f;
    public float speed = 2f;
    [SerializeField]
    bool eagle;

    // Start is called before the first frame update
    void Start()
    {
    }

    bool CheckCast()
    {
        Vector2 childTrans = transform.position;
        childTrans.x += 1f;

        RaycastHit2D foo = Physics2D.Raycast(childTrans, -transform.up, 3f); // what do we hit when we are "infront" of the possum?

        childTrans.x -= 2f;
        RaycastHit2D foo2 = Physics2D.Raycast(childTrans, -transform.up, 3f);// what do we hit when we are "in back" of the possum?


        if ((foo.collider == null) || (foo2.collider == null)) // if either was null, then we reached an edge
            return true;

        if (foo.collider != foo2.collider) // if they don't match, we reached an edge
        {
            return true;
        }
        return false;  // if they're equal, we are still "on" the platform
    }


    // Update is called once per frame
    void Update()
    {
        if (!eagle)
        {
            if (CheckCast())
            {
                dir *= -1f;
                GetComponent<SpriteRenderer>().flipX ^= true;
            }
        }

        if (master.machineOn)
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed*dir, 0);
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (eagle)
        {
            dir *= -1f;
            GetComponent<SpriteRenderer>().flipX ^= true;
        }
    }
}
