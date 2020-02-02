using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour
{
    public List<Collider> colliders;
    public bool solved;
    public UnityEvent startEvent;
    public UnityEvent solvedEvent;
    public GameObject theObject;
    public GameObject targetObject;

    private int collidedTriggers = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if ( colliders.Contains(other) )
        {
            collidedTriggers++;
        }
        Debug.Log(string.Format("enter {0} {1}", other, collidedTriggers));
        CheckTriggers();
    }
    private void OnTriggerExit(Collider other)
    {
        if ( colliders.Contains(other) )
        {
            collidedTriggers--;
        }
        Debug.Log(string.Format("exit {0} {1}", other, collidedTriggers));
        CheckTriggers();
    }

    private void CheckTriggers()
    {
        if (collidedTriggers == colliders.Count)
        {
            solved = true;
            Debug.Log("solved");
            solvedEvent.Invoke();

            Destroy( theObject.GetComponent<Rigidbody>() );
            theObject.transform.SetPositionAndRotation(targetObject.transform.position, targetObject.transform.rotation);
        }
        else
        {
            if (solved)
                Debug.Log("unsolved");
            solved = false;
        }
    }

    public void StartSolving()
    {
        startEvent.Invoke();
    }
}
