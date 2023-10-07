using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerDescriptionHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    public GameObject descriptionPanelPrefab;
    private GameObject descriptionPanel;

    public void OnPointerEnter(PointerEventData eventData){
        descriptionPanel = (GameObject)Instantiate(descriptionPanelPrefab, transform.position - new Vector3(600, 228, 0), transform.rotation);
        descriptionPanel.transform.SetParent(CanvasManager.instance.transform, false);
    }

    public void OnPointerExit(PointerEventData eventData){
        Destroy(descriptionPanel);
    }
}
