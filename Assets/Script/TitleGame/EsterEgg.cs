using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EsterEgg : MonoBehaviour
{
    public static EsterEgg instance;
    public Image movingImage;


    bool isClick = false;
    int clickCount = 0;
    readonly int actionCount = 5;
    readonly float duration = 1f;
    float tempTime = 0f;

    int imageIndex = 0;
    bool isMove = false;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    void Start()
    {
        movingImage.gameObject.SetActive(false);
    }


    public void OnClickEsterButton()
    {
        if (isMove) return;

        isClick = true;
        SoundManager.Instance.PlaySound(SoundType.eggSound);
        tempTime = 0f;
        clickCount++;

        if (clickCount >= actionCount)
            ActionEsterEgg();
    }

    public void ActionEsterEgg(bool showHanel = false)
    {
        isClick = false;
        tempTime = 0;
        clickCount = 0;

        movingImage.gameObject.SetActive(true);
        isMove = true;

        SoundManager.Instance.PlaySound(SoundType.marioGalaxy);
        imageIndex = Random.Range(0, showHanel ? ResourceManager.Instance.HanelImageResources.Count : ResourceManager.Instance.MMGImageResources.Count);
        movingImage.sprite = showHanel ? ResourceManager.Instance.GetHanelImage(imageIndex) : ResourceManager.Instance.GetMMGImage(imageIndex);
        movingImage.SetNativeSize();

        int type = Random.Range(0, 2);
        if (type == 0 && !showHanel)
        {
            movingImage.transform.localPosition = new Vector2(1550f, 0f);
            movingImage.transform.DOLocalMoveX(-1550f, 5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                movingImage.gameObject.SetActive(false);
                isMove = false;
            });
        }
        else
        {
            movingImage.transform.localPosition = new Vector2(0f, -1000f);
            movingImage.transform.DOLocalMoveY(1000f, 5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                movingImage.gameObject.SetActive(false);
                isMove = false;
            });
        }
    }


    void Update()
    {
        if (isClick)
        {
            tempTime += Time.deltaTime;
            if (tempTime >= duration)
            {
                isClick = false;
                tempTime = 0;
                clickCount = 0;
            }
        }
    }

}
