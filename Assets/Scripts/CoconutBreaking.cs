using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class RockBreaking : MonoBehaviour
{
    [SerializeField]
    private GameObject Idel;
    [SerializeField]
    private GameObject Breaking;
    [SerializeField]
    private Button Bail_Button;

    private void Start()
    {
        OnClick();
    }
    private void OnClick()
    {
        Bail_Button.onClick.AddListener(delegate {

            Breaking.SetActive(true);
            Idel.SetActive(false);

        });
    }

}
