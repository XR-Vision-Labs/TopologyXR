using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using Datacenter;

public class SharedRack : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rackName;
    [SerializeField] private Image saveIcon;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button clearButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private Color grayColor;
    [SerializeField] private Color greenColor;

    //private SaveToJson saveToJson;
    //private SaveToCloud cloudSave;
    public TextMeshProUGUI debugRack;

    public bool issavedlocally {
         set
        {
            if(saveIcon != null)
            {
                saveIcon.color = value ? greenColor : grayColor;
            }
        } 
    }
    
    public void log(string value) {
         
            if(debugRack != null)
            {
                debugRack.text += value;
            }
         
    }

    #region UnityFunctions
    //private void Start()
    //{
    //    rackName.text = gameObject.name;

    //    saveButton.onClick.AddListener(CloudSave);
    //    clearButton.onClick.AddListener(ClearRack);
    //    deleteButton.onClick.AddListener(CloudDelete);
    //}

    //private void OnDisable()
    //{
    //    saveButton.onClick.RemoveListener(CloudSave);
    //    clearButton.onClick.RemoveListener(ClearRack);
    //    deleteButton.onClick.RemoveListener(CloudDelete);
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        CloudSave();
    //    }

    //    if (Input.GetKeyDown(KeyCode.C))
    //    {
    //        ClearRack();
    //    }
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        CloudDelete();
    //    }
        

    //}

    #endregion


    #region Local_File_Mgt
    //public void Save()
    //{
    //    Rack rack = new Rack()
    //    {
    //        name = transform.name,
    //        transformData = new TransformData()
    //        {
    //            position = transform.position,
    //            rotation = transform.rotation,
    //        }
    //    };

    //    if (saveToJson!=null)
    //    {
    //        issavedlocally =saveToJson.Save(rack);
    //    }
    //}



    public void ClearRack()
    {
        Destroy(this.gameObject);
    }

    //public void DeleteRack()
    //{
    //    if (saveToJson != null)
    //    {
    //        saveToJson.ClearRacks(this.gameObject);
    //    }
    //}
    #endregion


    //#region Cloud_Mgt

    //private void CloudSave()
    //{
    //    Vector3 position =transform.position;
    //    Quaternion rotation =transform.rotation;

    //    RackVector3 pos = new RackVector3(position.x, position.y, position.z);
    //    RackQuaternion rot = new RackQuaternion(rotation.x, rotation.y, rotation.z, rotation.w);

    //    Rack rack = new Rack("DC01", gameObject.name, pos, rot, 1000, "2U", "Mounted", 10, 10);

    //    Debug.Log("Saving rack to cloud " + gameObject.name);
    //    SaveToCloud.Instance.SaveRack(gameObject.name, rack);

    //    Debug.Log("Rack Saved");
    //}

    //private void CloudDelete()
    //{
    //    SaveToCloud.Instance.DeleteRack(gameObject.name);
    //}


    //#endregion

}
