using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private static readonly int MoveBool = Animator.StringToHash("Moving");

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        controller = transform.parent.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        
        Vector3 move = new Vector3(x, 0, z);
        animator.SetBool(MoveBool, move.magnitude > 0.1);
        controller.Move(move * (Time.deltaTime * 4));

        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(move);
        }
    }
}
