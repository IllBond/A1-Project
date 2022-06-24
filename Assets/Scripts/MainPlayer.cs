using BGGames.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainPlayer : GameSingleton<MainPlayer>
{
    [SerializeField] private RectTransform MessageWindow;
    [SerializeField] private RectTransform MessageWindowNewPos;

    [SerializeField] private Vector3 UpPos;
    [SerializeField] private Vector3 DownPos;


    [SerializeField] private Text messageText;
    [SerializeField] private Text MoneyValue;
    [SerializeField] private Text ExpValue;

    [SerializeField] private bool isDown;

    private Coroutine oldCoroutine;


    public float money = 100;
    public float Money
    {
        get => money;
        set
        {
            if ((money + value) <= 0)
            {
                money = 0;
                MoneyValue.text = "" + money;
                return;
            }
            money += value;
            MoneyValue.text = "" + money;
        }
    }

    public void AddMoney()
    {
        Money = 50;
        Debug.Log(Money);
    }

    public float raiting = 500;
    public float Raiting
    {
        get => raiting;
        set
        {
            if ((raiting + value) <= 0)
            {
                raiting = 0;
                ExpValue.text = "" + raiting;
                return;
            }
            raiting += value;
            if (raiting >= 10000)
            {
                AchivemntController.Instance.AchGameScore();
            }
            ExpValue.text = "" + raiting;
        }
    }

    public override void Update()
    {
        base.Update();
        if (isDown)
        {
            MessageWindow.position = Vector3.Lerp(MessageWindow.position, DownPos, Time.unscaledDeltaTime * 5);
        }
        else
        {
            MessageWindow.position = Vector3.Lerp(MessageWindow.position, UpPos, Time.unscaledDeltaTime * 5);
        }
    }

    private string _message = "";
    public string Message
    {
        get => _message;
        set
        {
            _message = value;
        }
    }

    public override void Awake()
    {
        base.Awake();
        UpPos = MessageWindow.position;
        DownPos = MessageWindowNewPos.position;
    }

    public override void Start()
    {
        base.Start();
        ExpValue.text = "" + raiting;
        MoneyValue.text = "" + money;
    }

    public void ShowMessage(string text)
    {
        if (oldCoroutine != null)
        {
            StopCoroutine(oldCoroutine);
        }

        oldCoroutine = StartCoroutine(SetMessage(text));
    }

    IEnumerator SetMessage(string text)
    {
        MessageWindow.position = UpPos;
        isDown = true;
        Message = text;
        messageText.text = "" + Message;
        yield return new WaitForSecondsRealtime(2);

        isDown = false;
    }
}
