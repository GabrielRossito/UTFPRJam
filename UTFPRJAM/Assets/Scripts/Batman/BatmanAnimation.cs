using UnityEngine;
using System.Collections;

public class BatmanAnimation : MonoBehaviour
{

    private Animator _animator { get { return GetComponent<Animator>(); } }

    private bool _punching;
    private bool _run;

    public void Punch()
    {
        _punching = true;
        _animator.SetTrigger("Punch");
    }

    public void PunchEnd()
    {
        _punching = false;
    }

    public void Idle()
    {
        if(_run)
        {
            _animator.SetBool("Run", false);
            _run = false;
        }
    }

    public void Run()
    {
        if(!_run)
        { 
            _run = true;
            _animator.SetBool("Run", true);
        }
    }

}
