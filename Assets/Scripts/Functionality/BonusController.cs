using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class BonusController : MonoBehaviour
{
    [SerializeField]
    private GameObject Bonus_Object;
    [SerializeField]
    private SlotBehaviour slotManager;
    [SerializeField]
    private GameObject raycastPanel;
    [SerializeField]
    private List<CoconutBreaking> BonusCases;
    [SerializeField]
    private AudioController _audioManager;
    [SerializeField]
    private TMP_Text Win_Text;

    [SerializeField]
    private List<int> CaseValues;

    int index = 0;
    int winAmount = 0;

    internal void GetBailCaseList(List<int> values)
    {
        index = 0;
        CaseValues.Clear();
        CaseValues.TrimExcess();
        CaseValues = values;
        if (Win_Text) Win_Text.text = "0";
        winAmount = 0;

        foreach (CoconutBreaking cases in BonusCases)
        {
            cases.ResetCase();
        }

        for (int i = 0; i < CaseValues.Count; i++)
        {
            if (CaseValues[i] == -1)
            {
                CaseValues.RemoveAt(i);
                CaseValues.Add(-1);
            }
        }

        if (raycastPanel) raycastPanel.SetActive(false);
        StartBonus();
    }

    internal void enableRayCastPanel(bool choice)
    {
        if (raycastPanel) raycastPanel.SetActive(choice);
    }

    internal void GameOver()
    {
        slotManager.CheckPopups = false;
        if (Bonus_Object) Bonus_Object.SetActive(false);
        if (_audioManager) _audioManager.SwitchBGSound(false);
    }

    internal int GetValue()
    {
        int value = 0;

        value = CaseValues[index];

        winAmount += value;

        index++;

        if (_audioManager) _audioManager.PlayBonusAudio("coconut");

        return value;
    }

    private void StartBonus()
    {
        if (_audioManager) _audioManager.SwitchBGSound(true);
        if (Bonus_Object) Bonus_Object.SetActive(true);
    }

    internal void PlayWinSound()
    {
        if (_audioManager) _audioManager.PlayBonusAudio("win");
    }

    internal void PlayLoseSound()
    {
        if (_audioManager) _audioManager.PlayBonusAudio("lose");
    }

    internal void UpdateWinText()
    {
        if (Win_Text) Win_Text.text = winAmount.ToString();
    }
}
