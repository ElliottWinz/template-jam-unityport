using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Vector2 pos;
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
        RotateGun();
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
        bulletScript.direction = (Vector3)TrackMouse();
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
    private Vector2 TrackMouse()
    {
        if (Mouse.current == null || Camera.main == null)
        {
            return Vector2.zero;
        }

        pos = Mouse.current.position.ReadValue();
        Ray mouseRay = Camera.main.ScreenPointToRay(pos);

        Plane gunPlane = new Plane(
            Vector3.forward,
            new Vector3(0f, 0f, transform.position.z));

        if (!gunPlane.Raycast(mouseRay, out float distance))
        {
            return Vector2.zero;
        }


        Vector3 mouseWorldPosition = mouseRay.GetPoint(distance);
        
        return mouseWorldPosition - transform.position;
    }

    private void RotateGun()
    { 

      
        float angle = Mathf.Atan2(TrackMouse().y, TrackMouse().x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }
}

