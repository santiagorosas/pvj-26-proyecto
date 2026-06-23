using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRayShoot : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        Vector2 origin = transform.position;

        Debug.Log("mouse pos: " + Mouse.current.position.ReadValue());

        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // El vector dirección entre punto A y punto B es B-A
        Vector2 direction = (mouseWorldPosition - origin).normalized;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 20);       
        Debug.DrawRay(origin, direction * 20, Color.yellow);

        
        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("pegó con algo: " + hitObject.name);

            Debug.DrawLine(origin, hit.point, Color.red);
        }
        
        
    
    }
}
