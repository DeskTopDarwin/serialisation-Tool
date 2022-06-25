using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

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
        
        string json = "";
        var shapes = FindObjectsOfType<Rigidbody>();
        ShapeList listToJson = new ShapeList();

        foreach (var shape in shapes)
        {
            Shape toJson = new Shape();
            toJson.primitiveType = shape.name;
            toJson.position = shape.position;
            toJson.rotation = shape.rotation;
            toJson.color = shape.GetComponent<MeshRenderer>().material.color;

            listToJson.shapes.Add(toJson);
        }

        json += JsonUtility.ToJson(listToJson);
        File.WriteAllText(Application.dataPath + "/saveFile.json", json);
    }

    private void Load() 
    {
        string jsonstring = File.ReadAllText(Application.dataPath + "/saveFile.json");
        ShapeList listOfItems = JsonUtility.FromJson<ShapeList>(jsonstring);
        //Debug.Log(listOfItems.shapes.Count);

        //destroy current on map
        List<Rigidbody> temp = new List<Rigidbody>();
        temp = FindObjectsOfType<Rigidbody>().ToList();
        foreach (var item in temp)
        {
            DestroyImmediate(item.gameObject);
        }
        temp.Clear();

        //create from save file
        foreach (var item in listOfItems.shapes)
        {
            PrimitiveType primitive;

            switch (item.primitiveType)
            {
                case "Cube":
                    primitive = PrimitiveType.Cube;
                    break;
                case "Cylinder":
                    primitive = PrimitiveType.Cylinder;
                    break;
                case "Shere":
                    primitive= PrimitiveType.Sphere;
                    break;
                default:
                    primitive = PrimitiveType.Cube;
                    break;
            }

            GameObject shape = GameObject.CreatePrimitive(primitive);
            shape.name = item.primitiveType;
            shape.transform.position = item.position;
            shape.transform.rotation = item.rotation;
            shape.GetComponent<MeshRenderer>().material.color = item.color;
            shape.AddComponent<Rigidbody>();
        }
    }

    private void CreateShapes()
    {
        int randomValue = Random.Range(0, 3);
        PrimitiveType primitiveType;
        string shapeName;

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

        switch (randomValue)
        {
            case 0:
                shapeName = "Cube";
                break;
            case 1:
                shapeName = "Cylinder";
                break;
            case 2:
                shapeName = "Sphere";
                break;

            default:
                shapeName = "Cube";
                break;
        }

        GameObject shape = GameObject.CreatePrimitive(primitiveType);
        shape.name = shapeName;
        shape.transform.position = new Vector3(Random.Range(-spawnRange2D, spawnRange2D), Random.Range(1, spawnRange3D), Random.Range(-spawnRange2D, spawnRange2D));
        shape.GetComponent<MeshRenderer>().material.color = new Color32((byte)Random.Range(0,255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        shape.AddComponent<Rigidbody>();

    }
    
    [System.Serializable]
    public class Shape
    {
        public string primitiveType;
        public Vector3 position;
        public Quaternion rotation;
        public Color32 color;
    }

    [System.Serializable]
    public class ShapeList
    {
        public List<Shape> shapes = new List<Shape>();
    }

}
