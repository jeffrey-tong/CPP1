using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shoot))]
public class EnemyTurret : Enemy
{
    public float projectileFireRate;
    public int turretRange;
    float timeSinceLastFire;
    Shoot shootScript;
    public Transform playerObject;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        shootScript = GetComponent<Shoot>();
        shootScript.OnProjectileSpawned.AddListener(UpdateTimeSinceLastFire);

        if(turretRange <= 0)
        {
            turretRange = 4;
            Debug.Log("Turret rnage not set, setting it to 4");
        }
    }

    private void OnDisable()
    {
        shootScript.OnProjectileSpawned.RemoveListener(UpdateTimeSinceLastFire);
    }

    void UpdateTimeSinceLastFire()
    {
        timeSinceLastFire = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curClips = anim.GetCurrentAnimatorClipInfo(0);
        if (curClips[0].clip.name != "Shoot")
        {
            if(Time.time >= timeSinceLastFire + projectileFireRate)
            {
                if (Mathf.Abs(playerObject.transform.position.x - this.transform.position.x) < turretRange)
                {
                    anim.SetTrigger("shoot");
                }
                
            }
        }
        this.sr.flipX = playerObject.transform.position.x < this.transform.position.x;
    }

    public override void Death()
    {
        Destroy(gameObject);
    }
}
