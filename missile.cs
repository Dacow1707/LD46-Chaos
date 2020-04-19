using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : MonoBehaviour
{

    bool update = true;

    [HideInInspector] public float Direction;
    public float Speed;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        if (Direction == 1)
        {
            rb.velocity = transform.up * Speed * Time.fixedDeltaTime;
            rb.rotation = 180;
        }
        else if (Direction == 2)
        {
            rb.velocity = transform.up * Speed * Time.fixedDeltaTime;
            rb.rotation = 90;
        }
        else if (Direction == 3)
        {
            rb.velocity = transform.up * Speed * Time.fixedDeltaTime;
        }
        else if (Direction == 4) {
            rb.velocity = transform.up * Speed * Time.fixedDeltaTime;
            rb.rotation = -90;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Border" && update) {
            FindObjectOfType<Player>().UpdateScore(1);
            update = false;
            StartCoroutine(Destruct());
        }
    }

    IEnumerator Destruct() {

        yield return new WaitForSeconds(3);

        Destroy(gameObject);

    }

}
