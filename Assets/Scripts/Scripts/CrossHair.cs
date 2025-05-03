using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [Header("RectileSettings")]
    [Range(0,100)]
    [SerializeField] private float rectileValue;
    [SerializeField] private float rectileSpeed;
    [SerializeField] private float rectileMargin;

    [Header("RectTransforms")]
    [SerializeField] private RectTransform center;
    [SerializeField] private RectTransform top;
    [SerializeField] private RectTransform bottom;
    [SerializeField] private RectTransform left;
    [SerializeField] private RectTransform right;

    [Header("RectileUpdates")]
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Aim aim;

    private void Update()
    {
        RectileSmoothing();
    }


    private void RectileSmoothing()
    {
        float topValue;
        float bottomValue;
        float leftValue;
        float rightValue;

        topValue = Mathf.Lerp(top.position.y, center.position.y + rectileMargin + rectileValue, rectileSpeed * Time.deltaTime);
        bottomValue = Mathf.Lerp(bottom.position.y, center.position.y - rectileMargin - rectileValue, rectileSpeed * Time.deltaTime);

        rightValue = Mathf.Lerp(right.position.x, center.position.x + rectileMargin + rectileValue, rectileSpeed * Time.deltaTime);
        leftValue = Mathf.Lerp(left.position.x, center.position.x - rectileMargin - rectileValue, rectileSpeed * Time.deltaTime);


        top.position = new Vector2(top.position.x, topValue);
        bottom.position = new Vector3(bottom.position.x, bottomValue);

        left.position = new Vector2(leftValue,center.position.y);
        right.position = new Vector2(rightValue, center.position.y);


        if (!aim.isAiming)
        {
            rectileValue = player.CurrentSpeed * 8f;
        }
        else
        {
            rectileValue = 20f;
        }
    }

}
