using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using FishNet;

public class SingleShotGun : Gun
{
    private float lastShootTime = 0;

    public void Shoot()
    {
        

        if(Time.time > lastShootTime + ((SingleShotGunInfo)itemInfo).fireRate)
        {
            itemGameObject.GetComponent<Animator>().SetTrigger("shoot");
            
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.GetComponent<IDamageable>() != null)
                {
                    RPC_ServerFire(hit.collider.gameObject);
                }
                //hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((SingleShotGunInfo)itemInfo).damage,base.OwnerId);
                RPC_Shoot(hit.point, hit.normal);
            }
            lastShootTime = Time.time;
        }
    }
    [ServerRpc]
    private void RPC_ServerFire(GameObject hitPlayer)
    {
        hitPlayer.GetComponent<IDamageable>().TakeDamage(((SingleShotGunInfo)itemInfo).damage, base.OwnerId);
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
}
