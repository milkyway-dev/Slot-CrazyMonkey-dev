using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

    [Header("Menu UI")]
    [SerializeField]
    private Button Home_button;
    [SerializeField]
    private Button Menu_Button;
    [SerializeField]
    private GameObject Menu_Object;
    [SerializeField]
    private RectTransform Menu_RT;

    [SerializeField]
    private Button About_Button;
    [SerializeField]
    private GameObject About_Object;
    [SerializeField]
    private RectTransform About_RT;

    [SerializeField]
    private Button Settings_Button;
    [SerializeField]
    private GameObject Settings_Object;
    [SerializeField]
    private RectTransform Settings_RT;

    [SerializeField]
    private Button Exit_Button;
    [SerializeField]
    private GameObject Exit_Object;
    [SerializeField]
    private RectTransform Exit_RT;

    [SerializeField]
    private Button Paytable_Button;
    [SerializeField]
    private GameObject Paytable_Object;
    [SerializeField]
    private RectTransform Paytable_RT;

    [Header("Popus UI")]
    [SerializeField]
    private GameObject MainPopup_Object;

    [Header("About Popup")]
    [SerializeField]
    private GameObject AboutPopup_Object;
    [SerializeField]
    private Button AboutExit_Button;

    [Header("Paytable Popup")]
    [SerializeField]
    private Button Info_button;
    [SerializeField]
    private GameObject PaytablePopup_Object;
    [SerializeField]
    private Button PaytableExit_Button;
    [SerializeField]
    private TMP_Text[] SymbolsText;

    [Header("Settings Popup")]
    [SerializeField]
    private GameObject SettingsPopup_Object;
    [SerializeField]
    private Button SettingsExit_Button;
    [SerializeField]
    private Button Sound_Button;
    [SerializeField]
    private Button Music_Button;
    [SerializeField]
    private AudioSource BG_Sounds;
    [SerializeField]
    private AudioSource Button_Sounds;
    [SerializeField]
    private AudioSource Spin_Sounds;
    [SerializeField]
    private GameObject MusicOn_Object;
    [SerializeField]
    private GameObject MusicOff_Object;
    [SerializeField]
    private GameObject SoundOn_Object;
    [SerializeField]
    private GameObject SoundOff_Object;

    [SerializeField]
    private Button GameExit_Button;

    [SerializeField] private AudioController audioController;

    private bool isMusic = true;
    private bool isSound = true;

    [Header("Win Popup")]
    [SerializeField]
    private GameObject WinPopup_Object;
    [SerializeField]
    private TMP_Text Win_Text;

    [Header("FreeSpins Popup")]
    [SerializeField]
    private GameObject FreeSpinPopup_Object;
    [SerializeField]
    private TMP_Text Free_Text;
    [SerializeField]
    private Button FreeSpin_Button;
    private int FreeSpins;

    [SerializeField]
    private SlotBehaviour slotManager;

    private void Start()
    {

        if (Info_button) Info_button.onClick.RemoveAllListeners();
        if (Info_button) Info_button.onClick.AddListener(delegate { OpenPopup(PaytablePopup_Object); });

        if (PaytableExit_Button) PaytableExit_Button.onClick.RemoveAllListeners();
        if (PaytableExit_Button) PaytableExit_Button.onClick.AddListener(delegate { ClosePopup(PaytablePopup_Object); });

        if (FreeSpin_Button) FreeSpin_Button.onClick.RemoveAllListeners();
        if (FreeSpin_Button) FreeSpin_Button.onClick.AddListener(delegate { StartFreeSpins(FreeSpins); });


        if (MusicOn_Object) MusicOn_Object.SetActive(true);
        if (MusicOff_Object) MusicOff_Object.SetActive(false);

        if (SoundOn_Object) SoundOn_Object.SetActive(true);
        if (SoundOff_Object) SoundOff_Object.SetActive(false);

        if (audioController) audioController.ToggleMute(false);


        isMusic = true;
        isSound = true;

        if (Sound_Button) Sound_Button.onClick.RemoveAllListeners();
        if (Sound_Button) Sound_Button.onClick.AddListener(ToggleSound);

        if (Music_Button) Music_Button.onClick.RemoveAllListeners();
        if (Music_Button) Music_Button.onClick.AddListener(ToggleMusic);

        if (GameExit_Button) GameExit_Button.onClick.RemoveAllListeners();
        if (GameExit_Button) GameExit_Button.onClick.AddListener(CallOnExitFunction);
    }

    internal void PopulateWin(double amount)
    {
        int initAmount = 0;
        if (WinPopup_Object) WinPopup_Object.SetActive(true);
        if (MainPopup_Object) MainPopup_Object.SetActive(true);

        DOTween.To(() => initAmount, (val) => initAmount = val, (int)amount, 5f).OnUpdate(() =>
        {
            if (Win_Text) Win_Text.text = initAmount.ToString();
        });

        DOVirtual.DelayedCall(6f, () =>
        {
            if (WinPopup_Object) WinPopup_Object.SetActive(false);
            if (MainPopup_Object) MainPopup_Object.SetActive(false);
            slotManager.CheckBonusGame();
        });
    }

    void ReturnToHome() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OpenMenu()
    {
        if (Menu_Object) Menu_Object.SetActive(false);
        if (Exit_Object) Exit_Object.SetActive(true);
        if (About_Object) About_Object.SetActive(true);
        if (Paytable_Object) Paytable_Object.SetActive(true);
        if (Settings_Object) Settings_Object.SetActive(true);

        DOTween.To(() => About_RT.anchoredPosition, (val) => About_RT.anchoredPosition = val, new Vector2(About_RT.anchoredPosition.x, About_RT.anchoredPosition.y + 150), 0.1f).OnUpdate(() =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(About_RT);
        });

        DOTween.To(() => Paytable_RT.anchoredPosition, (val) => Paytable_RT.anchoredPosition = val, new Vector2(Paytable_RT.anchoredPosition.x, Paytable_RT.anchoredPosition.y + 300), 0.1f).OnUpdate(() =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(Paytable_RT);
        });

        DOTween.To(() => Settings_RT.anchoredPosition, (val) => Settings_RT.anchoredPosition = val, new Vector2(Settings_RT.anchoredPosition.x, Settings_RT.anchoredPosition.y + 450), 0.1f).OnUpdate(() =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(Settings_RT);
        });
    }

    private void CloseMenu()
    {

        DOTween.To(() => About_RT.anchoredPosition, (val) => About_RT.anchoredPosition = val, new Vector2(About_RT.anchoredPosition.x, About_RT.anchoredPosition.y - 150), 0.1f).OnUpdate(() =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(About_RT);
        });

        DOTween.To(() => Paytable_RT.anchoredPosition, (val) => Paytable_RT.anchoredPosition = val, new Vector2(Paytable_RT.anchoredPosition.x, Paytable_RT.anchoredPosition.y - 300), 0.1f).OnUpdate(() =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(Paytable_RT);
        });

        DOTween.To(() => Settings_RT.anchoredPosition, (val) => Settings_RT.anchoredPosition = val, new Vector2(Settings_RT.anchoredPosition.x, Settings_RT.anchoredPosition.y - 450), 0.1f).OnUpdate(() =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(Settings_RT);
        });

        DOVirtual.DelayedCall(0.1f, () =>
         {
             if (Menu_Object) Menu_Object.SetActive(true);
             if (Exit_Object) Exit_Object.SetActive(false);
             if (About_Object) About_Object.SetActive(false);
             if (Paytable_Object) Paytable_Object.SetActive(false);
             if (Settings_Object) Settings_Object.SetActive(false);
         });
    }

    private void OpenPopup(GameObject Popup)
    {
        if (audioController) audioController.PlayButtonAudio();
        if (audioController)audioController.PlayButtonAudio();
        if (Popup) Popup.SetActive(true);
        if (MainPopup_Object) MainPopup_Object.SetActive(true);
    }

    private void ClosePopup(GameObject Popup)
    {
        if (audioController) audioController.PlayWLAudio("spin");
        if (audioController)audioController.PlayButtonAudio();
        if (Popup) Popup.SetActive(false);
        if (MainPopup_Object) MainPopup_Object.SetActive(false);
    }

    internal void InitialiseUIData(string SupportUrl, string AbtImgUrl, string TermsUrl, string PrivacyUrl, Paylines symbolsText)
    {
        PopulateSymbolsPayout(symbolsText);
    }

    private void PopulateSymbolsPayout(Paylines paylines)
    {
        for (int i = 0; i < paylines.symbols.Count; i++)
        {
            if (i < SymbolsText.Length)
            {
                string text = null;
                if (paylines.symbols[i].multiplier._5x != 0)
                {
                    text += paylines.symbols[i].multiplier._5x;
                }
                if (paylines.symbols[i].multiplier._4x != 0)
                {
                    text += "\n" + paylines.symbols[i].multiplier._4x;
                }
                if (paylines.symbols[i].multiplier._3x != 0)
                {
                    text += "\n" + paylines.symbols[i].multiplier._3x;
                }
                if (paylines.symbols[i].multiplier._2x != 0)
                {
                    text += "\n" + paylines.symbols[i].multiplier._2x;
                }
                if (SymbolsText[i]) SymbolsText[i].text = text;
            }
        }
    }

    private void CallOnExitFunction()
    {
        Application.ExternalCall("window.parent.postMessage", "onExit", "*");
    }

    private void ToggleMusic()
    {
        isMusic = !isMusic;
        if(isMusic)
        {
            if (MusicOn_Object) MusicOn_Object.SetActive(true);
            if (MusicOff_Object) MusicOff_Object.SetActive(false);
            if(audioController) audioController.ToggleMute(false, "bg");
        }
        else
        {
            if (MusicOn_Object) MusicOn_Object.SetActive(false);
            if (MusicOff_Object) MusicOff_Object.SetActive(true);
            if (audioController) audioController.ToggleMute(true, "bg");

        }
    }

    private void ToggleSound()
    {
        isSound = !isSound;
        if(isSound)
        {
            if (SoundOn_Object) SoundOn_Object.SetActive(true);
            if (SoundOff_Object) SoundOff_Object.SetActive(false);
            if (audioController) audioController.ToggleMute(false, "wl");
            if (audioController) audioController.ToggleMute(false, "button");

        }
        else
        {
            if (SoundOn_Object) SoundOn_Object.SetActive(false);
            if (SoundOff_Object) SoundOff_Object.SetActive(true);
            if (audioController) audioController.ToggleMute(true, "wl");
            if (audioController) audioController.ToggleMute(true, "button");
        }
    }

    private void StartFreeSpins(int spins)
    {
        if (MainPopup_Object) MainPopup_Object.SetActive(false);
        if (FreeSpinPopup_Object) FreeSpinPopup_Object.SetActive(false);
        slotManager.FreeSpin(spins);
    }

    internal void FreeSpinProcess(int spins)
    {
        FreeSpins = spins;
        if (FreeSpinPopup_Object) FreeSpinPopup_Object.SetActive(true);
        if (Free_Text) Free_Text.text = spins.ToString();
        if (MainPopup_Object) MainPopup_Object.SetActive(true);
    }
}
