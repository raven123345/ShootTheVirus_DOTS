using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    [SerializeField]
    Vector3 crush_dir = new Vector3(1f, 1f, 1f);
    [SerializeField]
    bool double_dir = false;

    [HideInInspector]
    public Vector3 collider_bound_center;

    Collider col;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        if(col)
        {
            collider_bound_center = col.bounds.center;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Crush(float force)
    {
        rb.isKinematic = false;
        rb.AddForce(new Vector3(Random.Range(-crush_dir.x, crush_dir.x), Random.Range(crush_dir.y * 0.5f, crush_dir.y), Random.Range(-crush_dir.z, crush_dir.z)) * force, ForceMode.Impulse);
    }

}
