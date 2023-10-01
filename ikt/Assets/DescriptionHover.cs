using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    private GameObject descriptionPanel;

    public void OnPointerEnter(PointerEventData eventData){
        descriptionPanel = this.transform.Find("UpgradeDescription").gameObject;

        descriptionPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData){
        descriptionPanel = this.transform.Find("UpgradeDescription").gameObject;

        descriptionPanel.SetActive(false);
    }
}
