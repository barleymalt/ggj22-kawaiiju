using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KawaiijuManager : MonoBehaviour
{
    [SerializeField] private Transform m_Cursor;

    private Camera _camera;
    private NavMeshAgent _navAgent;

    private void Awake()
    {
        _camera = Camera.main;
        _navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                _navAgent.SetDestination(hit.point);
            }
        }


        // NavMesh.SamplePosition()
        // m_Cursor.position = worldPosition;
    }
}