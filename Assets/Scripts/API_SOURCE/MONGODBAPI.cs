using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;  // For UI elements like Text
using TMPro;



public class MONGODBAPI : MonoBehaviour{
    [System.Serializable]
    public class jsonGET{
        public string reactant1;

    }

    [System.Serializable]
    public class reaction //reaction class 
    {
        public string reactant1;
        public string reactant2;
        public string result;
        public reaction(string r1, string r2, string r)
        {
            reactant1 = r1;
            reactant2 = r2;
            result = r;
        }

    }
    [System.Serializable]
    public class database{//database class
        public string id;
        public reaction[] reactions;

    }

    public Button myButton; //reference to the button

    public TextMeshProUGUI table_delimited;   //input element 2 (change to enum)

    public TextMeshProUGUI STATUS;   //input element 2 (change to enum)


    public TextMeshProUGUI ID;   //input element id



    private string apiUrl = "http://localhost:3000/id";

    // Start is called before the first frame update

    // public TextMeshProUGUI allIDs;  // Reference for displaying the result in the UI




    void Start()
    {
        // StartCoroutine(GetIDS());
        //Create("NewId12345"); 
        if (myButton != null)
        {
            // Add listener to the button to call OnButtonClick when clicked
            myButton.onClick.AddListener(OnButtonClick);
        }
    }


    void OnButtonClick()
    {
        Create("23423");
        // StartCoroutine(GetIDS("23423"));
        // GetIDS(ID.text);
    }

    reaction[] readReactionTable(string text){//takes in a 3xn text matrix and returns an array of reactions
        string[] fields = text.Split(',');
        reaction[] reactions = new reaction[fields.Length / 3];
            for (int i = 0; i < fields.Length; i += 3)
            {   

                reaction temp = new reaction(fields[i],fields[i+1],fields[i+2]);
                reactions[i] = temp;
            }

        return reactions;
        
    }


    public void Create(string newId)//creates a database of reactions
    {
        // This method will start the coroutine to send the data to the backend
        reaction[] reactions = readReactionTable(table_delimited.text);
        StartCoroutine(PostId(newId,reactions));
    }

    IEnumerator PostId(string idValue, reaction[] reactions)
    {
        // Create an object that contains the data
        var idData = new database
        {
            id = idValue,
            reactions  = reactions
        };

        // Convert the object to JSON using JsonUtility
        string jsonString = JsonUtility.ToJson(idData);
        Debug.Log("Print jsonString POST!" + jsonString);

        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonString);

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for a response
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error while sending POST request: " + request.error);
        }
        else
        {
            Debug.Log("ID STATUS: " + request.downloadHandler.text);
            STATUS.text= "ID STATUS: " + request.downloadHandler.text;
        }
    }
    void Load(){//This can load a database from our MongoDB instance 
   
            
    }



    //CHANGE ALL OF THIS SHIT
    IEnumerator GetIDS(string ID) //gets all of the IDS 
    {
        var getID = new database
        {
            id = ID,
        };

        string jsonString = JsonUtility.ToJson(getID);
        Debug.Log("Print GET jsonString!" + jsonString);

        // Convert the JSON string to byte array
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonString);

        // Create a UnityWebRequest with the GET method
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();


        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("data GET: " + request.downloadHandler.text);
        }
    }



    //Once you get all of the IDS, you can pick one:
    // void pickID(){




    // }




    // private database databaseOption = database();
}








// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using MongoDB.Driver;


// public class reaction
// {
//     public string reactant1;
//     public string reactant2;
//     public string result;
// }

// public class MONGODBAPI : MonoBehaviour
// {
//     // Start is called before the first frame update


//     MongoClient client = new MongoClient("mongodb+srv://lamcolin:<db_password>@atomdatabase.0hxfh.mongodb.net/?retryWrites=true&w=majority&appName=AtomDatabase");
//     IMongoDatabase database;
//     IMongoCollection<BsonDocument> collection;
//     void Start()
//     {
//         database = client.GetDatabase("HighScoreDB");
//     }


//     void load()
//     {

//     }

//     void create()
//     {
        
//     }



//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
