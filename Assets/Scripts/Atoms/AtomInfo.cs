using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomInfo : MonoBehaviour
{
    public int AtomIndex = 0;
    public int PointsWhenAnnihilated = 1;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.mass = (float) PointsWhenAnnihilated;
    }
}
