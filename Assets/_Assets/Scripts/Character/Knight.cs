using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : InputAction
{
    private string NAME_ANI_BLOCK = "isBlock";


    [Header("SKILL KNIGHT")]
    public KeyCode inputBlock = KeyCode.B;


    protected override void Update()
    {
        base.Update();
        HandleBlock();
    }

    private void HandleBlock()
    {
        if (Input.GetKeyDown(inputBlock))
        {
            animator.SetTrigger(NAME_ANI_BLOCK);
        }
    }
}
