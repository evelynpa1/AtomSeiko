using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomColliderInformer : MonoBehaviour
{
    public bool WasCombinedIn { get; set; }

    private bool _hasCollided;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_hasCollided && !WasCombinedIn)
        {
            _hasCollided = true;
            ThrowAtomController.instance.CanThrow = true;
            ThrowAtomController.instance.SpawnAAtom(AtomSelector.instance.NextAtom);
            AtomSelector.instance.PickNextAtom();
            Destroy(this);
        }
    }
}
