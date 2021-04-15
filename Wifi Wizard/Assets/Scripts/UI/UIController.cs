using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(GameManager))] //Forces this gameobject to have a UI controller
public class UIController : MonoBehaviour {

    [HideInInspector] public GameObject canvas = null;
    GameManager gameManager = null;
    private GameObject pauseMenu;

    private TMP_Text tooltipText;
    private GameObject tooltip;

    private bool tooltipWasActive = false;


    void Start() {
        if(canvas == null){
            canvas = GameObject.Find("Canvas");

            if(canvas == null){ //Still cant find canvas. Prob doesnt exist or its in the wrong heigharchy location.
                Debug.LogError("No canvas found in the level to display UI onto. Add it to the level from the prefabs folder.");
            }else{
                pauseMenu = canvas.transform.Find("PauseMenu").gameObject; //Bad practice to search the entire level for a string but its a small project so idc. It wont be bad at this scale.
                if(pauseMenu == null) Debug.LogError("No pause menu found.");
            }
        }

        if(gameManager == null){
            gameManager = GetComponent<GameManager>();
        }

        tooltip = canvas.gameObject.transform.Find("Tooltip").gameObject;
        tooltipText = tooltip.transform.Find("Text").gameObject.GetComponent<TMP_Text>();
    }


    void Update() {
        
    }

    public void UpdateInventory(List<AccessPoint> contents){
        if(canvas != null){
            
            Transform inventorySlotsUI = canvas.transform.Find("Inventory").Find("SlotsLayout");
            Transform[] slots = inventorySlotsUI.GetComponentsInChildren<Transform>();

            int n = 0; //This is a hardcoded prototype only way of doing this. Ill change it later
            foreach(Transform t in slots){
                if(t.CompareTag("InventorySlot")){
                    Transform ui = t.Find("Icon");
                    ui.Find("Name").gameObject.GetComponent<TMP_Text>().text = contents[n].displayName;
                    ui.Find("AmountLeft").gameObject.GetComponent<TMP_Text>().text = contents[n].amount.ToString();
                    n++; //This is a hardcoded prototype only way of doing this. Ill change it later
                }
            }

        }
    }

/// <summary>
/// //Lock cursor, set timescale, bring up pause menu, set gamemanager to pause game
/// </summary>
/// <param name="isPaused"></param>
    public void PauseGame(bool isPaused){

        //Lock cursor, set timescale, bring up pause menu, set gamemanager to pause game
        Cursor.lockState = (isPaused) ? CursorLockMode.Confined : CursorLockMode.Locked;
        Time.timeScale = (isPaused) ? 0 : 1;
        pauseMenu.SetActive(isPaused);
        GameManager.GamePaused = isPaused;

        if(tooltip.activeSelf && isPaused){
            tooltip.SetActive(false);
            tooltipWasActive = true;
        }else if(tooltipWasActive && !isPaused){
            tooltip.SetActive(true);
            tooltipWasActive = false;
        }
        
        
    }

    public void ShowTooltip(string tooltip){
        tooltipText.SetText(tooltip);
        this.tooltip.SetActive(tooltip.Trim() != "");
    }



}
