using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{
    private Text lab;
    private void Awake()
    {
        lab = transform.Find("Text").GetComponent<Text>();
    }

    private void Start()
    {
        string temp = "lua integration state where scale in projects lua integration state where scale in projects lua integration state where scale in projects lua integration state where scale in projectslua integration state where scale in projects";
        temp = temp.Replace(" ", "\u00A0");
        lab.text = temp;
    }

    private void Update()
    {
        string temp = "lua integration state where scale in projects lua integration state where scale in projects lua integration state where scale in projects lua integration state where scale in projectslua integration state where scale in projects";
        temp = temp.Replace(" ", "\u00A0");
        lab.text = temp;
        WrapTextWithHyphen(lab, temp);
    }

    public void WrapTextWithHyphen(Text textComponent, string str)
    {
        string originalText = str;
        string[] words = originalText.Split('\u00A0');

        float textWidth = textComponent.rectTransform.rect.width;
        string wrappedText = "";
        string line = "";
        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            string spaceLine = line + "\u00A0";
            if (GetTextPreferredWidth(textComponent,spaceLine + word) > textWidth)
            {
                for (int j = 1; j <= word.Length; j++)
                {
                    string substring = word.Substring(0, j);
                    if (GetTextPreferredWidth(textComponent,spaceLine + substring + "-") > textWidth)
                    {
                        //防止刚好是空格+单个字母+"-"超行的情况，这个时候直接把这个单词放到下一行
                        if (j == 1)
                        {
                            wrappedText += line + " ";
                            line = word;
                        }
                        else
                        {
                            string tempStr = spaceLine + word.Substring(0, j - 1) + "-\n";
                            wrappedText += tempStr;
                            line = word.Substring(j - 1);
                        }
                        break;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(line))
                {
                    line += "\u00A0"; 
                }
                line += word;
            }
        }

        wrappedText += line;
        textComponent.supportRichText = true;
        textComponent.text = wrappedText;
    }

    public float GetTextPreferredWidth(Text textComp, string content)
    {
        return textComp.cachedTextGenerator.GetPreferredWidth(content,
            textComp.GetGenerationSettings(textComp.rectTransform.rect.size));
    }
}
