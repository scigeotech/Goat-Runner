using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CharacterController playerGoat;
    private Vector3 direction;
    public float gravity = -9.81f;
    public float jumpStrength = 5f;
    InputAction jumpAction;
    private Animator animator; 
    private void Awake()
    {
        // This method is called when the script instance is being loaded
        playerGoat = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        jumpAction = InputSystem.actions.FindAction("Jump");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        direction = Vector3.zero;
        jumpAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //apply gravity
        direction += Vector3.up * gravity * Time.deltaTime; //vector * gravity * time elapsed
        if (playerGoat.isGrounded)
        {
            animator.SetBool("IsRunning", true); //set grounded animation
            direction = Vector3.down; //reset the y direction when grounded
            if (jumpAction.triggered)
            {
                direction = Vector3.up * jumpStrength; //jump
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
