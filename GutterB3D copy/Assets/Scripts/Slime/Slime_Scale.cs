using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Scale : MonoBehaviour {

// This script uses tweening to make the character undulate as it moves, jumps, etc
// Right-click the character to make an Empty GO child called "ScaleTween".
// Make the Mesh a child of ScaleTween so the mesh inherits scaling from ScaleTween.

// There is a new AnmationCurve for each type of motion: move, jump, etc.
// Each starts at 1, ends at 1, and has at least one middle keyframe.
// In this case, Move center goes down and jump goes up.

    public Transform scaleTweener;
    Vector3 startScale;
    public AnimationCurve curveMove = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public AnimationCurve curveJump = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    float elapsed = 0f;
    float elapsedJump = 0f;

    // bools activated by a separate character movement script:
    public bool jumpTweenOn = false;
    public bool moveTweenOn = false;

    private void Start(){
        startScale = scaleTweener.localScale;
    }

    private void FixedUpdate () {
    //TWEENING jump (curve goes up). Note it will always finish the current elapse:
        if ((jumpTweenOn)||(elapsedJump > 0)){
            Vector3 newScale = new Vector3(startScale.x - (curveJump.Evaluate(elapsedJump) -1),
                startScale.y + (curveJump.Evaluate(elapsedJump) -1),
                startScale.z - (curveJump.Evaluate(elapsedJump) -1)
                );
            scaleTweener.localScale = newScale;
            elapsedJump += Time.deltaTime;
            //reset / end:
            if (elapsedJump >= 1f){
                elapsedJump = 0;
                jumpTweenOn = false;
            }
        }

        //TWEENING walk (curve ges down). Note it will always finish the current elapse:
        if ((moveTweenOn)||(elapsed > 0)){
            Vector3 newScale = new Vector3(startScale.x + (1-curveMove.Evaluate(elapsed)),
                startScale.y * curveMove.Evaluate(elapsed),
                startScale.z + (1-curveMove.Evaluate(elapsed))
                );
            scaleTweener.localScale = newScale;
            elapsed += Time.deltaTime;
            //reset / loop:
            if (elapsed >= 1f){
                elapsed = 0;
            }
        }
    }

}