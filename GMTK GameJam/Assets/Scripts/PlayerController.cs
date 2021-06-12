using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
public class PlayerController : MonoBehaviour
{

    private Vector2 input;
    private Rigidbody2D rb;
    private bool firingCooldown;
    DistanceJoint2D joint;
    [SerializeField]
    VisualEffectAsset flashEffect;
    
    public void OnMovement(InputAction.CallbackContext value)
    {
        input = value.ReadValue<Vector2>();
    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float rot = rb.rotation;
        float changeRot = input.x * Time.fixedDeltaTime * 180 * -1;

        rot += changeRot;
        rb.SetRotation(rot);

        rot = rot * Mathf.Deg2Rad;
        
        Vector2 forwardDir = new Vector2(Mathf.Cos(rot), Mathf.Sin(rot));
        rb.AddForce(forwardDir * Time.fixedDeltaTime * input.y * 200 * rb.mass);

        if(input.y > 0.5)
        {

        }else
        {

        }
    }

    public void shoot(InputAction.CallbackContext value)
    {
        if(value.ReadValue<float>() > 0 && firingCooldown)
        {
            this.transform.Find("VFX").gameObject.GetComponent<VisualEffect>().Play();
            GameObject frontCannon = this.transform.Find("FrontCannon").gameObject;
            Vector3 fcPos = frontCannon.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(fcPos, fcPos - this.transform.position);
            //Debug.Log(hit.collider);
            //Debug.Log(hit.collider.gameObject.tag);
            if(hit.collider != null && hit.collider.gameObject.tag != "Wall")
            {
                DistanceJoint2D dj2d = this.gameObject.AddComponent<DistanceJoint2D>();
                dj2d.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                dj2d.enableCollision = true;
                dj2d.maxDistanceOnly = true;
                joint = dj2d;
                firingCooldown = false;
            }
        }
        else
        {
            if(value.ReadValue<float>() == 0)
            {
                firingCooldown = true; 
                DistanceJoint2D dj2d = this.GetComponent<DistanceJoint2D>();
                if (dj2d != null)
                {
                    Destroy(dj2d);
                }
            }
        }
        
        
       }

    void Update()
    {
    }

}
