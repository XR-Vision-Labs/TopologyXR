using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ServerSnapBehaviour : MonoBehaviour
{
    public List<Transform> snaps;
    public List<bool> snapAvailability;

    // true for up and false for down
    public List<int> snapPos;

    private Transform parent;
    private Transform snap;
    private int index=-1;

    //private void OnEnable()
    //{
    //    snapAvailability = new List<bool>(34);
    //    snapPos = new List<int>(34);
    //}

    private void Start()
    {
        Debug.Log("Size of snaps is " + snaps.Count);

        snapAvailability = new List<bool>();
        snapPos = new List<int>();

        for (int i = 0; i < snaps.Count; i++)
        {
            snapAvailability.Add(true); // Adds a new element to the list
            snapPos.Add(0); // Adds a new element to the list
        }
    }


    private bool CheckUp(int index, int size)
    {
        for (int i = 1; i < size; i++)
        {
            ++index;
            if (snapAvailability[index])
            {
                SampleController.Instance.Log($"place is empty at {index} so continuing");
                continue;
            }
            else
            {
                SampleController.Instance.Log($"Place is not empty at {index} so downresult is set to false");
                //upResult = false;

                return false;

            }
        }

        return true;
    }

    private bool CheckDown(int index, int size)
    {
        for (int i = 1; i < size; i++)
        {
            --index;

            if (snapAvailability[index])
            {
                SampleController.Instance.Log($"place is empty at {index} so continuing");
                continue;
            }
            else
            {
                SampleController.Instance.Log($"Place is not empty at {index} so downresult is set to false");
                //downResult = false;

                return false;
            }
        }

        return true;
    }

    private bool CheckPlace(int index)
    {

        if (snaps[index])
        {
            SampleController.Instance.Log($"place is empty at hover place");
            return true;
        }
        return false;
    }

    public int CheckAvailability(Transform snap, int s)
    {        
        

        parent = snap.parent;
        Debug.Log("Parent is " + parent);

        this.snap = snap;


        int size = s;
        Debug.Log("Size is " + size);

        index = GetIndex(parent);
        Debug.Log("Index is " + index);


        // check once on hovered place

        if (!CheckPlace(index))
        {
            SampleController.Instance.Log("Hover Place is not empty so returning");
            return -1;
        }


        // for loop for down
        if(CheckDown(index, size))
        {
            SampleController.Instance.Log("Down place is empty so returning 2");
            return 2;
        }
        else if(CheckUp(index, size))
        {
            SampleController.Instance.Log("Up place is empty so returning 1");
            return 1;
        }
        else
        {
            SampleController.Instance.Log("No place is empty so returning -1");
            return -1;
        }
        
        


        //int temp = index;

        

        //temp = index;
        //if (!downResult)
        //{
        //    //for loop for up

        //    Debug.Log($"Down place is not empty so checking up");

            
        //}

        //else
        //{
        //    Debug.Log("Down place is empty so returning 2");
        //    return 2;
        //}

        //if (upResult)
        //{
        //    Debug.Log("Upplace is empty so returning 1");

        //    return 1;
        //}

    }

    public int GetIndex(Transform snapParent)
    {
        if (snaps.Count > 0)
        {
            Debug.Log("Searching from the list");
            return snaps.IndexOf(snapParent);
        }

        return -1;
    }

    
    public int CheckOccupy(int index,int size)
    {
        // check wheter upper place is alloted or down
        // checking down

        Debug.Log("Finding the occupied place");
        Debug.Log("Index is " + index + "Size is " + size);

        if (snapPos[index + 1] == index)
        {
            Debug.Log("Upper part matched so returning 1");
            return 1;
        }else if (snapPos[index-1]== index)
        {
            Debug.Log("Down part matched so returning 2");
            return 2;
        }
        else
        {
            Debug.Log("No part matched so returning -1");
            return -1;
        }

        // checking up

    }


    public void RemoveFromList(int result, int index, int size)
    {
        // get the index make available at index
        // search for all the places occupied by index and then make them avaialba

        if (result == 1)
        {
            // for up
            for (int i = 0; i < size; i++)
            {
                Debug.Log("Configuring at index "+(index+1));
                snapAvailability[index + i] = true;

                EnableSnaps(snaps[index + 1]);
                snapPos[index + i] = 0;
            }
        }
        
        else if (result == 2)
        {
            for(int i= 0; i < size; i++)
            {

                Debug.Log("Configuring at index " + (index + 1));
                snapAvailability[index - i] = true;

                EnableSnaps(snaps[index - 1]);
                snapPos[index - i] = 0;
            }
        }

        else if (result == 3)
        {
            snapAvailability[index] = true;
        }

        // remove index from pos list too
    }

    public void DisableSnaps(Transform snap)
    {
        //var child = snap.GetChild(0).GetComponent<CustomSnapInteractable>();
        //child.enabled = false;
        BoxCollider[] colliders = snap.GetComponents<BoxCollider>();
        foreach(BoxCollider collider in colliders)
        {
            collider.enabled = false;
        }

    }

    public void EnableSnaps(Transform snap)
    {
        //var child = snap.GetChild(0).GetComponent<CustomSnapInteractable>();
        //child.enabled = true;
        BoxCollider[] colliders = snap.GetComponents<BoxCollider>();
        foreach (BoxCollider collider in colliders)
        {
            collider.enabled = true;
        }

    }


    public void AddToList(int result, int index, int size=1)
    {
        Debug.Log("Index is " + index);
        Debug.Log("Size is " + size);
        switch (result)
        {
            case 1:
                snapAvailability[index] = false;
                Debug.Log("Executing up");
                Debug.Log("Index is " + index);

                int val = index;
                for (int i=1; i<size; i++)
                {

                    

                    Debug.Log("Upper index is " + (val));
                    snapAvailability[++val] = false;

                    snapPos[val] = index;

                    //Debug.Log("Disaling snap interaction on other snaps");
                    //snaps[val].GetChild(0).GetComponent<CustomSnapInteractable>().enabled = false;

                    DisableSnaps(snaps[val]);

                    Debug.Log("up was available so mark this and up unavailable");
                }

                break;

            case 2:

                Debug.Log("Executing down");
                snapAvailability[index] = false;
                int val2 = index;
                for (int i=1; i<size; i++)
                {
                    
                    Debug.Log(val2);
                    snapAvailability[--val2] = false;

                    snapPos[val2] = index;
                    Debug.Log("down was available so mark this and down unavailable");

                    //Debug.Log("Disaling snap interaction on other snaps");
                    //snaps[val2].GetChild(0).GetComponent<CustomSnapInteractable>().enabled = false;

                    DisableSnaps(snaps[val2]);
                }

               

                break;

            case 3:
                Debug.Log("its 1u so Making unavailable");
                snapAvailability[index] = false;
                break;

            case -1:
                Debug.Log("Place not available");
                break;
        }
    }


}
