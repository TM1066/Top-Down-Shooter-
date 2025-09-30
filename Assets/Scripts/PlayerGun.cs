using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public float shootPower;
    public Rigidbody2D playerBodyRig;

    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        // float rotationValue = Random.Range(0f, 360f);
        // this.transform.rotation = Quaternion.Euler(0, 0, rotationValue);

        // THIS CODE IS ONLY FOR THE PC BUILD, THE ARCADE BUILD WILL ONLY USE LEFT AND RIGHT DIRECTIONS FOR ROTATING!!!!

        // Get Mouse position in world
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // stay in 2D plane

        // Direction from center player body to mouse
        Vector3 direction = mousePos - playerBodyRig.transform.position;

        // Get the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation around Z axis only
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    //can make an IEnumerator or just incorporate a stopwatch and compare ticks to slow fire rate
    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //calculating the direction away from player, using negative for kickback force
            Vector2 direction = (playerBodyRig.transform.position - projectileSpawnPoint.position).normalized;

            for (int i = 0; i < 5; i++)
            {
                Debug.Log("Shooting Projectile!!!!!!! BOMBS AWAY");
                var projectile = Instantiate(projectilePrefab, projectileSpawnPoint);
                projectile.transform.SetParent(null);
                projectile.GetComponent<Rigidbody2D>().AddForce(shootPower * ( -direction + new Vector2(Random.Range(0f, 5f),Random.Range(0f, 5f))), ForceMode2D.Impulse);
            }
            // Push the Player away from the barrel shooting
            playerBodyRig.AddForce(shootPower * direction, ForceMode2D.Impulse);
        }
    }
}
