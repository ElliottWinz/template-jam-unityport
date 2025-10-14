using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]Rigidbody2D rb;
    [SerializeField] float movementSpeed;
    [SerializeField] float thrust = 10f;

    [SerializeField] int AIType;
    private Vector2 moveDirection;
    private bool DB;


    // Update is called once per frame

    private void Start()
    {
        DB = true;
    }
    void FixedUpdate()
    {
        if (DB)
        {
            DB = false;
            StartCoroutine(skeletonAI());
        }

        if (Mathf.Abs(rb.velocity.magnitude) < movementSpeed)
        {
            rb.AddForce(moveDirection * thrust);

        }
    }
    IEnumerator skeletonAI()
    {


        moveDirection = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
        Debug.Log(moveDirection);
        yield return new WaitForSeconds(Random.Range(.1f, .5f));

        DB = true;
    }
}
