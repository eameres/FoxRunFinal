using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeDelete : MonoBehaviour
{
    bool dropped = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShakeDel()
    {
        dropped = true;
        Vector2 pos = transform .position;

        for (float i = 0f; i < 2f; i += .05f)
        {
            pos += new Vector2(Random.Range(-.05f, .05f)*i, Random.Range(-.05f, .05f)*i);
            transform.position = pos;
            yield return new WaitForSeconds(.05f);
        }

        GetComponent<Rigidbody2D>().constraints =  RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        //Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Player") && !dropped)
            StartCoroutine ("ShakeDel");
    }
}
