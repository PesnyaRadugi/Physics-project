using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalkaScript : MonoBehaviour
{
    // просто мини-скрипт чтобы навесить ползунок для регулировки параметров
    public void SliderChanged(float newMass)
    {
        GetComponent<Rigidbody2D>().mass = newMass;
        GameObject.FindGameObjectWithTag("Projectile").GetComponent<ProjectileScript>().M = newMass;

    }
}
