using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utility : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] cameras;
    public GameObject[] canvases;

    public static Dictionary<string, string> actionSlotBindings = new Dictionary<string, string>();
    public static Dictionary<string, int> inputSymbols = new Dictionary<string, int>();


    void Awake()
    {
        ReadConfigs();


        //reset initialized levels file each time session is started when debugging
        if(!States.initializedLevelsFileInitialized && States.debugMode)
        {
            string litPath = Application.persistentDataPath + "/initializedLevels";
            File.WriteAllLines(litPath, new string[]{""});
            States.initializedLevelsFileInitialized = true;
        }
    }

    void Start()
    {
        this.enabled = true;
        
        
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip);
        //Debug.Log(GetFrame(animator.GetCurrentAnimatorStateInfo(0).normalizedTime));

        //0.346
        //1.125
    }

/*     void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static string CurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

     void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StaticPoint[] staticPoints;
        int state = 0;
        string id;
        Vector3 posVector = new Vector3();
        Vector3 rotVector = new Vector3();
        Interactable newItem = null;
        
        Debug.Log("Loaded: "+scene.name);
        GameObject PlayerStart = GameObject.Find(LoadingData.targetMarker);

        //Debug.Log(PlayerStart);

        if(PlayerStart == null)
        {
            PlayerStart = GameObject.Find("DefaultStart");
        }

        Debug.Log(PlayerStart);

        transform.position = PlayerStart.transform.position;
        transform.rotation = PlayerStart.transform.rotation;


        players = GameObject.FindGameObjectsWithTag("Player");
        cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        canvases = GameObject.FindGameObjectsWithTag("Canvas");

        Debug.Log("len "+players.Length);

        if(players.Length > 1)
        {
            Destroy(players[1]);
        }

        if(cameras.Length > 1)
        {
            Destroy(cameras[1]);
        }

        if(canvases.Length > 1)
        {
            Destroy(canvases[1]);
        }


        //initialize levels when they are loaded for the first time

        string litPath = Application.persistentDataPath + "/initializedLevels";

        string[] lines = File.ReadAllLines(litPath);

        List<string> initializedLevels = new List<string>(lines);

        //level is not in initialized list, get default state from scene itself the first time scene is loaded
        if(!initializedLevels.Contains(scene.name))
        {
            SaveSystem.InitStateFile(SceneManager.GetActiveScene().name);
            using (StreamWriter sw = File.AppendText(litPath))
            {
                sw.WriteLine(scene.name);
            }
        }
        //level has been initialized, just update it (get all info about interactables from file, remove initial interactables in scene)
        //fetch level state from file and Interactables from current scene, add/remove/change states in current scene to match the file
        else
        {
            
            string path = Application.persistentDataPath + "/levelstate_" + scene.name;
            string[] linesList = File.ReadAllLines(path);
            staticPoints = GameObject.FindObjectsByType<StaticPoint>(FindObjectsSortMode.None);            


            //go through lines
            for (int i = 0; i < linesList.Length - 1; i++) //last line is the current unique id and does not represent an object
            {
                string[] line = linesList[i].Split(" ");
                int type = int.Parse(line[0]);
                id = line[2];
                int shift;
                Debug.Log("id "+id);

                //check if object is marked for deletion, pass
                if(type == 0)
                {
                    continue;
                }
                //parse state if not an item
                else if(type != 2)
                {
                    state = int.Parse(line[3]);
                    shift = 1;
                }
                else
                {
                    shift = 0;
                }

                //rewrite the id into gameobject at scene
                //parse pos/rot from file
                //iterate through initial objects in scene
                //if an object is at same position than an object in file, set its id from file
                //if that object is also an InteractionPoint, set its state
                posVector.x = float.Parse(line[shift+3]);
                posVector.y = float.Parse(line[shift+4]);
                posVector.z = float.Parse(line[shift+5]);
                rotVector.x = float.Parse(line[shift+6]);
                rotVector.y = float.Parse(line[shift+7]);
                rotVector.z = float.Parse(line[shift+8]);

                int j = 0;
                for (; j < staticPoints.Length; j++)
                {
                    //Debug.Log(staticPoints[j].transform.position+" "+ posVector);
                    //position of a gameobject matches a position of the current line
                    if(Mathf.Approximately(staticPoints[j].transform.position.x, posVector.x) && 
                        Mathf.Approximately(staticPoints[j].transform.position.y, posVector.y) && 
                        Mathf.Approximately(staticPoints[j].transform.position.z, posVector.z))
                    {
                        //Debug.Log("setting id!");
                        staticPoints[j].id = id;

                        //object is not an item, set state of InteractionPoint to correct state from LevelState file
                        if(staticPoints[j] as ItemPickup == null)
                        {
                            staticPoints[j].state = (ObjectState)state;
                        }

                        break;
                    }
                }
                //scene does not contain a gameobject with position, so new object will have to be created
                if(j == staticPoints.Length + 1)
                {
                    //if line refers to item, go through droppedlist and instantiate item
                    if(type == 2)
                    {
                        for (int d = 0; d < ItemStorage.droppedItems.Count; d++)
                        {
                            if(ItemStorage.droppedItems[d].id.Equals(id))
                            {
                                newItem = ItemStorage.droppedItems[d];
                                GameObject.Instantiate(newItem.gameObject, posVector, Quaternion.Euler(rotVector.x, rotVector.y, rotVector.z));
                                break;
                            }
                        }
                    }
                    //line refers to enemy
                    else if(type == 3)
                    {

                    }
                    
                }
                

                

            }

            //if gameobject in scene did not get an id, destroy it
            for (int j = 0; j < staticPoints.Length; j++)
            {
                Debug.Log(staticPoints[j].id);
                if(staticPoints[j].id.Equals(""))
                {
                    Destroy(staticPoints[j].gameObject);
                }
            }
            
        }


        

    } */

    public static Vector3 DroppedItem(GameObject item, Vector3 sourcePosition, Vector3 playersDir)
    {
        sourcePosition += playersDir / 3.0f;
        var ray = new Ray(sourcePosition, Vector3.down);
        Vector3 groundedPos = sourcePosition;

        var everythingExceptPlayers = ~(1 << LayerMask.NameToLayer("Player"));

        var layerMask = Physics.DefaultRaycastLayers & everythingExceptPlayers;

        RaycastHit hit;

        Debug.DrawRay(sourcePosition,Vector3.down);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            groundedPos.y -= hit.distance;
            Debug.Log(groundedPos);
        }

        BoxCollider itemMeshBounds = item.GetComponent<BoxCollider>();

        groundedPos.y += (itemMeshBounds.size.z * item.transform.localScale.y) / 2.0f;


        return groundedPos;
    }


    public static int GetFrame(Animator animator, float percentage)
    {
        AnimationClip clip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        float lengthOfAnim = clip.length;   //1.125
        int animFrameCount = Mathf.RoundToInt(lengthOfAnim * clip.frameRate); //27

        return Mathf.FloorToInt(animFrameCount * percentage);
    }

    public static float GetPercentage(Animator animator, int frame)
    {
        AnimationClip clip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        float lengthOfAnim = clip.length;
        int animFrameCount = Mathf.RoundToInt(lengthOfAnim * clip.frameRate);

        return frame*1.0f / animFrameCount;
    }

    public static float GetCurrentProgress(Animator animator)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float progress = stateInfo.normalizedTime;
        return progress % 1.0f;
    }


    public static void ReadConfigs()
    {
        string path1 = Application.persistentDataPath + "/config/actionslot.bps";
        string path2 = Application.persistentDataPath + "/config/input.bps";
        string[] linesList = File.ReadAllLines(path1);
        Debug.Log("reading "+linesList.Length+" lines in actionslot.bps!");

        string pattern = @"\s";
        string uDir = "(Up |up)";
        string dDir = "(Down |down)";
        string lDir = "(Left |left)";
        string rDir = "(Right |right)";

        foreach (var line in linesList)
        {
            if(line.Contains('#') || line.Equals(""))
            {
                continue;
            }
            string s1 = Regex.Replace(line.Split(":")[0],pattern,string.Empty);
            string s2 = Regex.Replace( line.Split(":")[1],pattern,string.Empty);
            Debug.Log(s1+" "+s2);
            s2 = Regex.Replace(s2,uDir,"u");
            s2 = Regex.Replace(s2,dDir,"d");
            s2 = Regex.Replace(s2,lDir,"l");
            s2 = Regex.Replace(s2,rDir,"r");
            actionSlotBindings.Add(s1, s2);
            //Debug.Log("adding binding of actionSlot "+line.Split(":")[0]+" to input "+line.Split(":")[1]);
        }

        linesList = File.ReadAllLines(path2);
        Debug.Log("reading "+linesList.Length+" lines in input.bps!");

        foreach (var line in linesList)
        {
            if(line.Contains('#') || line.Length == 0)
            {
                continue;
            }
            inputSymbols.Add(line.Split("=")[0],int.Parse(line.Split("=")[1]));
            //Debug.Log("adding binding of input symbol "+line.Split("=")[0]+" to number "+line.Split("=")[1]);
        }

    }

    public static string ConvertInputs(string input)
    {
        string result = "";

        string[] t = input.Split(",");
        List<string[]> t2 = new List<string[]>();

        foreach (var item in t)
        {
            t2.Add(item.Split("+"));
        }

        int i = 0;

        if(input[i].Equals('R'))
        {
            if(input[i+1].Equals('1'))
            {
                result += inputSymbols["R1"];
            }
            else if(input[i+1].Equals('2'))
            {
                result += (inputSymbols["R2"]);
            }
        }
        else if(input[i].Equals('L'))
        {
            if(input[i+1].Equals('1'))
            {
                result += (inputSymbols["L1"]);
            }
            else if(input[i+1].Equals('2'))
            {
                result += (inputSymbols["L2"]);
            }
        }
        else if(input[i].Equals("-") && input[i+1].Equals(">"))
        {
            result += (inputSymbols["->"]);
        }
        else if(input[i].Equals("n") && input[i+1].Equals("e"))
        {
            result += (inputSymbols["ne"]);
        }
        else if(input[i].Equals("s") && input[i+1].Equals("e"))
        {
            result += (inputSymbols["se"]);
        }
        else if(input[i].Equals("s") && input[i+1].Equals("w"))
        {
            result += (inputSymbols["sw"]);
        }
        else if(input[i].Equals("n") && input[i+1].Equals("w"))
        {
            result += (inputSymbols["nw"]);
        }
        else
        {
            result += (inputSymbols[input[i]+""]);
        }
            
        
        return result;
    }


    public static string GetActionSlotBinding(string actionSlot)
    {
        return actionSlotBindings[actionSlot];
    }

}
