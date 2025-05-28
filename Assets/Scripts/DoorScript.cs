using System.Collections;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] Transform DoorCenter;

    [SerializeField] Animator doorAnim;

    public bool doorOpen = false;
    bool doorCanMove;
    bool animDone;

    public float rotationSpeed = 2f;

    [SerializeField] Transform newRotatePos;
    [SerializeField] Transform oldRotatePos;

    Animator animator;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void ChangeDoorState()
    {
        doorOpen = !doorOpen;
        if (doorOpen == false)
        {
           
            animator.SetBool("doorOpen", false);

            StartCoroutine(WaitForAnim());
           
        }
        if (doorOpen == true)
        {

            animDone = false;

            animator.SetBool("doorOpen", true);

            StartCoroutine(WaitForAnim());

            if (animDone == true)
            {
            }
          
        }
    }

    IEnumerator WaitForAnim()
    {

        yield return new WaitForSeconds(4f);
       
        animator.SetTrigger("done");
        Debug.Log("AnimDone");
    }

}
