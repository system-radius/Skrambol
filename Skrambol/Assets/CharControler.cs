using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControler : MonoBehaviour
{   
    private float moveSpeed = 8.0f;

    Vector3 forward, right;

    // Start is called before the first frame update

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;    
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0) {
            Move();
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * h;
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * v;
        Vector3 facing = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = facing;
        transform.position += rightMovement;
        transform.position += upMovement;

        Camera.main.transform.position = new Vector3(
            transform.position.x, 
            Camera.main.transform.position.y,
            transform.position.z    
        );

    }

}
