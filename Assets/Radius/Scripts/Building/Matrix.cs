using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Matrix : MonoBehaviour
{

    public static Matrix Instance { set; get; }

    public List<GameObject> BuildsPrefabs;      //Будівлі
    public List<GameObject> LandscapePrefab;
    public List<GameObject> ClearBuildsPrefabs;

    public int selectionX = -1;
    public int selectionY = -1;

    private const float TITLE_SIZE = 1f;
    private const float TITLE_OFFSET = 0.5f;

    public GameObject go;
    public int SizeMatrix = 96;
    public int[,] Board = new int[96, 96];

    public bool B1 = false, B2 = false, tree = false, dobro = false;
    void Start()
    {
        Instance = this;
        SpawnAll();
    }

    public Text text;
    // Update is called once per frame
    void Update()
    {
        //DrawBoard();

        UpdateSelection();
        if (dobro)
            ClickMatrix();
        else
            Dest();
        if (Input.GetMouseButtonDown(0) && B1 == true)
        {
            if (!Camera.main)
                return;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100.0f))
            {
                //Debug.Log(selectionX + "||" + selectionY);
                selectionX = (int)hit.point.x;
                selectionY = (int)hit.point.z;

                if (selectionX >= 0 && selectionX < SizeMatrix - 1 && selectionY >= 0 && selectionY < SizeMatrix - 2 && hit.collider.name == "Plane" && hit.collider.tag != "GameController" && hit.collider.tag != "Canvas")
                    go = Instantiate(BuildsPrefabs[0], GetTitleCenter(selectionX, selectionY), transform.rotation) as GameObject;
            }
            else
            {
                selectionX = -1;
                selectionY = -1;
            }
            B1 = false;

        }
        if (Input.GetMouseButtonDown(0) && B2 == true)
        {
            if (!Camera.main)
                return;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100.0f))
            {
                //Debug.Log(selectionX + "||" + selectionY);
                selectionX = (int)hit.point.x;
                selectionY = (int)hit.point.z;
            }
            else
            {
                selectionX = -1;
                selectionY = -1;
            }
            if (selectionX >= 0 && selectionX < SizeMatrix - 1 && selectionY >= 0 && selectionY < SizeMatrix - 1 && hit.collider.name == "Plane" && hit.collider.tag != "GameController" && hit.collider.tag != "Canvas")
                go = Instantiate(BuildsPrefabs[1], GetTitleCenter(selectionX, selectionY), transform.rotation) as GameObject;
            B2 = false;
        }
        if (Input.GetMouseButtonDown(0) && tree == true)
        {
            if (!Camera.main)
                return;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100.0f))
            {
                //Debug.Log(selectionX + "||" + selectionY);
                selectionX = (int)hit.point.x;
                selectionY = (int)hit.point.z;
            }
            else
            {
                selectionX = -1;
                selectionY = -1;
            }
            if (selectionX >= 0 && selectionX <= SizeMatrix && selectionY >= 0 && selectionY <= SizeMatrix && hit.collider.name == "Plane" && hit.collider.tag != "GameController" && hit.collider.tag != "Canvas")
                go = Instantiate(ClearBuildsPrefabs[0], GetTitleCenter(selectionX, selectionY), transform.rotation) as GameObject;
            tree = false;
        }
    }

    private void DrawBoard()
    {
        Vector3 widthLine = Vector3.right * SizeMatrix;
        Vector3 heightLine = Vector3.forward * SizeMatrix;

        for (int i = 0; i <= SizeMatrix; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= SizeMatrix; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }
        }
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }
    }
    //Метод для визначення центру
    public Vector3 GetTitleCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TITLE_SIZE * x) + TITLE_OFFSET;
        origin.z += (TITLE_SIZE * y) + TITLE_OFFSET;
        return origin;
    }

    //Метод для спайна будівль
    public void SpawnObject(int index, int x, int y)
    {
        go = Instantiate(BuildsPrefabs[index], GetTitleCenter(x, y), transform.rotation) as GameObject;
    }
    private Quaternion orientation = Quaternion.Euler(0, 0, 0);
    public void SpawnLandscape(int index, int x, int y)
    {
        go = Instantiate(LandscapePrefab[index], GetTitleCenter(x, y), orientation) as GameObject;
    }

    private void SpawnAll()
    {
        for (int i = 0; i < SizeMatrix; i++)
        {
            for (int j = 0; j < SizeMatrix; j++)
            {
                SpawnLandscape(0, i, j);
                //if((i == 0 && j == 0))
                //    SpawnLandscape(1, i, j);
            }
        }
    }
    
    public void UpdateSelection()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100.0f))
        {
            //Debug.Log(selectionX + "||" + selectionY);
            //text.text = selectionX + "||" + selectionY;
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }
    //Destroy zone
    private void Dest()
    {
        GameObject[] ObjectsZone = GameObject.FindGameObjectsWithTag("GameController"); //зона 
        //int[,] Clear = new int[Size, Size];
        for (int i = 0; i < ObjectsZone.Length; i++)
        {
            Destroy(ObjectsZone[i]);
        }
    }
    //Показ зони
    public void ClickMatrix()
    {
        Dest();       
        GameObject[] build = GameObject.FindGameObjectsWithTag("Building");             //всі будівлі
        GameObject[] Clearbuild = GameObject.FindGameObjectsWithTag("ClearBuild");             //всі очисні будівлі
        //int[,] clea = new int[96, 96];
        foreach (GameObject BuildInfo in build)
        {
            for (int i = 0; i < SizeMatrix; i++)
            {
                for (int j = 0; j < SizeMatrix; j++)
                {
                    Board[i, j] = BuildInfo.GetComponent<BuildRayCast>().ThisNotacces[i, j];
                    //clea[i, j] = clear.GetComponent<ClearBuild>().ThisNotacces[i, j];
                    foreach (GameObject item in Clearbuild)
                    {
                        if (item.GetComponent<ClearBuild>().ThisNotacces[i, j] == 3)
                            Board[i, j] = 3;
                    }
                    if (Board[i, j] == 1 && Board[i, j] != 3)
                        SpawnObject(2, i, j);
                    if (Board[i, j] == 2 && Board[i, j] != 3)
                        SpawnObject(3, i, j);
                }
            }
        }
    }
}