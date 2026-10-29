using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Rigidbody2D rb;
    [SerializeField] float movementSpeed;
    [SerializeField] GameObject player;
    [SerializeField] Slider healthBar;
    [SerializeField] GameObject downPrefab;
    private Vector3 pos;



    [SerializeField] int AIType;
    private Vector2 moveDirection;
    private bool DB;
    private float cooldown = 0;
    public int damage = 1;
    public int health = 3;
    


    // Update is called once per frame

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        DB = true;
        if (healthBar == null)
        {
            healthBar = GetComponentInChildren<Slider>(true);
        }
        pos = healthBar.transform.position;

        healthBar.transform.position = new Vector3(pos.x, pos.y - 100000, pos.z);

    }
    void Update()
    {
        healthBar.GetComponent<UnityEngine.UI.Slider>().value = (float)health / 3;
        if (health <= 0)
        {
            if(Random.Range(1, 10) == 1)
            {
                Instantiate(downPrefab, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
        if (cooldown < Time.time)
        {
            DB = true;
        }
        if (DB)
        {
            moveDirection = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized;
            transform.position += new Vector3(moveDirection.x, moveDirection.y, 0) * movementSpeed * Time.deltaTime;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.tag == "Bullet")
        {
            health -= collision.GetComponent<Bullet>().damage;
            Destroy(collision.gameObject);
            healthBar.transform.position = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);


        }
        else if (collision.tag == "Player" && cooldown < Time.time)
        {
            if (cooldown <= Time.time)
            {
                collision.GetComponent<PlayerScript>().health -= damage;
            }
            cooldown = Time.time + 1f;
            DB = false;
        

        }
    }
}
