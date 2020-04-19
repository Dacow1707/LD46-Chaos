using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exclamation : MonoBehaviour
{

    bool follow = true;
    bool True = true;

    public float upConstant;
    public float rightConstant;
    public float downConstant;
    public float leftConstant;

    Player player;

    GameObject up;
    GameObject right;
    GameObject down;
    GameObject left;

    GameObject arrow;
    GameObject missile;

    public GameObject Arrow;
    public GameObject Background;
    public GameObject Missile;

    Rigidbody2D rb;

    Vector2 position;

    int whereto;

    void Start()
    {
        player = FindObjectOfType<Player>();

        rb = GetComponent<Rigidbody2D>();

        up = transform.GetChild(0).gameObject;
        right = transform.GetChild(1).gameObject;
        down = transform.GetChild(2).gameObject;
        left = transform.GetChild(3).gameObject;

        whereto = Random.Range(1, 5);

        if (whereto == 1)
        {
            position.y = upConstant;
            position.x = player.transform.position.x;
            arrow = Instantiate(Arrow, up.transform);
        }
        else if (whereto == 2)
        {
            position.y = player.transform.position.y;
            position.x = rightConstant;
            arrow = Instantiate(Arrow, right.transform);
        }
        else if (whereto == 3)
        {
            position.y = downConstant;
            position.x = player.transform.position.x;
            arrow = Instantiate(Arrow, down.transform);
        }
        else if (whereto == 4)
        {
            position.y = player.transform.position.y;
            position.x = leftConstant;
            arrow = Instantiate(Arrow, left.transform);
        }

        rb.position = position;

        StartCoroutine(StopFollow());
        StartCoroutine(Destruct());

    }


    void Update()
    {
        if (True)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            arrow.GetComponent<SpriteRenderer>().enabled = true;
            True = false;
        }

        if (follow == true)
        {
            if (whereto % 2 == 1)
            {
                position.x = player.transform.position.x;
            }
            else if (whereto % 2 == 0)
            {
                position.y = player.transform.position.y;
            }

            rb.position = position;
        }
    }

    IEnumerator StopFollow() {

        yield return new WaitForSeconds(3) ;

        Instantiate(Background, transform);

        follow = false;

    }

    IEnumerator Destruct() {

        yield return new WaitForSeconds(4);

        missile = Instantiate(Missile, transform.GetChild(whereto + 3).position, Quaternion.identity);
        missile.GetComponent<missile>().Direction = whereto;

        Destroy(gameObject);

    }
}
