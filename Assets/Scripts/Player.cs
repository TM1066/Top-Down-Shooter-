using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
        //healthTextObject.transform.position = this.transform.position - new Vector3(0, 2.5f, 0);

        
    }

    void LateUpdate()
    {
        //Keeping Camera on Player
        if (GlobalVariables.cameraFollowingPlayer)
        {
            Camera.main.transform.position = this.transform.position + new Vector3(0, 0, -10);
        }
    }
    //for after shooting
    public void DelayCameraFollowingPlayer(float delayDuration, float lerpDuration)
    {
        StartCoroutine(CameraLerp(delayDuration, lerpDuration));
    }

    private IEnumerator CameraLerp(float delayDuration, float lerpDuration)
    {
        yield return new WaitForSeconds(delayDuration);

        float timeElapsed = 0;

        Vector3 fromVector = Camera.main.transform.position;

        while (timeElapsed < lerpDuration) 
        {
            Camera.main.transform.position = Vector3.Slerp(fromVector, this.transform.position, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        GlobalVariables.cameraFollowingPlayer = true;
    }
}
