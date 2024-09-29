using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAtomController : MonoBehaviour
{
    public static ThrowAtomController instance;

    public GameObject CurrentAtom { get; set; }
    [SerializeField] private Transform _atomTransform;
    [SerializeField] private Transform _parentAfterThrow;
    [SerializeField] private AtomSelector _selector;

    private PlayerController _playerController;

    private Rigidbody2D _rb;
    private CircleCollider2D _circleCollider;

    public Bounds Bounds { get; private set; }

    private const float EXTRA_WIDTH = 0.03f;

    public bool CanThrow { get; set; } = true;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();

        SpawnAAtom(_selector.PickRandomAtomForThrow());
    }

    private void Update()
    {
        if (UserInput.IsThrowPressed && CanThrow)
        {
            int index = CurrentAtom.GetComponent<SpriteIndex>().Index;
            Quaternion rot = CurrentAtom.transform.rotation;

            GameObject go = Instantiate(AtomSelector.instance.Atom, CurrentAtom.transform.position, rot);
            float r = (index+1)*307%256;
            float g = (index+1)*593%256;
            float b = (index+1)*811%256;
            go.GetComponent<SpriteRenderer>().color = new Color(r,g,b);
            float scale = AtomSelector.weights[index] / 31f * 0.6f + 0.1f;
            go.transform.localScale = new Vector3(scale,scale,scale);
            go.GetComponent<AtomInfo>().AtomIndex = index;
            go.GetComponent<AtomInfo>().PointsWhenAnnihilated = AtomSelector.weights[index];
            go.transform.SetParent(_parentAfterThrow);

            Destroy(CurrentAtom);

            CanThrow = false;
        }
    }

    public void SpawnAAtom(GameObject atom)
    {   
        // TODO: fix??
        print("SPAWN A ATOM" + atom);
        GameObject go = Instantiate(atom, _atomTransform);
        CurrentAtom = go;
        _circleCollider = CurrentAtom.GetComponent<CircleCollider2D>();
        Bounds = _circleCollider.bounds;

        _playerController.ChangeBoundary(EXTRA_WIDTH);
    }
}
