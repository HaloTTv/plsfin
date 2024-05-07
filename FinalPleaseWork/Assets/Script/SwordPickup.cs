using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    public Transform playerHand;
    public Transform originalPosition;
    public Animator playerAnimator; // Reference to the player's animator
    public Collider swordCollider; // Collider used for damage calculation
    private bool isHeld = false;

    void Start()
    {
        swordCollider.enabled = false; // Disable the collider initially
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isHeld && Vector3.Distance(transform.position, playerHand.position) < 2f)
        {
            PickUp();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && isHeld)
        {
            Drop();
        }
    }

    void PickUp()
    {
        isHeld = true;
        transform.SetParent(playerHand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true;
        swordCollider.enabled = true; // Enable the collider when picked up
        playerAnimator.SetBool("IsHoldingSword", true); // Trigger the holding sword animation
    }

    void Drop()
    {
        isHeld = false;
        transform.SetParent(null);
        transform.position = originalPosition.position;
        GetComponent<Rigidbody>().isKinematic = false;
        swordCollider.enabled = false; // Disable the collider when dropped
        playerAnimator.SetBool("IsHoldingSword", false); // End the holding sword animation
    }
}
