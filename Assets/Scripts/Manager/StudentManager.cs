using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StudentManager : MonoBehaviour
{   
    public GameObject GameObjectMessage;

    public StudentAdapter StudentAdapter;

    public RectTransform Content;

    public List<Color> Colors;

    private ConsultManager ConsultManager;

    [SerializeField]
    private List<StudentAdapter> StudentsAdapter;

    private void Awake()
    {
        ConsultManager = GameObject.FindGameObjectWithTag("ConsultManager").GetComponent<ConsultManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowStudent();
    }

    // Update is called once per frame
    void Update()
    {
        if (ConsultManager.GetIsChange())
        {
            RemoveChilds();
            ShowStudent();
            ConsultManager.SetIsChange(false);
        }
    }

    public void ShowStudent()
    {
        int Position = 0;
        var Students = ConsultManager.GetListStudent();
        Debug.Log(Content.childCount);
        if (Content.childCount < 1)
        {
            Students.datos.ForEach(student =>
            {
                var StudentRectTransform = StudentAdapter.GetComponent<RectTransform>();
                float templateHeight = StudentRectTransform.rect.height;
                var item = Instantiate(StudentAdapter);
                item.name = "Student " + (Position + 1).ToString();
                item.NameText.text = string.Format("{0} {1}", ((student.nombre == null) ? "" : student.nombre), ((student.apellido == null) ? "" : student.apellido));
                item.IdText.text = student.Id == 0 ? "NoId" : student.Id.ToString();
                item.MailText.text = student.email == null ? "" : student.email;
                item.AgeOldText.text = student.edad == 0 ? "" : student.edad.ToString();
                item.NoteText.text = student.nota == 0 ? "NoNota" : student.nota.ToString();
                item.ApprovedToggle.isOn = false;
                var pos = Position % 2 == 0 ? 0 : 1;
                item.ChangeColor(Colors[pos]);
                item.Parent(Content);
                var TopY = (templateHeight * Position) * -1;
                item.Location(TopY);
                item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                StudentsAdapter.Insert(Position, item);
                var Heigth = templateHeight * (Position + 1);
                Content.sizeDelta = new Vector2(Content.rect.width, Heigth);
                Position++;
            });
        }
    }

    public void validate()
    {
        var Students = ConsultManager.GetListStudent();
        bool IsError = false;
        float Note = 0;
        StudentsAdapter.ForEach(student => 
        { 
            if(student.NoteText.text == "NoNota")
            {
                Note = 0;
            }
            else
            {
                Note = float.Parse(student.NoteText.text);
            }

            if (Note >= 3f & Note <= 5 & !student.ApprovedToggle.isOn)
            {
                student.GameObjectMessage.SetActive(true);
                IsError= true;
            }
            else if (Note >= 3f & Note <= 5 & student.ApprovedToggle.isOn)
            {
                student.GameObjectMessage.SetActive(false);

            }
            else if (Note >= 0f & Note < 3 & student.ApprovedToggle.isOn)
            {
                student.GameObjectMessage.SetActive(true);
                IsError = true;
            }
            else if (Note >= 0f & Note < 3 & !student.ApprovedToggle.isOn)
            {
                student.GameObjectMessage.SetActive(false);
            }
        });
        if (!IsError)
        {
            GameObjectMessage.SetActive(true);
        }
        else
        {
            Debug.Log("Revise los check");
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void RemoveChilds()
    {
        int countChild = Content.childCount;
        for(int i = 0; i < countChild; i++)
        {
            DestroyImmediate(Content.GetChild(0).gameObject);
        }
    }
}
