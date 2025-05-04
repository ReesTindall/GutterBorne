using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAttackRelay : MonoBehaviour
{
    public RatController rat;      

    public void HitStart() => rat.HitStart();
    public void HitEnd()   => rat.HitEnd();
}