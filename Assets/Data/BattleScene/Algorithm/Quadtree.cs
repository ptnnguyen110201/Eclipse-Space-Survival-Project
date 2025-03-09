using System.Collections.Generic;
using UnityEngine;

public class Quadtree<T> where T : MonoBehaviour
{
    private Rect boundary;
    private List<T> objects;
    private int maxObjects;
    private bool divided;

    private Quadtree<T> northeast;
    private Quadtree<T> northwest;
    private Quadtree<T> southeast;
    private Quadtree<T> southwest;

    public Quadtree(Rect boundary, int maxObjects = 4)
    {
        this.boundary = boundary;
        this.maxObjects = maxObjects;
        this.objects = new List<T>();
        this.divided = false;
    }

    public void Insert(T obj)
    {
        if (!this.boundary.Contains(obj.transform.position)) return;

        if (this.objects.Count < maxObjects)
        {
            this.objects.Add(obj);
        }
        else
        {
            if (!this.divided) Subdivide();

            this.northeast.Insert(obj);
            this.northwest.Insert(obj);
            this.southeast.Insert(obj);
            this.southwest.Insert(obj);
        }
    }

    private void Subdivide()
    {
        Vector2 size = new Vector2(boundary.width / 2, boundary.height / 2);
        float x = boundary.x;
        float y = boundary.y;

        this.northeast = new Quadtree<T>(new Rect(x + size.x, y, size.x, size.y), maxObjects);
        this.northwest = new Quadtree<T>(new Rect(x, y, size.x, size.y), maxObjects);
        this.southeast = new Quadtree<T>(new Rect(x + size.x, y + size.y, size.x, size.y), maxObjects);
        this.southwest = new Quadtree<T>(new Rect(x, y + size.y, size.x, size.y), maxObjects);

        this.divided = true;
    }

    public List<T> Query(Rect range)
    {
        List<T> found = new();

        if (!this.boundary.Overlaps(range)) return found;

        foreach (var obj in this.objects)
        {
            if (obj != null && range.Contains(obj.transform.position))
            {
                found.Add(obj);
            }
        }

        if (this.divided)
        {
            found.AddRange(this.northeast.Query(range));
            found.AddRange(this.northwest.Query(range));
            found.AddRange(this.southeast.Query(range));
            found.AddRange(this.southwest.Query(range));
        }

        return found;
    }

    public void Remove(T obj)
    {
        this.objects.Remove(obj);

        if (this.divided)
        {
            this.northeast.Remove(obj);
            this.northwest.Remove(obj);
            this.southeast.Remove(obj);
            this.southwest.Remove(obj);
        }
    }

    public void Update(T obj)
    {
        Remove(obj);
        Insert(obj);
    }

}
