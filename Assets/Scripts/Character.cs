﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LookMode {Free, Locked};

public class Character : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float sensitivity = 10.0f;
    [Range(45.0f, 100.0f)]
    public float maxYAngle = 80.0f;
    [Range(0.0f, 10.0f)]
    public float speed = 1.0f;
    [Range(0.0f, 300.0f)]
    public float force = 10.0f;
    public bool inTutorial = true;
    public GameObject crosshair;

    private CharacterController controller;
    private Vector2 currentRotation = new Vector2(-89.14001f, -54.945f);
    private LayerMask dragMask;
    private LookMode lookMode = LookMode.Free;
    private bool ignoreMouseMove = false;
    private Rigidbody draggedBody;
    private Vector3   dragPoint;
    private float     dragDistance;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        dragMask = LayerMask.GetMask("Draggable", "Phone");
    }

    // Update is called once per frame
    void Update()
    {
        if (inTutorial || Mathf.Abs( Input.GetAxis("Shift") ) > 0.1f)
        {
            SetLookMode(LookMode.Free);
        }
        else
        {
            SetLookMode(LookMode.Locked);

            controller.SimpleMove( Input.GetAxis("Vertical") * transform.forward * speed );
            controller.SimpleMove( Input.GetAxis("Horizontal") * transform.right * speed );

            float mouseXmove = Input.GetAxis("Mouse X");
            float mouseYmove = Input.GetAxis("Mouse Y");
            if ( ignoreMouseMove && (Mathf.Abs(mouseXmove) > 0.1f || Mathf.Abs(mouseYmove) > 0.1f) )
            {
                ignoreMouseMove = false;
            }
            else
            {
                currentRotation.x += mouseXmove * sensitivity;
                currentRotation.y -= mouseYmove * sensitivity;
            }
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
            transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        }

        if ( Input.GetMouseButton(0) )
        {
            if (draggedBody == null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
               
                var hits = Physics.RaycastAll(ray, Mathf.Infinity, dragMask);
                foreach (var hit in hits)
                {
                    // raycast hit this gameobject
                    Debug.Log("Hit:" + hit.collider.name);

                    var puzzle      = hit.collider.gameObject.GetComponentInChildren<Puzzle>();
                    var phoneAudio  = hit.collider.gameObject.GetComponentInChildren<PhoneAudio>();

                    if (puzzle != null)
                    {
                        if (!hit.rigidbody)
                        {
                            continue;
                        }

                        draggedBody = hit.rigidbody;
                        dragPoint = draggedBody.transform.InverseTransformPoint(hit.point);
                        dragDistance = hit.distance;

                        Debug.Log("Hit puzzle:" + puzzle.name);
                        puzzle.StartSolving();
                    }

                    if (phoneAudio != null)
                    {
                        Debug.Log("Hit phone");
                        phoneAudio.PlayVoicemail();
                    }
                }
            }
            else
            {
                Vector3 desiredPosition = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(dragDistance);
                // Debug.Log(string.Format("drag {0} {1}", draggedBody.transform.position, desiredPosition));
                // draggedBody.MovePosition(desiredPosition);
                Vector3 worldDragPoint = draggedBody.transform.TransformPoint(dragPoint);
                draggedBody.AddForceAtPosition(
                    force * (desiredPosition - worldDragPoint), worldDragPoint, ForceMode.Force
                );
            }
        }
        else
        {
            if (draggedBody != null)
            {
                draggedBody = null;
            }
        }
    }

    public void SetLookMode(LookMode mode)
    {
        if (lookMode == mode)
            return;

        lookMode = mode;

        switch (lookMode)
        {
            case LookMode.Free:
                Cursor.lockState = CursorLockMode.None;
                crosshair.SetActive(false);
                break;
            case LookMode.Locked:
                Cursor.lockState = CursorLockMode.Locked;
                ignoreMouseMove = true;
                crosshair.SetActive(true);
                // currentRotation = new Vector2(transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.x);
                break;
        }
    }

    public void FinishTutorial()
    {
        inTutorial = false;
        draggedBody = null;
    }
}
