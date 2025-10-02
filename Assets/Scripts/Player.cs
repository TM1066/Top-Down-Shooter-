using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 100;

    public TextMeshProUGUI healthTextObject;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        healthTextObject.text = $"HP: {health}/100";
        healthTextObject.transform.position = this.transform.position - new Vector3(0, 2.5f, 0);
    }
}
