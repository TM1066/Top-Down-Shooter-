using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Range(1,100)]
    [SerializeField] float speedValue = 25f;
    [SerializeField] Rigidbody2D playerRig;
    [SerializeField] InputActionAsset inputActionAsset;
    private InputAction moveAction;

    void Start()
    {
        var playerActionMap = inputActionAsset.FindActionMap("Player");
        moveAction = playerActionMap.FindAction("Movement");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementInput = moveAction.ReadValue<Vector2>();
        Debug.Log($"Movement Vector: {movementInput}");

        playerRig.AddForce(movementInput * speedValue);
    }
}
