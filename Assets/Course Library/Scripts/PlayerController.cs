using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Private modifiers to get the components of the player

    private PlayerInput playerInput;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    // Public modifiers to get the VFS/SFX/UI files

    public ParticleSystem explotionParticles;
    public ParticleSystem dirtParticles;
    public ParticleSystem moneyParticles;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public GameObject titlePanel;
    public GameObject scorePanel;

    // Variables playing with the physics and logic

    private float jump = 10f;
    private float maxHeight = 7f;
    public int score = 0;
    
    public bool isGameStarted = false;
    public bool isOnGround = true;
    public bool isGameOver = false;
    public bool isJumping = false;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        // Panels to show or hide the title, score and game over UI
        titlePanel.SetActive(true);
        scorePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        UpdateScoreText();

        Time.timeScale = 0f; 
    }

    void Update() // Keep the input system in Update for performance
    {
        if (!isGameStarted) return;

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
        // Conditionals to get collision with obstacles, play animations and sound effects

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            UpdateGameOverPanel();

            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            playerAudio.PlayOneShot(crashSound, 1.0f);
            dirtParticles.Stop();
            explotionParticles.Play();
            
        }

        if (collision.gameObject.CompareTag("Barrier"))
        {
            isGameOver = true;
            UpdateGameOverPanel();

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
            isJumping = false;
            
            if (isGameStarted && !isGameOver) dirtParticles.Play();

            // Conditional to stop dirt animation if the player dies

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
            playerRb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            isOnGround = false;
            isJumping = false;
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
            score += 100;
            UpdateScoreText();

            // Conditional to increase the speed of the obstacles when reaching a certain score

            if (score >= 2000) ObstacleMovement.objectSpeed = 30f;
            else if (score >= 1500) ObstacleMovement.objectSpeed = 25f;
            else if (score >= 1000) ObstacleMovement.objectSpeed = 20f;
            else if (score >= 500) ObstacleMovement.objectSpeed = 15f;
            else if (score >= 300) ObstacleMovement.objectSpeed = 12f;

        }

        // Conditional to get trigger destroy bomb and stop game

        if (collider.gameObject.CompareTag("Bomb"))
        {
            Destroy(collider.gameObject);

            isGameOver = true;
            UpdateGameOverPanel();

            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            playerAudio.PlayOneShot(crashSound, 1.0f);
            explotionParticles.Play();
        }
    }

    // Method to update the score text on the UI when collecting money
    public void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    // Method to update the game over panel when the player dies

    public void UpdateGameOverPanel()
    {
        if (isGameOver)
        {
            gameOverPanel.SetActive(true);
        }
    }

    // Method to start the game when the player presses the start button

    public void StartGame()
    {
        isGameStarted = true;
        titlePanel.SetActive(false);
        scorePanel.SetActive(true);

        Time.timeScale = 1f; 
    }

    // Method to restart the game when the player dies and presses the restart button

    public void RestartGame()
    {
        Time.timeScale = 1f; 

        Physics.gravity = new Vector3(0, -9.81f, 0); 
        ObstacleMovement.objectSpeed = 10f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
