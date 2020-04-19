using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    bool blast;

    GameObject fire;
    public GameObject Fire;

    void Start()
    {
        StartCoroutine(Blast());
        StartCoroutine(Destruct());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && blast) {

            fire = Instantiate(Fire, transform.GetChild(0));

            Destroy(fire, 10f);
            Destroy(gameObject);
        }
    }

    IEnumerator Blast() {

        yield return new WaitForSeconds(2);

        blast = true;

    }

    IEnumerator Destruct() {

        yield return new WaitForSeconds(5);

        fire = Instantiate(Fire, transform.GetChild(0).position, Quaternion.identity);

        Destroy(fire, 10f);
        Destroy(gameObject);

    } 
}
