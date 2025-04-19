using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using Coffee.UIExtensions;


public class InGame : MonoBehaviour
{

    List<TextMetaData> textData = new List<TextMetaData>();
    public Timer timer;
    public Text titleIndexText;
    public Text titleText;
    public GameObject nextButton;
    public UIParticle finishParticle;


    public Image bg;

    int questionIndex = 0;








    void Start()
    {
        Init();

        timer = GetComponent<Timer>();
        timer.Init();

        Application.targetFrameRate = 60;
    }

    void Init()
    {
        LoadIntroLocalizationData();
        titleText.text = "";
        titleIndexText.text = "";
        finishParticle.gameObject.SetActive(false);
    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // 좌 화살표 키를 누르면 이전 질문으로 이동
            ChangeQuestion(Mathf.Max(1, questionIndex - 1));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 우 화살표 키를 누르면 다음 질문으로 이동
            ChangeQuestion(Mathf.Min(textData.Count, questionIndex + 1));
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            // 엔터 키를 누르면 타이머 시작
            // OnClickTimer(30f); // 예: 30초 타이머 시작
        }
    }




    public void OnClickNextQuestion()
    {
        ChangeQuestion(Mathf.Min(textData.Count, questionIndex + 1));
    }

    public void OnClickTimer(float time)
    {
        if (time == 0)
            timer.SetTimer(time, null);
        else
            timer.AddTimer(time, null);
    }


    public void OnClickMoveQuestion(InputField inputField)
    {
        if (inputField.text == "멍멍개" || inputField.text == "mmg" || inputField.text == "MMG")
        {
            EsterEgg.instance.ActionEsterEgg();
            inputField.text = "";
            return;
        }
        if (inputField.text == "327")
        {
            SoundManager.Instance.PlaySound(SoundType.yahaha);
            LoadIntroLocalizationData();
            inputField.text = "";
            return;
        }

        int index;
        if (int.TryParse(inputField.text, out index))
        {
            if (index <= 0) index = 1;
            if (index >= textData.Count) index = textData.Count - 1;

            ChangeQuestion(index);
            inputField.text = "";
        }
    }


    void ChangeQuestion(int nextQuestionIndex)
    {
        if (EsterEgg.instance.IsEsterEggAniAction)
            return;


        questionIndex = nextQuestionIndex;

        if (questionIndex < textData.Count)
        {
            var targetData = this.textData.Find(x => x.No.Equals(questionIndex.ToString()));

            titleText.transform.DOKill();
            titleIndexText.transform.DOKill();
            titleText.transform.DOScale(1f, 0.4f).From(1.5f).SetEase(Ease.InOutBack);
            titleIndexText.transform.DOScale(1f, 0.4f).From(0.5f).SetEase(Ease.OutBack);

            titleText.text = RegexReplace(targetData.Title);
            titleIndexText.text = $"No.{questionIndex}";
            ChangeImage(targetData.Type);

            finishParticle.gameObject.SetActive(targetData.Type == QType.F);
            if (targetData.Type == QType.F)
                finishParticle.Play();


            if (targetData.Type >= QType.F && targetData.Type <= QType.G)
            {
                titleText.text = "";
                titleIndexText.text = "";
                timer.ResetTimer();
                timer.ShowTimer(false);
                nextButton.SetActive(false);
            }
            else
            {
                timer.SetTimer(targetData.Time, null);
                timer.ShowTimer(true);
                nextButton.SetActive(true);

                if (targetData.Type == QType.H)
                    EsterEgg.instance.ActionEsterEgg(true);
                else
                    SoundManager.Instance.PlaySound(SoundType.NextQuestion);

            }
        }
        else
        {
            titleText.text = "";
            titleIndexText.text = "";
        }
        // timer.ResetTimer();
    }

    string RegexReplace(string input)
    {
        string output = input.Replace("\\n", "\n");
        output = output.Replace("comma", ",");

        return output;
    }







    public void ChangeImage(QType type)
    {
        var image = ResourceManager.Instance.GetImage(type);
        if (image == null)
        {
            bg.sprite = null;
            return;
        }

        bg.sprite = image;
    }






    public void LoadIntroLocalizationData()
    {
        TextAsset ta = ResourceManager.Instance.GetStringData();
        if (ta)
        {
            List<Dictionary<string, object>> csv = CSVReader.Read(ta);
            textData = TextMetaData.Create(csv);
        }
    }
}
