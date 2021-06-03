using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] GameObject treasure_notification = null;
    [SerializeField] TMP_Text treasure_notification_text = null;
    ToolBag toolBag = null;
    TreasureBag treasureBag = null;


    private void Start()
    {
        toolBag = FindObjectOfType<ToolBag>();
        treasureBag = FindObjectOfType<TreasureBag>();
        treasure_notification.SetActive(false);
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

    IEnumerator DisplayTreasureType(Treasure treasure)
    {
        treasure_notification_text.text = "Treasure is " + treasure.GetTreasureType().ToString();
        treasure_notification.SetActive(true);
        yield return new WaitForSeconds(2f);
        treasure_notification.gameObject.SetActive(false);
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
        StartCoroutine(DisplayTreasureType(treasure));

    }

    private void ProcessMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
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
