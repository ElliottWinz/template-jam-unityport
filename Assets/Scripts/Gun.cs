using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Vector2 pos;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        rotateGun();
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    } 

    private void Shoot()
    {
        Debug.Log("Shoot!");
    }

    private Vector2 trackMouse()
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

    private void rotateGun()
    { 

      
        float angle = Mathf.Atan2(trackMouse().y, trackMouse().x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }
}

