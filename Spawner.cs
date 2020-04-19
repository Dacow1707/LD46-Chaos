using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{

    Vector3 pos;
    Vector3 pos_;

    public GameObject Exclamation;
    public GameObject Spike;
    public GameObject Target;

    [HideInInspector]public float spawnInterval = 2;

    bool spawn = true;
    bool spawn_ = true;
    bool spawn__ = true;

    [HideInInspector] public bool isScoreGreaterThanFive;

    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();         
    }

    void Update()
    {
        if (player.enabled)
        {

            if (spawn)
            {
                StartCoroutine(SpawnEnemy());
            }

            if (spawn_)
            {
                StartCoroutine(SpawnSpike());
            }

            if (isScoreGreaterThanFive && spawn__)
            {
                StartCoroutine(SpawnTarget());
            }
        }

    }

    IEnumerator SpawnTarget()
    {
        spawn__ = false;

        yield return new WaitForSeconds(spawnInterval * 8);

        pos.x = Random.Range(-6.5f,6.5f);
        pos.y = Random.Range(-3f, 3f);

        Instantiate(Target, pos, Quaternion.identity);

        spawn__ = true;
    }

    IEnumerator SpawnEnemy() {

        spawn = false;

        yield return new WaitForSeconds(spawnInterval);

        Instantiate(Exclamation, transform.position, Quaternion.identity);

        spawn = true;
    }

    IEnumerator SpawnSpike() {

        spawn_ = false;

        yield return new WaitForSeconds(spawnInterval * 5);

        pos.x = Random.Range(-7.5f, 7.5f);
        pos.y = transform.GetChild(0).position.y;

        Instantiate(Spike, pos, Quaternion.identity);

        spawn_ = true;
    }

    public void Retry()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void MuteButton()
    {

        if (player.GetComponent<AudioSource>().volume == 0)
        {

            player.GetComponent<AudioSource>().volume = 0.2f;

        }
        else  {

            player.GetComponent<AudioSource>().volume = 0;

        }

    }

    public void Quit()
    {

        Application.Quit();

    }
}
