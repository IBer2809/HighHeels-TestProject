using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int _removedHeelsCount;
    public int GetRemovedHeelsCount() => _removedHeelsCount;
}
