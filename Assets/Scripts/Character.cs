using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float sensitivity = 10.0f;
    [Range(45.0f, 100.0f)]
    public float maxYAngle = 80.0f;
    [Range(0.0f, 10.0f)]
    public float speed = 1.0f;
    private CharacterController controller;
    private Vector2 currentRotation;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        controller.SimpleMove( Input.GetAxis("Vertical") * transform.forward * speed );
        controller.SimpleMove( Input.GetAxis("Horizontal") * transform.right * speed );

        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
        transform.rotation = Quaternion.Euler(currentRotation.y,currentRotation.x,0);

        if ( Input.GetMouseButtonDown(0) )
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
            var hits = Physics.RaycastAll(ray);
            foreach (var hit in hits)
            {
                // raycast hit this gameobject
                Debug.Log("Hit:" + hit.collider.name);
            }

            // Debug.Log(string.Format("raycast {0} {1}", Input.mousePosition, ray, hit));
        }
    }
}
