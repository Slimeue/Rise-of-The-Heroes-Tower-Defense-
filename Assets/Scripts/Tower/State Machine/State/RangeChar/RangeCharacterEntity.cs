using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCharacterEntity : CharEntity
{
    [HideInInspector]
    public float radius;

    public float _rotationSpeed;

    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Vector3 _targetDir;


    public bool _inRange;


    [SerializeField] public TowerTesting[] towerTesting;


    public override void Awake()
    {
        base.Awake();
        towerTesting = FindObjectsOfType<TowerTesting>();

    }

    private void Start() { }
}
