using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public class NPCController : MonoBehaviour
{
    private AnimationController _animController;

    private void Start()
    {
        _animController = GetComponent<AnimationController>();
    }
}
