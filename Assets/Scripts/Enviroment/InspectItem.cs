using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectItem : MonoBehaviour
{
    public Transform _CamTranform;
    public bool isSetupCam;
    public float FOV;
    public float NCP;
    public GameObject Item;
    public float RotationSpeed;
    private GameObject InstanceItem;

    private void OnEnable()
    {
        if (!Item) return;
        var Model = Instantiate(Item, gameObject.transform.position, gameObject.transform.rotation);
        Model.transform.SetParent(gameObject.transform);
        InstanceItem = Model;

    }

    private void Update()
    {
        if (!Item) return;
        InstanceItem.transform.Rotate(Vector3.up * ( RotationSpeed * Time.deltaTime ));
    }
}
