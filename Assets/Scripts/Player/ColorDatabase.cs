using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDatabase : MonoBehaviour
{
    private static readonly Color[] colors = new Color[]
    {
        Color.black,
        Color.blue,
        Color.cyan,
        Color.gray,
        Color.green,
        Color.magenta,
        Color.red,
        Color.white,
        Color.yellow,
    };

    private int lastColorIndex = -1;

    #region Enable/Disable
    private void OnEnable()
    {
        EventManager<bool>.EventRegister(EventKey.Player_ChangeColor_Request, OnColorChangeRequest);
    }

    private void OnDisable()
    {
        EventManager<bool>.EventUnregister(EventKey.Player_ChangeColor_Request, OnColorChangeRequest);
    }

    #endregion

    private void OnColorChangeRequest(bool param) 
    {
        if (colors.Length == 0) return;

        int randomIndex;

        if (colors.Length == 1)
        {
            randomIndex = 0;
        }
        else
        {
            do
            {
                randomIndex = Random.Range(0, colors.Length);
            }
            while (randomIndex == lastColorIndex);
        }

        lastColorIndex = randomIndex;
        Color randomColor = colors[randomIndex];

        EventManager<Color>.EventTrigger(EventKey.Player_ChangeColor_Apply, randomColor);
    }
}
