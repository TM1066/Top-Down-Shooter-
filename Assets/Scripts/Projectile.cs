using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 4;
    public float startSize = 1;
    public GameObject shooterObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ShrinkAndDissapear());
    }

    private IEnumerator ShrinkAndDissapear()
    {
        float timeElapsed = 0f;

        while (timeElapsed < lifeTime)
        {
            if (this)
            {
                this.gameObject.transform.localScale = Vector2.Lerp(new Vector2(startSize, startSize), new Vector2(0, 0), timeElapsed / lifeTime);

                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Code to handle removing player health (Scale with size)
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().health -= 5 * this.transform.localScale.x;
            Debug.Log($"Player Shot for {5 * this.transform.localScale.x} damage");
        }

        //Destroy(this.gameObject);
    }
}
