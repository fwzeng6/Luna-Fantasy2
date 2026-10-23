using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Image hpMaskImage;
    public Image mpMaskImage;
    public float originalSize;//血条原始宽度
    public GameObject batterPanelGo;
    public GameObject TalkPanelGo;
    public Image characterImage;
    public Text contentText;
    public Text nameText;
    public List<Sprite> characterSprites; 


    void Awake()  //Awake()在Start()之前被自动调用
    {
        Instance = this; //单例instance()的实例化
        originalSize = hpMaskImage.rectTransform.rect.width;
        setHPValue(1.0f);
    }

    /// <summary>
    /// 血条UI填充显示
    /// </summary>
    /// <param name="fillPercent">填充百分比</param>
  public void setHPValue(float fillPercent)
  {

        hpMaskImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,fillPercent*originalSize);
  }

    /// <summary>
    /// 蓝条UI填充显示
    /// </summary>
    /// <param name="fillPercent">填充百分比</param>
    public void setMPValue(float fillPercent)
    {

        mpMaskImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fillPercent * originalSize);
    }

    public void ShowOrHideBatterPanel(bool show)
    {
        batterPanelGo.SetActive(show);
    }


    //显示对话内容（包含名字，图片，对话内容的更换）

    public void ShowDialog(string content = null, string name = null)
    {
        if (content == null)
        {
            TalkPanelGo.SetActive(false);
        }
        else
        {
            TalkPanelGo.SetActive(true);
            if (name != null)
            {
                if (name == "Luna")
                {
                    characterImage.sprite = characterSprites[0];
                }
                else
                {
                    characterImage.sprite = characterSprites[1];
                }
                characterImage.SetNativeSize();
            }
            contentText.text = content;
            nameText.text = name;
        }
    }
}
