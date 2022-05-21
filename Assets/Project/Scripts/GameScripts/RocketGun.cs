using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGun : Gun
{
    public void Shoot()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            //hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((SingleShotGunInfo)itemInfo).damage);
            RPC_Shoot(hit.point, hit.normal);
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

}
