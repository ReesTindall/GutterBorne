using UnityEngine;
using UnityEngine.Events;

public class RatHealth : MonoBehaviour
{
    public int maxHP = 3;
    public UnityEvent onDeath;     

    int hp;
    void Awake() => hp = maxHP;

    public void TakeDamage(int dmg)
    {
        if (hp <= 0) return;
        hp -= dmg;

        if (hp <= 0)
        {
            onDeath?.Invoke();
            Destroy(gameObject);   
        }
    }
}