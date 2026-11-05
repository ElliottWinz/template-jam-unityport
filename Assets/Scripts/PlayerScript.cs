using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] float MaxSpeed = 400f;
    [SerializeField] float movespeedCorrection = 5f;
    public int totalScore = 0;
    [SerializeField] float XInput, YInput;
    [SerializeField] Rigidbody2D playerRidgidBody;
    private 
    bool coinDebounce = true;
    bool dbStepAudioPlay = true;

    bool alive = true;
    private Vector2 moveDirection; // direction which the player is moving, X and Y value.
    private Animator anim; // the player object's animator compnent
    [SerializeField] AudioSource source; // where the game's audio is being played from

    [SerializeField] List<AudioClip> clipList;


    public int maxHealth = 3;
    public int currentHealth = 0;   
    public float currentDecay = 0;
    public float decayRate = 0;


    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject decayBar;


    private void OnValidate()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        Debug.Log("Started!");
        coinDebounce = true;    
        anim = gameObject.GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.GetComponent<UnityEngine.UI.Slider>().maxValue = (float)maxHealth;
        decayBar.GetComponent<UnityEngine.UI.Slider>().maxValue = (float)maxHealth;

        if (SceneManager.GetActiveScene().buildIndex == 0) // if the first level, set to 0.
        {
            PlayerPrefs.SetInt("SavedScore", 0);
        }
    }



    private Vector2 getInputMovementVector()
    {
        Vector2 moveVector = Vector2.zero;
        moveVector = new Vector2(XInput, YInput);
       
        return moveVector.normalized;
    }
    
    private void FixedUpdate() 
    {
        if (alive)
        {
            moveCharacter();
            healthBar.GetComponent<UnityEngine.UI.Slider>().value = (float)currentHealth;
            if (!WaveSpawner.isPaused)
            {
            currentDecay += Time.fixedDeltaTime * decayRate * maxHealth;
            }
            decayBar.GetComponent<UnityEngine.UI.Slider>().value = currentDecay;

            if (currentHealth - currentDecay <= 0)
            {
                alive = false;
            }
        }
        
        
    }

    void getOtherInputs()
    {

        if (Input.GetKeyDown(KeyCode.Space) && coinDebounce)
        {
            coinDebounce = false;

            StartCoroutine(CoinCollectCoroutine());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            alive = false;
            Application.Quit();
        }
    }
    IEnumerator playStepSound()
    {
        source.PlayOneShot(source.clip);

        yield return new WaitForSeconds(.2f);
        source.clip = clipList[Random.Range(0,clipList.Count)];
        dbStepAudioPlay = true;
    }
    IEnumerator CoinCollectCoroutine() // this can extend to any interactable.
    {


        Collider2D[] hitCoins = Physics2D.OverlapCircleAll(gameObject.transform.position, .5f);
        foreach (var hitCollider in hitCoins)
        {

            if (hitCollider.isTrigger && hitCollider.gameObject.tag == "Down"){


                hitCollider.gameObject.GetComponent<InteractableScript>().CollectItem(this);
            }
        }
        yield return new WaitForSeconds(.1f);
        coinDebounce = true;


    }
    void moveCharacter()
    {
        moveDirection = getInputMovementVector();
        if (moveDirection != Vector2.zero)
        {
            Vector2 goalMovment = moveDirection * Time.fixedDeltaTime * MaxSpeed;
            playerRidgidBody.velocity = Vector2.Lerp(playerRidgidBody.velocity, goalMovment, Time.fixedDeltaTime * movespeedCorrection);

            if(dbStepAudioPlay == true)
            {
                dbStepAudioPlay = false;
                StartCoroutine(playStepSound()); // I'm almost certain there's a way to do this without all the bools.
            }
        }
        else
        {
            playerRidgidBody.velocity *= .95f;
            if(playerRidgidBody.velocity.magnitude < .05f)
            {
                playerRidgidBody.velocity = Vector2.zero;
            }
        }


    }

    public void ReverseDecay(float amountToAward)
    {
        if(currentDecay - (amountToAward * maxHealth) >= 0)
        {
            currentDecay -= amountToAward * maxHealth;

        }
        else
        {
            currentDecay = 0;
        }
    }
    void Update()
    {
        XInput = Input.GetAxisRaw("Horizontal");
        YInput = Input.GetAxisRaw("Vertical");
        if (anim)
        {
            anim.SetInteger("XDir", (int)XInput);
            anim.SetInteger("YDir", (int)YInput);

        }
        getOtherInputs();

    }

    
}
