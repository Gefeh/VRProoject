using UnityEngine;

public class Crocodile : MonoBehaviour
{
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void PlayAnimation(string animationState)
    {
        _animator.Play(animationState);
    }
}
