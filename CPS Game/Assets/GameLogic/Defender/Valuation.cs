using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Valuation : MonoBehaviour
{
    /// <summary>
    /// The Module the valuation is currently pointed at
    /// </summary>
    public Module CurrentSelection {
        get {
            if (this.module) return this.module;
            return null;
        }
    }

    private Module module;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        this.lineRenderer.startWidth = 0.1f;
        this.lineRenderer.endWidth = 0.1f;
    }

    private void OnMouseDown()
    {
        this.Deselect();
    }

    private void OnMouseUp()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            Module mod = hit.transform.GetComponent<Module>();
            if (mod != null)
            {
                this.Select(mod);
                Debug.Log(mod.gameObject.name);
            }
            else
            {
                this.Deselect();
            }
        }
        else
        {
            this.Deselect();
        }
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.DrawLine(this.transform.position, mousePos);
    }

    private void DrawLine(Vector3 start, Vector3 end)
    {
        this.lineRenderer.enabled = true;
        this.lineRenderer.SetPositions(new List<Vector3>()
        {
            new Vector3(start.x, start.y, -10),
            new Vector3(end.x, end.y, -10)
        }.ToArray());
    }

    private void Select(Module mod)
    {
        this.module = mod;
        this.DrawLine(this.transform.position, mod.transform.position);
    }

    private void Deselect()
    {
        this.module = null;
        this.lineRenderer.enabled = false;
    }
}
