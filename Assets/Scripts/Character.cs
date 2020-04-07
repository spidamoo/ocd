using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public enum LookMode {Free, Locked};
public enum CursorMode {Normal, Highlight, Dragging};

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
    public Text exitText;
    public PostProcessVolume vignetteVolume;
    public float vignetteSpan = 20.0f;
    public Animator uiAnimator;

    private CharacterController controller;
    private Vector2 currentRotation = new Vector2(289.0f, -18.1f);
    private LayerMask dragMask;
    private LookMode lookMode = LookMode.Free;
    private CursorMode cursorMode = CursorMode.Normal;
    private bool ignoreMouseMove = false;
    private Rigidbody draggedBody;
    private Vector3   dragPoint;
    private float     dragDistance;
    private float exitTimer;
    private float anxietyTimer;
    private Vignette vignetteSettings;

    float voicemail;
    int toEnding;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        dragMask = LayerMask.GetMask("Draggable", "Phone");

        Cursor.visible = false;

        vignetteVolume.profile.TryGetSettings(out vignetteSettings);
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

            controller.SimpleMove(
                Input.GetAxis("Vertical") * transform.forward * speed +
                Input.GetAxis("Horizontal") * transform.right * speed
            );

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

        if (draggedBody == null)
        {
            cursorMode = CursorMode.Normal;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
            var hits = Physics.RaycastAll(ray, 1.3f, dragMask);
            foreach (var hit in hits)
            {
                // raycast hit this gameobject
                Debug.Log("Hit:" + hit.collider.name);

                var puzzle      = hit.rigidbody ? hit.rigidbody.gameObject.GetComponentInChildren<Puzzle>() : hit.collider.gameObject.GetComponentInChildren<Puzzle>();
                var phoneAudio  = hit.collider.gameObject.GetComponentInChildren<PhoneAudio>();

                if ( (puzzle && !puzzle.solved) || phoneAudio )
                {
                    cursorMode = CursorMode.Highlight;
                }

                if ( !Input.GetMouseButtonDown(0) )
                    continue;

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

                FMODUnity.RuntimeManager.StudioSystem.getParameterByName("Voicemail", out voicemail);
                if (phoneAudio != null && voicemail < 0.5f)
                {
                    Debug.Log("Hit phone");


                    phoneAudio.PlayVoicemail();

                    if (toEnding == 1)
                    {
                        Debug.Log("Trigger eindanimatie");
                        uiAnimator.SetTrigger("credits");
                    }
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

            cursorMode = CursorMode.Dragging;

            if ( !Input.GetMouseButton(0) )
                draggedBody = null;
        }

        switch (cursorMode)
        {
            case CursorMode.Normal:
                crosshair.GetComponent<Image>().color = Color.white;
                break;
            case CursorMode.Highlight:
                crosshair.GetComponent<Image>().color = Color.green;
                break;
            case CursorMode.Dragging:
                crosshair.GetComponent<Image>().color = Color.blue;
                break;
        }

        crosshair.GetComponent<RectTransform>().anchoredPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if (exitTimer > 0.0f)
        {
            exitTimer -= Time.deltaTime;
            exitText.color = Color.white;
            if ( Input.GetKey ("escape") )
            {
                Application.Quit();
            }
        }
        else
        {
            if ( Input.GetKey ("escape") )
            {
                exitTimer = 1.0f;
            }
            exitText.color = Color.clear;
        }

        float anxiety;
        float puzzleCounter;
        //float voicemail;
        FMODUnity.RuntimeManager.StudioSystem.getParameterByName("Anxiety", out anxiety);
        FMODUnity.RuntimeManager.StudioSystem.getParameterByName("PuzzleCounter", out puzzleCounter);
        FMODUnity.RuntimeManager.StudioSystem.getParameterByName("Voicemail", out voicemail);
        // Debug.Log(string.Format("state: {0} {1}", puzzleCounter, anxiety));

        if (anxiety > 0.5f)
        {
            anxietyTimer += Time.deltaTime;
        }
        else
        {
            anxietyTimer -= Time.deltaTime * 10.0f;
            if (anxietyTimer < 0.0f)
            {
                anxietyTimer = 0.0f;
            }
        }

        vignetteSettings.intensity.value = anxietyTimer / vignetteSpan;

        if (puzzleCounter > 2.5f && anxiety > 0.5f && voicemail > 0.5f)
        {
            toEnding = 1;
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
                crosshair.SetActive(true);
                break;
            case LookMode.Locked:
                Debug.Log("euler " + transform.rotation.eulerAngles);
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
