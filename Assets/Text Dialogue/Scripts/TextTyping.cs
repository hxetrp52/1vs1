using UnityEngine;
using TMPro;
using System.Collections;
using System.Text.RegularExpressions;

public class TextTyping : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    [TextArea] public string[] scripts;

    private int nowText = -1;
    private int lastText = 0;

    private bool isTyping = false;
    private bool skipTyping = false;

    private void Start()
    {
        lastText = scripts.Length;
        Time.timeScale = 0;
        StartTyping();
    }

    public void StartTyping()
    {
        if (!isTyping && nowText + 1 < lastText)
        {
            nowText++;
            StartCoroutine(Typing(scripts[nowText]));
        }
    }


    private void Update()
    {
        if (isTyping && Input.GetMouseButtonDown(0))
        {
            skipTyping = true;
        }
        else if (!isTyping && Input.GetMouseButtonDown(0) && nowText + 1 < lastText)
        {
            StartTyping();
        }
        else if (!isTyping && nowText + 1 >= lastText && Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;
            Destroy(gameObject); // 대화 끝나면 오브젝트 삭제
        }
    }


    IEnumerator Typing(string rawText)
    {
        isTyping = true;
        skipTyping = false;

        // TMP 태그 제거 (ex. <color=#FF0000>)
        string displayText = Regex.Replace(rawText, "<.*?>", "");
        textUI.text = "";

        for (int i = 0; i < displayText.Length; i++)
        {
            if (skipTyping)
            {
                textUI.text = displayText;
                break;
            }

            textUI.text += displayText[i];
            yield return new WaitForSecondsRealtime(0.05f); // 글자당 지연 시간
        }

        isTyping = false;
    }
}
