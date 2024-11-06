using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class RendererSelect : MonoBehaviour
{
    [SerializeField] private WinSpot winSpots;

    [SerializeField] private TMP_Text rewardCoins_txt;

    [Header("Vidual Rect Distance")]
    [SerializeField] private float additionalDistance = 5f;
    [SerializeField] private Vector2 centerDistance = Vector2.zero;

    public void RenderInterfase(WinSpot _selectObj)
    {
        winSpots = _selectObj;

        SetText();

        Rect vidualRect = RendererBoundsinScreenSpace(winSpots.gameObject.GetComponentInChildren<Renderer>());
        RectTransform rt = GetComponent<RectTransform>();

        rt.position = new Vector2(vidualRect.xMin + centerDistance.x, vidualRect.yMin + centerDistance.y);
        rt.sizeDelta = new Vector2(vidualRect.width + additionalDistance, vidualRect.height + additionalDistance);
    }

    private void SetText() => rewardCoins_txt.text = winSpots.RewardCoins.ToString();

    public void IdelAnim()
    {
        DOTween.Sequence()
            .Append(transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 1f))
            .Append(transform.DOScale(new Vector3(1f, 1f, 1f), 1f))
            .SetLoops(-1);
    }

    static Rect RendererBoundsinScreenSpace(Renderer r)
    {
        Bounds bigBounds = r.bounds;

        Vector3[] screenSpaceCorners = new Vector3[8];

        Camera mainCamera = Camera.main;

        screenSpaceCorners[0] = mainCamera.WorldToScreenPoint(new Vector3(bigBounds.center.x + bigBounds.extents.x, bigBounds.center.y + bigBounds.extents.y, bigBounds.center.z + bigBounds.extents.z));
        screenSpaceCorners[1] = mainCamera.WorldToScreenPoint(new Vector3(bigBounds.center.x + bigBounds.extents.x, bigBounds.center.y + bigBounds.extents.y, bigBounds.center.z - bigBounds.extents.z));
        screenSpaceCorners[2] = mainCamera.WorldToScreenPoint(new Vector3(bigBounds.center.x + bigBounds.extents.x, bigBounds.center.y - bigBounds.extents.y, bigBounds.center.z + bigBounds.extents.z));
        screenSpaceCorners[3] = mainCamera.WorldToScreenPoint(new Vector3(bigBounds.center.x + bigBounds.extents.x, bigBounds.center.y - bigBounds.extents.y, bigBounds.center.z - bigBounds.extents.z));
        screenSpaceCorners[4] = mainCamera.WorldToScreenPoint(new Vector3(bigBounds.center.x - bigBounds.extents.x, bigBounds.center.y + bigBounds.extents.y, bigBounds.center.z + bigBounds.extents.z));
        screenSpaceCorners[5] = mainCamera.WorldToScreenPoint(new Vector3(bigBounds.center.x - bigBounds.extents.x, bigBounds.center.y + bigBounds.extents.y, bigBounds.center.z - bigBounds.extents.z));
        screenSpaceCorners[6] = mainCamera.WorldToScreenPoint(new Vector3(bigBounds.center.x - bigBounds.extents.x, bigBounds.center.y - bigBounds.extents.y, bigBounds.center.z + bigBounds.extents.z));
        screenSpaceCorners[7] = mainCamera.WorldToScreenPoint(new Vector3(bigBounds.center.x - bigBounds.extents.x, bigBounds.center.y - bigBounds.extents.y, bigBounds.center.z - bigBounds.extents.z));

        float min_x = screenSpaceCorners[0].x;
        float min_y = screenSpaceCorners[0].y;
        float max_x = screenSpaceCorners[0].x;
        float max_y = screenSpaceCorners[0].y;

        for (int i = 0; i < 8; i++)
        {
            if (screenSpaceCorners[i].x < min_x) { min_x = screenSpaceCorners[i].x; }
            if (screenSpaceCorners[i].y < min_y) { min_y = screenSpaceCorners[i].y; }
            if (screenSpaceCorners[i].x > max_x) { max_x = screenSpaceCorners[i].x; }
            if (screenSpaceCorners[i].y > max_y) { max_y = screenSpaceCorners[i].y; }
        }

        return Rect.MinMaxRect(min_x, min_y, max_x, max_y);
    }
}

internal class TMP_Pro
{
}