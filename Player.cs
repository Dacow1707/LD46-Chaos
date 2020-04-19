using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;

    public float speed;

    public Text scoreText;

    [HideInInspector] public int score = 0;

    Vector2 move;
    Vector2 MousePosition;

    public GameObject GameOverUI;
    public GameObject impactEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        Time.timeScale = 1;

        move.x = Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime;
        move.y = Input.GetAxisRaw("Vertical") * speed * Time.fixedDeltaTime;

        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {

        rb.position += move;

        Vector2 LookDirection = MousePosition - rb.position;
        float Angle = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;
        rb.rotation = Angle;

        Mathf.Clamp(rb.position.y, -5.5f, 5.5f);
        Mathf.Clamp(rb.position.x, -11.5f, 11.5f);

    }

    public void UpdateScore(int points) {

        if (enabled)
        {
            score += Mathf.Abs(points);
            scoreText.text = "Score: " + score.ToString();
            scoreText.GetComponent<Animator>().SetTrigger("Update");
        }

        if (score > 9) {
            FindObjectOfType<Spawner>().spawnInterval = 1.5f;
            FindObjectOfType<Spawner>().isScoreGreaterThanFive = true;
        }
        if (score > 19)
        {
            FindObjectOfType<Spawner>().spawnInterval = 1f;
        }
        if (score > 29)
        {
            FindObjectOfType<Spawner>().spawnInterval = 0.75f;
        }
        if (score > 39)
        {
            FindObjectOfType<Spawner>().spawnInterval = 0.5f;
        }
        if (score > 49)
        {
            FindObjectOfType<Spawner>().spawnInterval = 0.25f;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Missile" || collision.tag == "SpikeBall" || collision.tag == "Fire")
        {
            GameObject particle = Instantiate(impactEffect, transform.GetChild(0).position, Quaternion.identity);
            Destroy(particle, 2f);

            GameOverUI.SetActive(true);

            GameOverUI.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Score: " + score;

            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {

                PlayerPrefs.SetInt("HighScore", score);
                GameOverUI.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);

            }

            GameOverUI.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);

            GetComponent<SpriteRenderer>().enabled = false;
            enabled = false;

        }
    }

}
