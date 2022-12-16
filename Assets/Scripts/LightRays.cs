using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRays : MonoBehaviour
{

    LineRenderer lineRenderer;
    [SerializeField] Transform lightTip;
    Vector2 incPoint;
    Vector2 incDir;
    int index;

    bool updateRay;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        incPoint = new Vector2(lightTip.position.x, lightTip.position.y);
        incDir = Vector3.right;
        updateRay = true;
        index = 0;
        //lineRenderer.positionCount = 10;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(index++, incPoint);
    }

    // Update is called once per frame
    void Update()
    {
        //Laser();
    }

    public void startLaser()
    {
        //lineRenderer.enabled = false;
        InvokeRepeating("Laser", 1, 0.5f);
    }

    void Laser()
    {
        RaycastHit2D hit;

        if (!updateRay)
            return;

        updateRay = false;

        if (hit = Physics2D.Raycast(incPoint, incDir))
        {
            lineRenderer.enabled = true;

            if (hit.collider.tag == "Mirror")
            {
                hit.collider.tag = "Hitted";
                hit.collider.enabled = false;
                lineRenderer.positionCount = index + 1;
                lineRenderer.SetPosition(index++, hit.point);

                incDir = (hit.point - incPoint).normalized;
                incPoint = hit.point;
                var norm = hit.transform.up.normalized;

                Vector2 rfl = Vector2.Reflect(incDir, norm);
                incDir = rfl;
                //lineRenderer.SetPosition(3, pos);
                updateRay = true;
            }

            else if(hit.collider.tag == "Target")
            {
                lineRenderer.positionCount = index + 1;
                lineRenderer.SetPosition(index, hit.point);
                updateRay = false;
            }
        }
    }
}
