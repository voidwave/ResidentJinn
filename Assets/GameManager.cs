using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
public class GameManager : MonoBehaviour
{
    private Camera MainCam;
    public GameObject Jinn, Human, Wolf;
    private NavMeshAgent jinnNav;
    // Start is called before the first frame update
    void Start()
    {
        MainCam = GetComponent<Camera>();
        jinnNav = Jinn.GetComponent<NavMeshAgent>();
    }

    RaycastHit mouseHit;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.currentSelectedGameObject==null)
                if (Physics.Raycast(MainCam.ScreenPointToRay(Input.mousePosition), out mouseHit, 100))
                    jinnNav.SetDestination(mouseHit.point);
        }
    }
}
