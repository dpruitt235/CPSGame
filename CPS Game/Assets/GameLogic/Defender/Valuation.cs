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

    private Oracle parentOracle;

    public void UpdateLine()
    {
        if (this.lineRenderer.enabled && this.module)
        {
            this.DrawLine(this.transform.position, this.module.transform.position);
        }
    }

    private void Awake()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        this.lineRenderer.startWidth = 0.1f;
        this.lineRenderer.endWidth = 0.1f;
        this.parentOracle = this.GetComponentInParent<Oracle>();
    }

    private void OnMouseDown()
    {
        if (!this.parentOracle.InputActive)
            return;

        this.Deselect();
    }

    private void OnMouseUp()
    {
        if (!this.parentOracle.InputActive)
            return;

        //determine if it was dragged onto a module, if so, select that module
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            Module mod = hit.transform.GetComponent<Module>();
            if (mod != null)
            {
                this.Select(mod);
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
        if (!this.parentOracle.InputActive)
            return;

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
