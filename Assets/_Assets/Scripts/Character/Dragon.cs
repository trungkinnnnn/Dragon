using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : InputAction
{
    private string NAME_ANI_CROUCHATTACK = "isCrouchAttack";


    [Header("SKILL DRAGON")]
    public KeyCode inputCrouchAttack = KeyCode.J;


    protected override void Update()
    {
        base.Update();
        HandleCrouchAttack();
    }

    private void HandleCrouchAttack()
    {
        if(Input.GetKeyDown(inputCrouchAttack) && isCrouch)
        {
            animator.SetTrigger(NAME_ANI_CROUCHATTACK);
            Fire();
        }
    }    

}

