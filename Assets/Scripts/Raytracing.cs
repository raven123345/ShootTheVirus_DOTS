using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raytracing : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Camera cam;
    [SerializeReference]
    float max_raycast = 50f;
    [SerializeField]
    float crush_power = 50f;
    [SerializeField]
    AnimationCurve power_attenuation;
    [SerializeField]
    float search_radius = 2f;

    float max_distance_from_hit = 0f;

    Fracture fr_object;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction * max_raycast, out hit))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red);

                RayCastHitSomething(hit);
            }
        }
    }

    private void FixedUpdate()
    {

    }

    void RayCastHitSomething(RaycastHit hit)
    {
        //Fracture fr;
        //if (hit.transform.TryGetComponent<Fracture>(out fr))
        //{
        //    fr.Crush(Random.Range(crush_power * 0.5f, crush_power));
        //}

        Collider[] obj_in_range = Physics.OverlapSphere(hit.point, search_radius);
        List<Fracture> fracturable = new List<Fracture>();

        foreach (Collider col in obj_in_range)
        {
            Fracture fr;
            if (col.transform.TryGetComponent<Fracture>(out fr))
            {

                fracturable.Add(fr);
            }
        }
        if (fracturable.Count > 0)
        {
            max_distance_from_hit = 0f;
            foreach (Fracture piece in fracturable)
            {
                

                float distance = Vector3.Distance(piece.collider_bound_center, hit.point);
                if (distance > max_distance_from_hit)
                {
                    max_distance_from_hit = distance;
                }
            }

            foreach (Fracture piece in fracturable)
            {
                float distance = Mathf.Clamp(Mathf.Abs((Vector3.Distance(piece.collider_bound_center, hit.point) / max_distance_from_hit)), 0f, 1f);
                Debug.Log("Dist: " + distance);
                //Debug.DrawRay(piece.collider_bound_center, collider_bound_center * transform.up, Color.blue, 5f);
                piece.Crush(Random.Range(crush_power * 0.5f, crush_power) * power_attenuation.Evaluate(distance));
            }
        }
    }
}