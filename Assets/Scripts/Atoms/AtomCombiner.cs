using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomCombiner : MonoBehaviour
{
    private int _layerIndex;

    private AtomInfo _info;

    private void Awake()
    {
        _info = GetComponent<AtomInfo>();
        _layerIndex = gameObject.layer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _layerIndex)
        {
            AtomInfo info = collision.gameObject.GetComponent<AtomInfo>();
            if (info != null)
            {
                if (info.AtomIndex == _info.AtomIndex)
                {
                    int thisID = gameObject.GetInstanceID();
                    int otherID = collision.gameObject.GetInstanceID();

                    if (thisID > otherID)
                    {
                        GameManager.instance.IncreaseScore(_info.PointsWhenAnnihilated);

                        if (_info.AtomIndex == AtomSelector.elements.Length -1)
                        {
                            Destroy(collision.gameObject);
                            Destroy(gameObject);
                        }

                        else
                        {
                            Vector3 middlePosition = (transform.position + collision.transform.position) / 2f;
                            GameObject go = Instantiate(SpawnCombinedAtom(), GameManager.instance.transform);
                            go.transform.position = middlePosition;
                            float r = (_info.AtomIndex+2)*307%256;
                            float g = (_info.AtomIndex+2)*593%256;
                            float b = (_info.AtomIndex+2)*811%256;
                            // go.GetComponent<SpriteRenderer>().color = new Color(r,g,b);
                            float scale = AtomSelector.weights[_info.AtomIndex+2] / 31f * 0.6f + 0.1f;
                            go.transform.localScale = new Vector3(scale,scale,scale);
                            go.GetComponent<AtomInfo>().AtomIndex = _info.AtomIndex+1;
                            go.GetComponent<AtomInfo>().PointsWhenAnnihilated = AtomSelector.weights[_info.AtomIndex+2];

                            ColliderInformer informer = go.GetComponent<ColliderInformer>();
                            if (informer != null)
                            {
                                informer.WasCombinedIn = true;
                            }

                            Destroy(collision.gameObject);
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }

    private GameObject SpawnCombinedAtom()
    {
        GameObject go = AtomSelector.instance.Atom;
        return go;
    }
}
