using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected Animator anim;

    protected int _health;
    public int maxHealth;

    public int health
    {
        get => _health;
        set
        {
            _health = value;
            if(_health > maxHealth)
            {
                _health = maxHealth;
            }
            if(_health <= 0)
            {
                Death();
            }
        }
    }

    public virtual void Death()
    {
        anim.SetTrigger("Death");
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        if(maxHealth <= 0)
        {
            maxHealth = 5;
        }
        health = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
    }
}
