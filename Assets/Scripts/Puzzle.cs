using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public List<Collider> colliders;
    public bool solved;
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
        }
        else
        {
            if (solved)
                Debug.Log("unsolved");
            solved = false;
        }
    }
}
