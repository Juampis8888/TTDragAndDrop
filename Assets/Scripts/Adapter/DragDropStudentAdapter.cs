using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DragDropStudentAdapter : MonoBehaviour
{
    public TextMeshProUGUI NameText;

    public TextMeshProUGUI NoteText;

    public Image Image;

    public void Parent(Transform Parent)
    {
        transform.SetParent(Parent);
    }
    public void Location(float TopY)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, TopY, 0);
    }
}
