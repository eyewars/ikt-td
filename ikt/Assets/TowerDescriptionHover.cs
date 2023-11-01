using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TowerDescriptionHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    public GameObject descriptionPanelPrefab;
    private GameObject descriptionPanel;
    private int towerIndex;

    void Start(){
        towerIndex = gameObject.GetComponent<TowerSelect>().towerIndex;
    }

    public void OnPointerEnter(PointerEventData eventData){
        descriptionPanel = (GameObject)Instantiate(descriptionPanelPrefab, transform.position - new Vector3(600, 228, 0), transform.rotation);
        descriptionPanel.transform.SetParent(CanvasManager.instance.transform, false);

        descriptionPanel.GetComponentInChildren<TextMeshProUGUI>(true).text = StatTracker.instance.getDescription(towerIndex);
    }

    public void OnPointerExit(PointerEventData eventData){
        Destroy(descriptionPanel);
    }
}
