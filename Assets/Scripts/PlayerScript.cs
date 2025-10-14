using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] float MaxSpeed = 400f;
    [SerializeField] float movementForce = 10f;
    [SerializeField] float movespeedCorrection = 5f;
    public int totalScore = 0;
    [SerializeField] float XInput, YInput;
    [SerializeField] Rigidbody2D rb; // player's rigidbody.
    private 
    bool coinDebounce = true;
    bool dbStepAudioPlay = true; 
    private Vector2 moveDirection; // direction which the player is moving, X and Y value.
    private Animator anim; // the player object's animator compnent
    [SerializeField] AudioSource source; // where the game's audio is being played from

    [SerializeField] List<AudioClip> clipList;

    [SerializeField] TextMeshProUGUI ScoreText;
    void Start()
    {
        Debug.Log("Started!");
        coinDebounce = true;
        anim = gameObject.GetComponent<Animator>();

        if (PlayerPrefs.HasKey("SavedScore"))
        {
            ScoreText.text = "Score: " + PlayerPrefs.GetInt("SavedScore").ToString();

        }

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
        moveCharacter();
    }

    void getOtherInputs()
    {

        if (Input.GetKeyDown(KeyCode.F) && coinDebounce)
        {
            coinDebounce = false;

            StartCoroutine(CoinCollectCoroutine());
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


        Collider2D[] hitCoins = Physics2D.OverlapCircleAll(gameObject.transform.position, 1.5f);
        foreach (var hitCollider in hitCoins)
        {

            if (hitCollider.isTrigger && hitCollider.gameObject.tag == "Coin"){


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
            Vector2 goalMovment = moveDirection * Time.fixedDeltaTime * movementForce;
            rb.velocity = Vector2.Lerp(rb.velocity, goalMovment, Time.fixedDeltaTime * movespeedCorrection);

            if(dbStepAudioPlay == true)
            {
                dbStepAudioPlay = false;
                StartCoroutine(playStepSound()); // I'm almost certain there's a way to do this without all the bools.
            }
        }
        else
        {
            rb.velocity *= .95f;
            if(rb.velocity.magnitude < .05f)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    public void awardPoints(int amountToAward)
    {
        totalScore += amountToAward;

        if (PlayerPrefs.HasKey("SavedScore"))
        {
            PlayerPrefs.SetInt("SavedScore", totalScore);
            ScoreText.text = "Score: " + PlayerPrefs.GetInt("SavedScore").ToString();

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
