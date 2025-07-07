using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SelectUI : MonoBehaviour
{
    [SerializeField] private int selectNumber = 0;
    [SerializeField] private Image selectImage;
    [SerializeField] private PlayableDirector PlayableDirector;
    [SerializeField] private TimelineAsset[] TimelineAssets;
    private AudioSource audioSource;
    private bool isOpened = false;

    private float timeLimit = 0;
    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeLimit -= Time.unscaledDeltaTime;
        ChoiceTitle();
        if (!isOpened)
        {
            MoveSelectImage();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PlayableDirector.Play(TimelineAssets[2]);
                isOpened = false;
            }
        }
    }

    private void MoveSelectImage()
    {
        RectTransform rect = selectImage.GetComponent<RectTransform>();
        if (Input.GetKeyDown(KeyCode.DownArrow) && selectNumber < 2 && timeLimit < 0)
        {
            audioSource.Play();
            timeLimit = 0.22f;
            selectNumber++;
            rect.DOAnchorPosY(rect.anchoredPosition.y - 5f, 0.2f); // UI 상에서 Y 간격   
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && selectNumber > 0 && timeLimit < 0)
        {
            audioSource.Play();
            timeLimit = 0.22f;
            selectNumber--;
            rect.DOAnchorPosY(rect.anchoredPosition.y + 5f, 0.2f);
        }

    }

    public void GoSelectScene()
    {
        SceneManager.LoadScene(1);
    }

    private void ChoiceTitle()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (selectNumber)
            {
                case 0:

                    PlayableDirector.Play(TimelineAssets[3]);
                    Debug.Log("씬 이동됌");
                    Invoke("GoSelectScene", 10f);
                    break;
                case 1:
                    PlayableDirector.Play(TimelineAssets[1]);
                    Debug.Log("타임라인 실행!");

                    isOpened = true;
                    break;
                case 2:
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
                    break;
            }

        }
    }
}
