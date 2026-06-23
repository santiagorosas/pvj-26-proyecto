using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRayShoot : MonoBehaviour
{
    [SerializeField] private LayerMask _raycastLayerMask;


    // Update is called once per frame
    void Update()
    {
        Enemy targetedEnemy;

        Vector2 origin = transform.position;

        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // El vector dirección entre punto A y punto B es B-A
        Vector2 direction = (mouseWorldPosition - origin).normalized;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 20, _raycastLayerMask);       
        
        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.DrawLine(origin, hit.point, Color.red);

            if (hitObject.GetComponent<Enemy>() != null)
            {
                targetedEnemy = hitObject.GetComponent<Enemy>(); 
            }
            else
            {
                targetedEnemy = null;
            }
        }
        else
        {
            Debug.DrawRay(origin, direction * 20, Color.yellow);
            targetedEnemy = null;
        }

        if (Mouse.current.leftButton.isPressed && targetedEnemy != null)
        {
            targetedEnemy.GetComponent<EnemyDeath>().Die();
        }
    }
}
