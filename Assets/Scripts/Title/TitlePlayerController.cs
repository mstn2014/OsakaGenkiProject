using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class TitlePlayerController : MonoBehaviour
{

    Animator animator;
    int IsStartId,IsStopId;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        IsStartId = Animator.StringToHash("IsStart");
        IsStopId = Animator.StringToHash("IsStop");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveToFront()
    {
        animator.SetBool(IsStartId, true);
        iTweenEvent.GetEvent(this.gameObject, "MoveToFront").Play();
    }

    void OnCompleteMove()
    {
        animator.SetBool(IsStopId, true);
    }
}