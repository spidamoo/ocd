using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    private Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip);
        if (m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "End titles")
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
