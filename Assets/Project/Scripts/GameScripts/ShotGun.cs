using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    new Transform camera;

    [Header("Tracer")]
    [SerializeField] GameObject tracer;
    [SerializeField] Transform muzzel;
    [SerializeField] float fadeDuration=0.3f;

    private float lastShootTime = 0;

    public void Awake()
    {
        camera=Camera.main.transform;
    }
    [Client]
    public void Shoot()
    {
        if (Time.time > lastShootTime + ((ShotGunInfo)itemInfo).fireRate)
        {
            itemGameObject.GetComponent<Animator>().SetTrigger("shoot");
            for (int i = 0; i < ((ShotGunInfo)itemInfo).shotGunPellets; i++)
            {
                Vector3 shootingDir = GetShootingDirection();
                Ray ray = new Ray(camera.position, shootingDir);


                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    RPC_Shoot(hit.point, hit.normal);
                    hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((ShotGunInfo)itemInfo).damage);
                    //CreateTracer(hit.point);
                }
                else
                {
                    //CreateTracer(camera.position + shootingDir * ((ShotGunInfo)itemInfo).range);
                }

            }
            lastShootTime = Time.time;
        }


    }
    [ServerRpc]
    private void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
        if (colliders.Length != 0)
        {
            GameObject bulletImpactObj = Instantiate(bulletImpactPrefab, hitPosition + hitNormal * 0.001f, Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpactPrefab.transform.rotation);
            bulletImpactObj.transform.SetParent(colliders[0].transform);
            base.Spawn(bulletImpactObj, null);
            Destroy(bulletImpactObj, 5f);
            //InstanceFinder.ServerManager.Despawn(bulletImpactObj);
        }
    }
    public override void Use()
    {
        Shoot();
    }
    Vector3 GetShootingDirection()
    {
        Vector3 targetPos = camera.position + camera.forward * ((ShotGunInfo)itemInfo).range;
        targetPos = new Vector3(
            targetPos.x + Random.Range(-((ShotGunInfo)itemInfo).inaccuracyDistance, ((ShotGunInfo)itemInfo).inaccuracyDistance),
            targetPos.y + Random.Range(-((ShotGunInfo)itemInfo).inaccuracyDistance, ((ShotGunInfo)itemInfo).inaccuracyDistance),
            targetPos.z + Random.Range(-((ShotGunInfo)itemInfo).inaccuracyDistance, ((ShotGunInfo)itemInfo).inaccuracyDistance)
            );

        Vector3 direction = targetPos - camera.position;
        return direction.normalized;
    }
    void CreateTracer(Vector3 end)
    {
        LineRenderer lr = Instantiate(tracer).GetComponent<LineRenderer>();
        lr.SetPositions(new Vector3[2] { muzzel.position, end });
        Destroy(lr,0.1f);
        //StartCoroutine(FadeTracer(lr));

    }
    IEnumerator FadeTracer(LineRenderer lr)
    {
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / fadeDuration;
            lr.startColor = new Color(lr.startColor.r, lr.startColor.g, lr.startColor.b, alpha);
            lr.endColor = new Color(lr.endColor.r, lr.endColor.g, lr.endColor.b, alpha);

            yield return null;
        }
        
    }
}
