using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Vector2 input;
    private Rigidbody2D rb;

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
        //Debug.Log(forwardDir);
        rb.AddForce(forwardDir * Time.fixedDeltaTime * input.y * 200 * rb.mass);
    }

    void Update()
    {
        
    }

}
