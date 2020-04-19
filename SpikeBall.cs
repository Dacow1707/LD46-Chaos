using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    Rigidbody2D rb;

    public GameObject impactEffect;

    bool down = true;

    Vector2 move;

    public float verticalSpeed;
    public float horizontalSpeed;
    float count = -0.4f;
    public float num;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (transform.position.x > 0)
        {
            move.x = -horizontalSpeed;
        }
        else {
            move.x = horizontalSpeed;
        }
    }

    void FixedUpdate()
    {
        if (down) {
            move.y = -verticalSpeed * Time.deltaTime;
            count += 1 / num;
            if (count >= 1) {
                down = false;
                count = 0;
            }
        }

        if (!down)
        {
            move.y = verticalSpeed * Time.deltaTime;
            count += 1 / num;
            if (count >= 1)
            {
                down = true;
                count = 0;
            }
        }

        rb.velocity = move;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            FindObjectOfType<Player>().UpdateScore(2);
            Destroy(gameObject);
        }

        if (collision.tag == "Border") {
            Destroy(gameObject);
        }
    }
}
