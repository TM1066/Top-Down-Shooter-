using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 4;
    public float startSize = 1;

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
}
