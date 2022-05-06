using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerStudents : MonoBehaviour, IDropHandler
{   
    private DragDropStudentManager dragDropStudentManager;

    private void Awake()
    {
        dragDropStudentManager = GameObject.FindGameObjectWithTag("Students").GetComponent<DragDropStudentManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.SetParent(transform);
            dragDropStudentManager.MoveContent();
            var Adapter = eventData.pointerDrag.GetComponent<DragDropStudentAdapter>();
            dragDropStudentManager.AddStudentPanel(Adapter, transform.gameObject.name);
        }        
    }
}
