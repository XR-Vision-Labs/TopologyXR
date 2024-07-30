using UnityEngine;
using UnityEngine.Android;

public class permission : MonoBehaviour
{
    void Start()
    {
        // Request read and write permissions at runtime
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead) ||
            !Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
    }
}
