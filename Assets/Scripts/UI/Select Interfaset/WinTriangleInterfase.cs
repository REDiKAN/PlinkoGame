using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTriangleInterfase : MonoBehaviour
{
    [SerializeField] private Transform mapObject;

    [SerializeField] private List<WinSpot> winSpots;

    [SerializeField] private List<RendererSelect> rendererSelects;

    [SerializeField] private GameObject rendererSelect;
    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        for (int i = 0; i < mapObject.childCount; i++)
        {
            if (mapObject.GetChild(i).tag == "WinTriangle")
            {
                GameObject uiRender = Instantiate(rendererSelect, canvas.transform);

                winSpots.Add(mapObject.GetChild(i).GetComponent<WinSpot>());
                rendererSelects.Add(uiRender.GetComponent<RendererSelect>());
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < rendererSelects.Count; i++)
        {
            rendererSelects[i].RenderInterfase(winSpots[i]);
        }
    }
}
