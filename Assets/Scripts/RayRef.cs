using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class RayRef : MonoBehaviour
{
    const int Infinity = 999;

    public int maxReflections = 5;
    Slider refsSlider;
    Text refsCount;

    int currentReflections = 0;

    [SerializeField]
    Vector2 startPoint, direction;
    List<Vector3> Points;
    int defaultRayDistance = 100;
    LineRenderer lr;

    // Use this for initialization
    void Start()
    {
        refsSlider = GameObject.Find("ReflectionsS").GetComponent<Slider>();
        refsCount = GameObject.Find("ReflectionsCnt").GetComponent<Text>();

        Points = new List<Vector3>();
        lr = transform.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        maxReflections = (int)refsSlider.value;
        refsCount.text = $"{refsSlider.value}";

        var hitData = Physics2D.Raycast(startPoint, (direction - startPoint).normalized, defaultRayDistance);

        startPoint = new Vector2(transform.parent.transform.parent.gameObject.transform.position.x, transform.parent.transform.parent.gameObject.transform.position.y);
        direction = new Vector2(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y);

        currentReflections = 0;
        Points.Clear();
        Points.Add(startPoint);

        if (hitData)
        {
            ReflectFurther(startPoint, hitData);
        }
        else
        {
            Points.Add(startPoint + (direction - startPoint).normalized * Infinity);
        }

        lr.positionCount = Points.Count;
        lr.SetPositions(Points.ToArray());
    }

    private void ReflectFurther(Vector2 origin, RaycastHit2D hitData)
    {
        if (currentReflections > maxReflections) return;

        Points.Add(hitData.point);
        currentReflections++;

        Vector2 inDirection = (hitData.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);

        var newHitData = Physics2D.Raycast(hitData.point + (newDirection * 0.0001f), newDirection * 100, defaultRayDistance);
        if (newHitData)
        {
            ReflectFurther(hitData.point, newHitData);
        }
        else
        {
            Points.Add(hitData.point + newDirection * defaultRayDistance);
        }
    }
}