using System.Collections;
using UnityEngine;

public class CopyLink : MonoBehaviour
{

    [SerializeField] private GameObject _text;

    void CopyToClipboard(string s)
    {
        TextEditor te = new TextEditor();
        te.text = s;
        te.SelectAll();
        te.Copy();
        StartCoroutine(ShowText());
    }


 
    public void Copy()
    {
        CopyToClipboard("Присоединяйся к нам - https://a1-security.com");
    }


    IEnumerator ShowText() {
        _text.SetActive(true);
        yield return new WaitForSeconds(3);
        _text.SetActive(false);
        yield break;
    }
}
