using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    
    private float downtime = .25f;
    private float next = 0;
    [SerializeField] private GameObject bulletPrefab;
    private GameObject instant;

    // Start is called before the first frame update
    private void Start()
    {
        
        //bulletPrefab = GameObject.FindWithTag("Bullet");
    }
    // Update is called once per frame
    void Update()
    {
        RotationManager.RotateObject(this);
        if (Input.GetMouseButton(0))
        {
            Cooldown();
        }
    } 

    private void Shoot()
    {
        Debug.Log("Shoot!");
        instant = Instantiate(bulletPrefab);

        Bullet bulletScript = instant.GetComponent<Bullet>();
        bulletScript.direction = (Vector3)RotationManager.TrackMouse(this);
        bulletScript.speed = 8;

    }

    private void Cooldown()
    {
        if (Time.time >= next)
        {
            next = Time.time + downtime;
            Shoot();
        }
    }
   

   
}

