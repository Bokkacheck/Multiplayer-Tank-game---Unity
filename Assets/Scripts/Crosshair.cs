using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    private Texture2D image;
    [SerializeField]
    private int size;
    private Vector3 screenPosition;

    public static float lookHeight;

    //void OnGUI()
    //{
    //    screenPosition = Camera.main.WorldToScreenPoint(transform.position);
    //    screenPosition.y = Screen.height - screenPosition.y;
    //    GUI.DrawTexture(new Rect(screenPosition.x - size / 2, screenPosition.y - lookHeight, size, size), image);
    //}
}
