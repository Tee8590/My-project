
using System;
using UnityEngine;

//[RequireComponent(typeof(AnimationController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, MinMaxRange(1, 11)] Vector2 jumpForce;
    [SerializeField, Range(0, 10)] float moveSpeed;
    [SerializeField] Transform playerModel;

    public event Action<Vector3> OnJump = delegate { };
    public event Action<Vector3> OnLand = delegate { };
   
    public void Jump() => OnJump.Invoke(Vector3.up *  jumpForce);
    public void Land() => OnLand.Invoke(Vector3.zero);
   
}
