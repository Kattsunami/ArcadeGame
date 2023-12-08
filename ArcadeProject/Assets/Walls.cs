using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private string animationName;

    public void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.transform.tag != "Ball") return;

        cameraAnimator.Play(animationName);
    }
}