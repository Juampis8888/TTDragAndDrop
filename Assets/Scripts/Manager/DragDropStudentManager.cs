using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DragDropStudentManager : MonoBehaviour
{
    public GameObject GameObjectMessage;

    public GameObject GameObjectValidate;

    public GameObject GameObjectBack;

    public GameObject GameObjectRestarting;

    public GameObject PanelWin;

    public GameObject PanelLose;

    public TextMeshProUGUI MessageText;

    public DragDropStudentAdapter StudentAdapter;

    public RectTransform Content;

    [SerializeField]
    private ConsultManager ConsultManager;

    [SerializeField]
    private List<DragDropStudentAdapter> StudentsDragDrop;

    [SerializeField]
    private List<StudentPanel> StudentsPanel;

    private void Awake()
    {
        ConsultManager = GameObject.FindGameObjectWithTag("ConsultManager").GetComponent<ConsultManager>();
    }

    void Start()
    {
        ShowStudentDragDrop();
    }

    public void ShowStudentDragDrop()
    {
        int Position = 0;
        var Students = ConsultManager.GetListStudent();
        if (Content.childCount < 1)
        {
            Students.datos.ForEach(student =>
            {
                var StudentRectTransform = StudentAdapter.GetComponent<RectTransform>();
                float templateHeight = StudentRectTransform.rect.height;
                var item = Instantiate(StudentAdapter);
                item.name = "Student " + (Position + 1).ToString();
                item.NameText.text = string.Format("{0} {1}", ((student.nombre == null) ? "" : student.nombre), ((student.apellido == null) ? "" : student.apellido));
                item.NoteText.text = string.Format("{0} {1}", "Nota:", student.nota == 0 ? "NoNota" : student.nota.ToString());
                item.Parent(Content);
                var TopY = ((templateHeight * Position) + (20 * (Position + 1))) * -1;
                item.Location(TopY);
                item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                StudentsDragDrop.Insert(Position, item);
                var Heigth = templateHeight * (Position + 1) + (20 * (Position + 1));
                Content.sizeDelta = new Vector2(Content.rect.width, Heigth);
                Position++;
            });
        }
    }

    public void MoveContent()
    {
        int Position = 0;
        int count = Content.childCount;
        if(count < 1)
        {
            GameObjectValidate.SetActive(true);
        }
        var StudentRectTransform = StudentAdapter.GetComponent<RectTransform>();
        float templateHeight = StudentRectTransform.rect.height;
        for (int i = 0; i < count; i++)
        {
            var TopY = ((templateHeight * i) + (20 * (i + 1))) * -1;
            Content.GetChild(i).transform.localPosition = new Vector3(0, TopY, 0);
            Position++;
        }
        var Heigth = templateHeight * (Position + 1) + (20 * (Position + 1));
        Content.sizeDelta = new Vector2(Content.rect.width, Heigth);
    }

    public void AddStudentPanel(DragDropStudentAdapter DDStudentAdapter, string NamePanel)
    {
        var studentPanel = new StudentPanel();
        if(NamePanel == "StudentsWin")
        {
            studentPanel.IsWin = true;
        }
        else if(NamePanel == "StudentsLose")
        {
            studentPanel.IsWin = false;
        }

        if (DDStudentAdapter.NoteText.text == "NoNota")
        {
            studentPanel.Note = 0;
        }
        else
        {
            studentPanel.Note = float.Parse(DDStudentAdapter.NoteText.text.Split(' ')[1]);
        }
        studentPanel.Name = DDStudentAdapter.NameText.text;
        if(!StudentsPanel.Exists(student => student.Name == studentPanel.Name))
        {
            StudentsPanel.Insert(StudentsPanel.Count, studentPanel);
        }
        else if(StudentsPanel.Exists(student => student.Name == studentPanel.Name))
        {
            if(studentPanel != StudentsPanel.Find(student => student.Name == studentPanel.Name))
            {
                var count = StudentsPanel.FindIndex(student => student.Name == studentPanel.Name);
                StudentsPanel.RemoveAt(count);
                StudentsPanel.Insert(count, studentPanel);
            }
        }  
    }

    public void Validate()
    {
        GameObjectBack.SetActive(false);
        GameObjectRestarting.SetActive(false);
        bool isError = false;
        StudentsPanel.ForEach(student => 
        {
            if(student.Note >= 3 & student.Note <= 5 & !student.IsWin)
            {;
                isError = true;

            }else if(student.Note >= 0 & student.Note < 3 & student.IsWin)
            {
                isError = true;
            }
        });

        GameObjectMessage.SetActive(true);
        if (!isError)
        {
            MessageText.text = "Todos los estudiantes estan en su respectivo cuadro";
            GameObjectBack.SetActive(true);
        }
        else
        {
            MessageText.text = "Se necesita correguir la posicion de los estudiantes, ya que se encuentran mal ubicados";
            GameObjectRestarting.SetActive(true);
        }
    }

    public void ResetAll()
    {
        GameObjectValidate.SetActive(false);
        GameObjectMessage.SetActive(false);
        Delete();
        ShowStudentDragDrop();
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    private void Delete()
    {
        int countPanel = StudentsPanel.Count;
        int countChild = Content.childCount;
        int countStudents = StudentsDragDrop.Count;
        int countPanelWin = PanelWin.transform.childCount;
        int countPanelLose = PanelLose.transform.childCount;

        for(int i = 0; i< countPanel; i++)
        {
            StudentsPanel.RemoveAt(0);
        }

        for(int i = 0; i<countStudents; i++)
        {
            StudentsDragDrop.RemoveAt(0);
        }

        for( int i = 0; i<countChild; i++)
        {
            DestroyImmediate(Content.GetChild(0).gameObject);
        }

        for(int i = 1; i<countPanelWin; i++)
        {
            DestroyImmediate(PanelWin.transform.GetChild(1).gameObject);
        }

        for (int i = 1; i < countPanelLose; i++)
        {
            DestroyImmediate(PanelLose.transform.GetChild(1).gameObject);
        }
    }
}
