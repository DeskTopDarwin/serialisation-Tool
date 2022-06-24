using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SaveLoad : EditorWindow
{
    static float spawnRange2D = 5;
    static float spawnRange3D = 5;

        
    // Start is called before the first frame update
    [MenuItem("Tool/SaveLoad")]
    public static void showWindow()
    {
        GetWindow(typeof(SaveLoad));
    }

    private void OnGUI()
    {
        GUILayout.Label("Save/Load/Create", EditorStyles.boldLabel);

        if (GUILayout.Button("Save"))
        {
            Save();
        }

        if (GUILayout.Button("Load"))
        {
            Load();
        }

        if (GUILayout.Button("CreateShape"))
        {
            CreateShapes();
        }
    }


    private void Save()
    {

    }

    private void Load() 
    {

    }

    private void CreateShapes()
    {
        int randomValue = Random.Range(0, 3);
        PrimitiveType primitiveType;

        switch (randomValue)
        {
            case 0:
                primitiveType = PrimitiveType.Cube;
                break;
            case 1:
                primitiveType= PrimitiveType.Cylinder;
                break;
            case 2:
                primitiveType = PrimitiveType.Sphere;
                break;
            default:
                primitiveType = PrimitiveType.Cube;
                break;
        }

        GameObject shape = GameObject.CreatePrimitive(primitiveType);
        
        shape.GetComponent<MeshRenderer>().material.color = new Color32((byte)Random.Range(0,255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        shape.transform.position = new Vector3(Random.Range(-spawnRange2D, spawnRange2D), Random.Range(1, spawnRange3D), Random.Range(-spawnRange2D, spawnRange2D));

    }

}
