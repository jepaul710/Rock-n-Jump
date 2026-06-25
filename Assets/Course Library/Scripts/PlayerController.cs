using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Objects from components to modify physics, visuals and sound

    private PlayerInput playerInput;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    // Public modifiers to get the VFS/SFX files

    public ParticleSystem explotionParticles;
    public ParticleSystem dirtParticles;
    public ParticleSystem moneyParticles;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    // Variables playing with the physics and logic

    private float thrust = 10f;
    private float maxHeight = 7f;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool isGameOver = false;
    public bool isJumping = false;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    void Update() // Keep the input system in Update for performance
    {
        // Conditional to get player input and set jump animation

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isJumping = true;
            playerAnim.SetTrigger("Jump_trig");
        }

        // Conditional to keep the player on scene when jumping too high

        if (transform.position.y > maxHeight)
        {
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0, playerRb.linearVelocity.z);
            transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        // Conditional to get collision with cone so stop game, visuals and sound

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            Debug.Log("Game Over!");

            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            playerAudio.PlayOneShot(crashSound, 1.0f);
            dirtParticles.Stop();
            explotionParticles.Play();
            
        }

        // Conditional to get collision with barrier so stop game, visuals and sound

        if (collision.gameObject.CompareTag("Barrier"))
        {
            isGameOver = true;
            Debug.Log("Game Over!");

            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            playerAudio.PlayOneShot(crashSound, 1.0f);
            dirtParticles.Stop();
            explotionParticles.Play();

        }

        // Conditional to play dirt animation when on ground

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticles.Play();

            // Conditional to stop dirt animation if the player dies in air

            if (isGameOver == true)
            {
                dirtParticles.Stop();
            }
        }
    }

    void FixedUpdate() // Keep the game physics in FixedUpdate for appliance
    {
        // Conditional to apply jump force, sound effect and stop dirt animation

        if (isJumping && !isGameOver)
        {
            playerRb.AddForce(Vector3.up * thrust, ForceMode.Impulse);
            isJumping = false;
            isOnGround = false;
            dirtParticles.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {

        // Conditional to get trigger destroy when collecting money

        if (collider.gameObject.CompareTag("Money"))
        {
            Destroy(collider.gameObject);
            moneyParticles.Play();
        }

        // Conditional to get trigger destroy bomb and stop game

        if (collider.gameObject.CompareTag("Bomb"))
        {

            Destroy(collider.gameObject);

            isGameOver = true;
            gravityModifier = 1000;
            Debug.Log("Game Over!");

            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            playerAudio.PlayOneShot(crashSound, 1.0f);
            explotionParticles.Play();
        }
    }
}
