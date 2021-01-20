using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plotter : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Prefab containing a line renderer")]
    public GameObject lineRenderer;

    [Tooltip("Shader used for rendering the plotter lines\n" +
             "we use particles/additive")]
    public Shader shader;
    
    private GameObject xRendererObject;
    private GameObject yRendererObject;
    private GameObject zRendererObject;
    
    private LineRenderer xRenderer;
    private LineRenderer yRenderer;
    private LineRenderer zRenderer;
    
    private Color xColor = Color.red;
    private Color yColor = Color.green;
    private Color zColor = Color.blue;
    private Color axisColor = Color.white;
    
    private float zPosition;
    
    private float ySection;
    private float xySection;
    private float yySection;
    private float zySection;
    
    private float xMinPos;
    private float xMaxPos;
    
    private List<float> xValues;
    private List<float> yValues;
    private List<float> zValues;


    private void Start()
    {
        // Setup
        // We basically instantiate three new objects and get the renderes therein
        xRendererObject = (GameObject) Instantiate(lineRenderer);
        yRendererObject = (GameObject) Instantiate(lineRenderer);
        zRendererObject = (GameObject) Instantiate(lineRenderer);

        xRenderer = (LineRenderer) xRendererObject.GetComponent<LineRenderer>();
        yRenderer = (LineRenderer) yRendererObject.GetComponent<LineRenderer>();
        zRenderer = (LineRenderer) zRendererObject.GetComponent<LineRenderer>();


        // We do the same a second time for the zero-axis lines
        // but these instances only display static lines
        // thus they can be local
        var xAxisRendererObject = (GameObject) Instantiate(lineRenderer);
        var yAxisRendererObject = (GameObject) Instantiate(lineRenderer);
        var zAxisRendererObject = (GameObject) Instantiate(lineRenderer);

        var xAxisRenderer = (LineRenderer) xAxisRendererObject.GetComponent<LineRenderer>();
        var yAxisRenderer = (LineRenderer) yAxisRendererObject.GetComponent<LineRenderer>();
        var zAxisRenderer = (LineRenderer) zAxisRendererObject.GetComponent<LineRenderer>();

        // Setup materials (colors), line widths and set the vertex counts to 1000
        // Each line will be made up of 1000 data points
        xRenderer.material = new Material(shader);
        xRenderer.startColor = xColor;
        xRenderer.endColor = xColor;
        xRenderer.startWidth = 0.002f;
        xRenderer.endWidth = 0.002f;
        xRenderer.positionCount = 1000;

        yRenderer.material = new Material(shader);
        yRenderer.startColor = yColor;
        yRenderer.endColor = yColor;
        yRenderer.startWidth = 0.002f;
        yRenderer.endWidth = 0.002f;
        yRenderer.positionCount = 1000;

        zRenderer.material = new Material(shader);
        zRenderer.startColor = zColor;
        zRenderer.endColor = zColor;
        zRenderer.startWidth = 0.002f;
        zRenderer.endWidth = 0.002f;
        zRenderer.positionCount = 1000;


        // Do the same for the zero axis renders, but we only use 2 points
        // left and right center point
        xAxisRenderer.material = new Material(shader);
        xAxisRenderer.startColor = axisColor;
        xAxisRenderer.endColor = axisColor;
        xAxisRenderer.startWidth = 0.0005f;
        xAxisRenderer.endWidth = 0.0005f;
        xAxisRenderer.positionCount = 2;

        yAxisRenderer.material = new Material(shader);
        yAxisRenderer.startColor = axisColor;
        yAxisRenderer.endColor = axisColor;
        yAxisRenderer.startWidth = 0.0005f;
        yAxisRenderer.endWidth = 0.0005f;
        yAxisRenderer.positionCount = 2;

        zAxisRenderer.material = new Material(shader);
        zAxisRenderer.startColor = axisColor;
        zAxisRenderer.endColor = axisColor;
        zAxisRenderer.startWidth = 0.0005f;
        zAxisRenderer.endWidth = 0.0005f;
        zAxisRenderer.positionCount = 2;


        // we get the zPosition as the near clipping plane
        zPosition = Camera.main.nearClipPlane;

        // divide the screen height in three graph sections
        ySection = Screen.height / 4.0f;
        xySection = ySection * 3;
        yySection = ySection * 2;
        zySection = ySection * 1;

        // minimum x position 0, maximum screen width
        xMinPos = 0;
        xMaxPos = Screen.width;

        Vector3 position;


        // compute the 3d world coordinates for the axis lines
        // ScreenToWorldPoint does the transformation from screen pixel coordinates to
        // 3D world coordinates
        // Set them in the axis renderers
        position = new Vector3(xMinPos, xySection, zPosition);
        xAxisRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(position));
        position = new Vector3(xMaxPos, xySection, zPosition);
        xAxisRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(position));

        position = new Vector3(xMinPos, yySection, zPosition);
        yAxisRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(position));
        position = new Vector3(xMaxPos, yySection, zPosition);
        yAxisRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(position));

        position = new Vector3(xMinPos, zySection, zPosition);
        zAxisRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(position));
        position = new Vector3(xMaxPos, zySection, zPosition);
        zAxisRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(position));


        // create three lists for the x,y and z sensor values
        // and initialize them with 1000 data points
        // we just use some sine here
        xValues = new List<float>();
        yValues = new List<float>();
        zValues = new List<float>();
        for (var i = 0; i < 1000; i++)
        {
            xValues.Add(Mathf.Sin(i / 1000.0f * Mathf.PI * 4.0f));
            yValues.Add(Mathf.Sin(i / 1000.0f * Mathf.PI * 4.0f));
            zValues.Add(Mathf.Sin(i / 1000.0f * Mathf.PI * 4.0f));
        }
    }

    private void OnGUI()
    {
        // draw a new label and show the received serial data
        GUI.Label(new Rect(10, 10, 220, 20), "Data: [values go here]");
    }

    private void FixedUpdate()
    {
        Vector3 position;

        // get the accelerometer values
        var tempData = GetComponent<AccelerometerInput>().GetDirection();


        // drop the left most data point
        // and add the new sensor value at the right edge
        // note that we normalize the values between -1 and 1
        xValues.RemoveAt(0);
        xValues.Add(tempData.x);

        yValues.RemoveAt(0);
        yValues.Add(tempData.y);

        zValues.RemoveAt(0);
        zValues.Add(tempData.z);

        // update all the points for the three line renderes
        for (var i = 0; i < 1000; i++)
        {
            // we scale the sensor values to fit comfortably between two sections
            var yPos = xySection + ySection * 0.707f * 0.5f * xValues[i];
            var xPos = xMinPos + (xMaxPos - xMinPos) / 1000.0f * i;
            position = new Vector3(xPos, yPos, zPosition);
            xRenderer.SetPosition(i, Camera.main.ScreenToWorldPoint(position));
        }

        for (var i = 0; i < 1000; i++)
        {
            var yPos = yySection + ySection * 0.707f * 0.5f * yValues[i];
            var xPos = xMinPos + (xMaxPos - xMinPos) / 1000.0f * i;
            position = new Vector3(xPos, yPos, zPosition);
            yRenderer.SetPosition(i, Camera.main.ScreenToWorldPoint(position));
        }

        for (var i = 0; i < 1000; i++)
        {
            var yPos = zySection + ySection * 0.707f * 0.5f * zValues[i];
            var xPos = xMinPos + (xMaxPos - xMinPos) / 1000.0f * i;
            position = new Vector3(xPos, yPos, zPosition);
            zRenderer.SetPosition(i, Camera.main.ScreenToWorldPoint(position));
        }
    }
}