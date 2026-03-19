using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CharacterController playerGoat;
    private Vector3 direction;
    public float gravity = -11f;
    public float jumpStrength = 6f;
    InputAction jumpAction;
    InputAction invertGravityAction;
    private Animator animator; 
    private void Awake()
    {
        // This method is called when the script instance is being loaded
        playerGoat = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        jumpAction = InputSystem.actions.FindAction("Jump");
        invertGravityAction = InputSystem.actions.FindAction("Interact");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        direction = Vector3.zero;
        jumpAction.Enable();
        invertGravityAction.Enable();
        gravity = -11f; //reset gravity to default when enabled
        jumpStrength = 6f; //reset jump strength to default when enabled
        transform.rotation = Quaternion.identity; //reset rotation to default when enabled
    }

    // Update is called once per frame
    void Update()
    {
        //apply gravity
        direction += Vector3.up * gravity * Time.deltaTime; //vector * gravity * time elapsed
        if (playerGoat.isGrounded || playerGoat.collisionFlags == CollisionFlags.Above) //check if grounded or hitting ceiling
        {
            animator.SetBool("IsRunning", true); //set grounded animation
            direction = Vector3.up * Mathf.Sign(gravity);; //reset the y direction when grounded
            if (jumpAction.triggered)
            {
                direction = Vector3.up * jumpStrength; //jump
            }
            if (invertGravityAction.triggered)
            {
                Debug.Log("Gravity Inverted!"); //log gravity inversion
                gravity *= -1; //invert gravity
                jumpStrength *= -1; //invert jump strength to match gravity
                //if gravity is positive, "flip"/rotate the goat upside down, otherwise reset rotation
                transform.rotation = Quaternion.Euler(0, gravity > 0 ? 180 : 0, gravity > 0 ? 180 : 0);
            }
        } else
        {
            animator.SetBool("IsRunning", false); //set idle animation
        }
        playerGoat.Move(direction * Time.deltaTime); //make the goat move
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.Gameover(); //end the game if obstacle touched
        }
        if (other.CompareTag("Collectible"))
        {
            GameManager.Instance.AddScore(); //add score if collectible touched
            Destroy(other.gameObject); //"consume" the collectible
        }
    }
}
