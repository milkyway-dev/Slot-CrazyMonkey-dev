using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardFlip : MonoBehaviour
{
    [SerializeField]
    private Sprite Card_Sprite;
    [SerializeField]
    private Transform Card_RT;
    [SerializeField]
    private Button Card_Button;
    [SerializeField]
    private Image Card_Image;
    bool once = false;

    private void Start()
    {
        if (Card_Button) Card_Button.onClick.RemoveAllListeners();
        if (Card_Button) Card_Button.onClick.AddListener(FlipMyObject);
    }

    private void FlipMyObject()
    {
        if (!once)
        {
            Card_RT.localEulerAngles = new Vector3(0, 180, 0);
            once = true;
            Card_RT.DORotate(new Vector3(0, 0, 0), 1, RotateMode.FastBeyond360);
            DOVirtual.DelayedCall(0.3f, () =>
            {
                if (Card_Image) Card_Image.sprite = Card_Sprite;
            });
        }
    }
}
