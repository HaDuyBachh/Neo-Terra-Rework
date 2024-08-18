using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    public float speed;
    Animator animator;
    private float gripTarget;
    private float indexTarget;
    private float thumbTarget;
    private float gripCurrent;
    private float indexCurrent;
    private float thumbCurrent;
    // public bool InvisibleWhenGrabbing;
    private string animatorGripParam = "grip";
    private string animatorIndexParam = "index";
    private string animatorThumbParam = "thumb";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }
    internal void SetGrip(float v)
    {
        gripTarget = v;
        // if (Math.Round(v) == 1 && InvisibleWhenGrabbing)
        // {
        //     GetComponent<SkinnedMeshRenderer>().enabled = false;
        // }
        // else
        // {
        //     GetComponent<SkinnedMeshRenderer>().enabled = true;
        // }
    }

    internal void SetIndex(float v)
    {
        indexTarget = v;
    }

    internal void SetThumb(float v)
    {
        thumbTarget = v;
    }
    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }
        if (indexCurrent != indexTarget)
        {
            indexCurrent = Mathf.MoveTowards(indexCurrent, indexTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorIndexParam, indexCurrent);
        }
        if (thumbCurrent != thumbTarget)
        {
            thumbCurrent = Mathf.MoveTowards(thumbCurrent, thumbTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorThumbParam, thumbCurrent);
        }
    }


}
