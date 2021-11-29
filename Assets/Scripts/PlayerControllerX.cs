using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    public float gravityModifier = 1.5f;
    private Rigidbody _playerRb;
    private const float LowerBound = 1.0f;
    private const float UpperBound = 15.0f;
    private bool outOfBounds = true;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource _playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;


    // Start is called before the first frame update
    private void Start()
    {
        _playerAudio = GetComponent<AudioSource>();
        _playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        fireworksParticle.transform.position = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.y > UpperBound)
        {
            outOfBounds = true;

            transform.position = new Vector3(transform.position.x, UpperBound, transform.position.z);
            _playerRb.velocity = Vector3.zero;
        }
        else if(transform.position.y < LowerBound)
        {

            transform.position = new Vector3(transform.position.x, LowerBound, transform.position.z);
            _playerRb.velocity = Vector3.zero;
        }
        else
        {
            outOfBounds = false;
        }

        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver && outOfBounds == false)
        {
            // Apply a small upward force at the start of the game
            _playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (collision.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            _playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(collision.gameObject);
        }
        // if player collides with money, fireworks
        else if (collision.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            _playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(collision.gameObject);
        }
    }
}