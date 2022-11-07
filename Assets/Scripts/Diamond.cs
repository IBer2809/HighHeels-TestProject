using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] private ParticleSystem _burstDiamond;


    public void ExplodeDiamond()
    {
        _burstDiamond.Play();
        Destroy(gameObject);
    }
}
