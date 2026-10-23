using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    private GameObject gunObject;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
        gunObject = GameObject.FindWithTag("Gun");
        transform.position = gunObject.transform.position;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= spawnTime + 2)
        {
            Destroy(gameObject);
        }
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
}
