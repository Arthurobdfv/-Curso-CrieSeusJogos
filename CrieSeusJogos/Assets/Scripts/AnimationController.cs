using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] 
    private string AnimationParameterName = "Transition";
    
    private AnimationEnum _currentAnimation;
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.GetInteger(AnimationParameterName);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _animator.SetInteger(AnimationParameterName, (int)_currentAnimation);
    }

    public void SetAnimation(AnimationEnum animationState){
        _currentAnimation = animationState;
    }

    public void Trigger(string anim)
    {
        _animator.SetTrigger(anim);
    }


}
