using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vegitable : MonoBehaviour, IDrag, IDoubleClick
{
    public int ID;
    public string Name;
    public Sprite vegitableImage;


    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void onStartDrag()
    {
        _rb.useGravity = false;
    }
    public void onEndDrag()
    {
        _rb.useGravity = true;
    }

    public void onDoubleClick()
    {
        this.transform.position.Scale(Vector3.one);
        this.GetComponent<Collider>().enabled = false;
        _rb.isKinematic = true;
        _rb.useGravity = false;
    }
}
