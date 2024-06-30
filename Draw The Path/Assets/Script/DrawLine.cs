using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawLine : MonoBehaviour
{

    public GameObject linePrefab;
    GameObject draw;
    LineRenderer _lineRenderer;
    EdgeCollider2D _edgeCollider;
    public List<Vector2> fingerPozList;

    public List<GameObject> Lines;

    [SerializeField] private TextMeshProUGUI drawCountText;
    int drawCount;

    private void Start()
    {
        drawCount = 3;
        drawCountText.text = drawCount.ToString();
    }



    void Update()
    {
        if (Time.timeScale != 0 && drawCount != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DrawCreate();
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 fingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Vector2.Distance(fingerPosition, fingerPozList[^1]) > .1f)
                {
                    DrawUptade(fingerPosition);
                }
            }
        }


        //Liste içinde eleman varsa ve sayý 0 deðilse azalt.
        if (Lines.Count != 0 && drawCount != 0)
        {
            if (Input.GetMouseButtonUp(0))
            {
                drawCount--;
                drawCountText.text = drawCount.ToString();
            }
        }
    }


    void DrawCreate()
    {
        // Çizgi oluþturma.
        draw = Instantiate(linePrefab, Vector2.zero, Quaternion.identity);
        Lines.Add(draw);
        _lineRenderer = draw.GetComponent<LineRenderer>();
        _edgeCollider = draw.GetComponent<EdgeCollider2D>();
        fingerPozList.Clear();


        // LineRenderer ve EdgeColider Çift poziyon alýyor x ,y ;
        fingerPozList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPozList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        _lineRenderer.SetPosition(0, fingerPozList[0]);
        _lineRenderer.SetPosition(1, fingerPozList[1]);

        //Liste boyutu kadar edgecolider boyutu artýcak.
        _edgeCollider.points = fingerPozList.ToArray();
    }

    void DrawUptade(Vector2 incomingFingerPoz)
    {
        fingerPozList.Add(incomingFingerPoz);
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, incomingFingerPoz);
        _edgeCollider.points = fingerPozList.ToArray();
    }

    public void KeepGoing()
    {
        if(ThrowBall.throwBallCount == 0)
        {
            // Top girdiðinde yapýlacak iþlemler.
            foreach (var item in Lines)
            {
                Destroy(item.gameObject);
            }

            Lines.Clear();

            drawCount = 3;
            drawCountText.text = drawCount.ToString();
        }
    }
}
