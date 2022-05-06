using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StudentAdapter : MonoBehaviour
{
    public TextMeshProUGUI IdText;

    public TextMeshProUGUI NameText;

    public TextMeshProUGUI MailText;

    public TextMeshProUGUI AgeOldText;

    public TextMeshProUGUI NoteText;

    public Image Image;

    public Toggle ApprovedToggle;

    public GameObject GameObjectMessage;

    public void Parent(Transform Parent)
    {
        transform.SetParent(Parent);
    }
    public void Location(float TopY)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, TopY, 0);
    }

    public void ChangeColor(Color color)
    {
        Image.color = color;
    }
}
