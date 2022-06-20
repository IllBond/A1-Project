using BGGames.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMobile : GameSingleton<DebugMobile>
{
    [SerializeField] private Text mainText;

    public void SetText(string text) {
        mainText.text += text + " ";
    }


}
