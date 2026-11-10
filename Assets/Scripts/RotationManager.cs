using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;
public static class RotationManager
{
    private static Vector2 pos;
    public static void RotateObject(Component gameObject,float offset = 0)
    {


        float angle = Mathf.Atan2(TrackMouse(gameObject).y, TrackMouse(gameObject).x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);

    }
    public static Vector2 TrackMouse(Component gameObject)
    {
        if (Mouse.current == null || Camera.main == null)
        {
            return Vector2.zero;
        }

        pos = Mouse.current.position.ReadValue();
        Ray mouseRay = Camera.main.ScreenPointToRay(pos);

        Plane gunPlane = new Plane(Vector3.forward,new Vector3(0f, 0f, gameObject.transform.position.z));

        if (!gunPlane.Raycast(mouseRay, out float distance))
        {
            return Vector2.zero;
        }
        
        Vector3 mouseWorldPosition = mouseRay.GetPoint(distance);

        return mouseWorldPosition - gameObject.transform.position;
    }

    public static Vector2 TrackObject(Component gameObject, Component targetObject)
    {
        if (Mouse.current == null || Camera.main == null)
        {
            return Vector2.zero;
        }

        pos = targetObject.transform.position;
        Ray mouseRay = Camera.main.ScreenPointToRay(pos);

        Plane gunPlane = new Plane(Vector3.forward, new Vector3(0f, 0f, gameObject.transform.position.z));

        if (!gunPlane.Raycast(mouseRay, out float distance))
        {
            return Vector2.zero;
        }

        Vector3 mouseWorldPosition = mouseRay.GetPoint(distance);

        return mouseWorldPosition - gameObject.transform.position;
    }
}
