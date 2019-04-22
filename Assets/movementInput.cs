using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class movementInput : MonoBehaviour
{
    [SerializeField] float deadZone = 20;
    Animator animator;
    SpriteRenderer rend;
    bool grounded = true;
    bool flipped = false;
    Collider2D[] cArray;
    int lives = 3;
    int coins = 0;
    int enemies = 0;
    bool camCentered = true;
    bool camMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        master.machineOn = true;

        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        cArray = GetComponents<BoxCollider2D>();

        StartCoroutine("MoveCamera");

    }
    void ReStart()
    {
        if (--lives <= 0)
            SceneManager.LoadScene("SampleScene");
        else
            animator.SetBool("Hurting", false);

        GameObject.Find("Lives").GetComponent<Text>().text = "Lives: " + lives;
    }

    IEnumerator MoveCamera() {

        do
        {
            Vector3 oldPos = Camera.main.transform.position;
            Vector3 temp;

            temp = transform.position - oldPos;
            temp.z = 0f;

            if (temp.magnitude > 4)
            {
                for (float t = 0f; t < 1.0f; t += .025f)
                {
                    Vector3 newPos = transform.position;
                    newPos.z = oldPos.z;

                    Camera.main.transform.position = Vector3.Lerp(oldPos, newPos, t);

                    yield return new WaitForSeconds(.025f);
                }
            }
            yield return new WaitForSeconds(.25f);
        } while (true);
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit2D foo = Physics2D.Linecast(transform.position, GameObject.Find("underneath").transform.position, LayerMask.GetMask("Ground"));


        if (GameObject.Find("Possum") != null)
        {
            RaycastHit2D foo2 = Physics2D.Linecast(transform.position, GameObject.Find("Possum").transform.position, ~(LayerMask.GetMask("Player") | LayerMask.GetMask("MobCol")));

            //if (foo2 != null)
            { 
                if (foo2.collider.name == "Possum")
                {
                    GameObject.Find("Possum").GetComponent<possum>().speed = 4f;
                    Debug.Log("The possum sees me!");
                } else
                {
                    GameObject.Find("Possum").GetComponent<possum>().speed = 2f;
                    Debug.Log("The possum can't see me." + foo2.collider.name);
                }
            }
        }

        //if (foo.collider != null)
        //    Debug.Log(foo.collider.name);


        Vector3 myVel = GetComponent<Rigidbody2D>().velocity;

        if (transform.position.magnitude > deadZone)
        {
            animator.SetBool("Hurting", true);
            ReStart();
            GetComponent<Rigidbody2D>().gravityScale = 1f;
            rend.flipY = false;
            transform.position = new Vector3(-2.5f, -1.75f, 0);
        }

            /*
            if (Mathf.Abs(myVel.y) > .01)
            {
                animator.SetBool("Jumping", true);
                grounded = false;
            }
            */

            if (grounded)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                animator.SetBool("Running", true);
                rend.flipX = false;
                myVel.x = 2f;

                GetComponent<Rigidbody2D>().velocity = myVel;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                myVel.x = -2f;
                animator.SetBool("Running", true);
                rend.flipX = true;

                GetComponent<Rigidbody2D>().velocity = myVel;
            }
            else
                animator.SetBool("Running", false);
        }

        if (Input.GetKeyDown(KeyCode.Space ))
        {
            if (grounded)
            {
                //animator.SetBool("Jumping", true);
                //animator.SetBool("Running", false);
                GetComponent<Rigidbody2D>().AddForce(transform .up * 50f * GetComponent<Rigidbody2D>().gravityScale);

                //grounded = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            coins++;
            Destroy(collision.gameObject);
            GameObject.Find("Coins").GetComponent<Text>().text = "Coins: " + coins;
            return;
        }
        grounded = true;
        animator.SetBool("Jumping", false);

        if (collision.gameObject.layer == 10)
        {
            if (collision.otherCollider == cArray[0])
            {
                animator.SetBool("Hurting", true);
                Invoke("ReStart", 3f);
            }
            else
            {
                Destroy(collision.gameObject);
                enemies++;
                GameObject.Find("Enemies").GetComponent<Text>().text = "Enemies: " + enemies;
            }
        }

        if (collision.gameObject.layer == 8)
            {
            if (collision.otherCollider  == cArray [2])
            {
                Debug.Log("collided with cArray[2]");
                animator.SetBool("Jumping", false);
                GetComponent<Rigidbody2D>().gravityScale = 1f;
                rend.flipY = false;
            }
            else if (collision.otherCollider == cArray[1])
            {
                animator.SetBool("Jumping", false);
                rend.flipY = true;
                GetComponent<Rigidbody2D>().gravityScale = -1f;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Vector3 myVel = GetComponent<Rigidbody2D>().velocity;

        //Debug.Log("exited");

        if (Mathf.Abs(myVel.y) > .01)
        {
            animator.SetBool("Jumping", true);
            animator.SetBool("Running", false);
            grounded = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {

    }
}
