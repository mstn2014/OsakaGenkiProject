using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Game1PlayerController : MonoBehaviour
{

    Animator animator;
    InputMgr m_input;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        GlobalSetting go = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_input = go.InputMgr;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DoPose()
    {
        animator.SetTrigger("IsPose");
    }

    public void DoPass()
    {
        animator.SetTrigger("IsPass");
    }

    public void DoRun()
    {
        this.transform.rotation = Quaternion.LookRotation(Vector3.left);
        animator.SetBool("IsRun", true);
    }

    public void DoStand()
    {
        this.transform.rotation = Quaternion.LookRotation(Vector3.back);
        animator.SetBool("IsRun", false);
    }
}