using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    ToolBag toolBag = null;
    TreasureBag treasureBag = null;

    private void Start()
    {
        toolBag = FindObjectOfType<ToolBag>();
        treasureBag = FindObjectOfType<TreasureBag>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ProcessMouseDown();
        }
        if (Input.GetMouseButtonDown(1))
        {
            ProcessTest();
        }
    }

    private void ProcessTest()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            return;
        }
        if (!hit.collider.TryGetComponent<Treasure>(out Treasure treasure))
        {
            return;
        }
        if (!toolBag.UseTest())
        {
            return;
        }
        Debug.Log(treasure.GetTreasureType());

    }

    private void ProcessMouseDown()
    {
        Debug.Log("Mouse down");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.Log("Nothing hit");
            return;
        }
        if (hit.collider.TryGetComponent<MinableBrick>(out MinableBrick brick))
        {
            brick.DecreaseToughness();
            toolBag.UsePick();
            return;
        }
        else if (hit.collider.TryGetComponent<Treasure>(out Treasure treasure))
        {
            treasureBag.AddMoney(treasure.GetTreasureValue());
            treasure.TakeTreaure();
        }
    }
}
