using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Data.Common;
using System.Data;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI leveltext;

    [SerializeField] Button bonusButton;
    [SerializeField] TextMeshProUGUI bonusText;


    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;

    [SerializeField] Button MusicButton;
    [SerializeField] Button SoundButton;
    [SerializeField] Button HapticButton;

    [SerializeField] Image RewardPanel;

    void Start()
    {
        leveltext.text = "Level " + (Data.instance.GetCurrentLevel() + 1);

        MusicSetting();
        SoundSetting();
        HapticSetting();
        BonusButton();


    }

    public void ButtonClickSound()
    {
        AudioManager.instance.Play("ButtonClick");
    }

    public void BonusButton()
    {
        if (Data.instance.GetBonusRewarded())
        {
            bonusText.text = "Bonus Rewarded";
            bonusButton.interactable = false;
        }
        else if ((Data.instance.GetCurrentLevel()) % 3 == 0)
        {
            bonusText.text = "Bonus  can be obtained.";

            bonusButton.interactable = true;
        }
        else
        {
            bonusText.text = "Bonus " + (Data.instance.GetCurrentLevel()) + "/3";
            bonusButton.interactable = false;
        }



    }

    public void GetBonus()
    {
        Data.instance.SetCurrencyMenu(30);
        Data.instance.SetBonusRewarded();
        bonusText.text = "Bonus Rewarded";
        bonusButton.interactable = false;

        RewardPanel.gameObject.SetActive(true);
        AudioManager.instance.Play("Reward");
        RewardPanel.transform.DOMove(transform.position, 2f)
            .SetEase(Ease.InOutBack)
            .OnComplete(() =>
            {
                RewardPanel.gameObject.SetActive(false);
            });
    }

    #region Settings Buttons

    private void MusicSetting()
    {
        if (Data.instance.GetMusic())
        {
            MusicButton.image.sprite = onSprite;
        }
        else
        {
            MusicButton.image.sprite = offSprite;
        }
    }
    private void SoundSetting()
    {
        if (Data.instance.GetMusic())
        {
            SoundButton.image.sprite = onSprite;
        }
        else
        {
            SoundButton.image.sprite = offSprite;
        }
    }
    private void HapticSetting()
    {
        if (Data.instance.GetMusic())
        {
            HapticButton.image.sprite = onSprite;
        }
        else
        {
            HapticButton.image.sprite = offSprite;
        }
    }
    public void MusicSettingChange()
    {

        if (Data.instance.GetMusic())
        {
            Data.instance.OnSetMusic(false);
            MusicButton.image.sprite = offSprite;
        }
        else
        {
            Data.instance.OnSetMusic(true);
            MusicButton.image.sprite = onSprite;

        }

    }
    public void SoundSettingChange()
    {
        if (Data.instance.GetSound())
        {
            Data.instance.OnSetSound(false);
            SoundButton.image.sprite = offSprite;
        }
        else
        {
            Data.instance.OnSetSound(true);
            SoundButton.image.sprite = onSprite;
        }

    }
    public void HapticSettingChange()
    {


        if (Data.instance.GetHapticFeedback())
        {
            Data.instance.OnSetHapticFeedback(false);
            HapticButton.image.sprite = offSprite;
        }
        else
        {
            Data.instance.OnSetHapticFeedback(true);
            HapticButton.image.sprite = onSprite;
        }

    }
    #endregion


    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }

}
