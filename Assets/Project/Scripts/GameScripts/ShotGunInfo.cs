using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FPS/New ShotGun")]
public class ShotGunInfo : ItemInfo
{
    public float damage;
    public int shotGunPellets;
    public float range;
    public float inaccuracyDistance;
    public float fireRate;
}
