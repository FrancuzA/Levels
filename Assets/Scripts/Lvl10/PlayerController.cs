using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    

    private void Start()
    {
        
        _animator = GetComponent<Animator>();   
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Teacher.Instance.SetPlayerIsCheating(true);
            _animator.SetTrigger("Cheating");
            _animator.SetBool("basic", false);// Gracz œci¹ga
        }
        else
        {
            Teacher.Instance.SetPlayerIsCheating(false);
            _animator.SetBool("basic", true);// Gracz przesta³ œci¹gaæ
        }
    }
}