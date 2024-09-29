using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;  // For UI elements like Text
// using Newtonsoft.Json;
using TMPro;

using SimpleJSON; 

public class RESTAPI : MonoBehaviour
{
    private string URL = "https://breadboard-community.wl.r.appspot.com/boards/@AdventurousChimpanzee/chat-with-your-scientist.bgl.api/run";
    private string API_KEY = "bb-646uc4s282ai2s4a1f214e4z1p681u44l291x6t6r5j4o3d6o2";
    public TextMeshProUGUI outputTxt;  // Reference for displaying the result in the UI
    public TextMeshProUGUI inputTxt;   // Reference for taking input from the UI

    public TextMeshProUGUI reactant12;   // input element 1 (change to enum)


    public TextMeshProUGUI product;   //input element 2 (change to enum)

    public TextMeshProUGUI question;   //input element 2 (change to enum)



    public Button myButton; //reference to the button


    [System.Serializable]
    public class Payload
    {
        public string key;
        public TextData text;
    }

    [System.Serializable]
    public class TextData
    {
        public string role;
        public Part[] parts;
    }

    [System.Serializable]
    public class Part
    {
        public string text;
    }









    
    void Start()
    {
        // You can start the request when the script starts or based on user input

        if (myButton != null)
        {
            // Add listener to the button to call OnButtonClick when clicked
            myButton.onClick.AddListener(OnButtonClick);
        }

    }






    void OnButtonClick()
    {
        
        



        string msg = "the reactants " + reactant12.text + " reacted to form" + product.text + " based on this context, answer: " + question.text;



        // string msg = inputTxt.text;
        Debug.Log("Print Response!" + msg);

        StartCoroutine(PostData(msg));
    }






    string InsertCharacterAt(string str, char character, int index)//substring
    {
        return str.Substring(0, index) + character + str.Substring(index);
    }


    IEnumerator PostData(string msg)
    {
        // Create JSON payload
        string message = msg;
        var jsonPayload = new Payload
        {
            key = API_KEY,
            text = new TextData
            {
                role = "user",
                parts = new Part[]
                {
                    new Part{ text = message }
                }
            }
        };

        // Serialize the payload to JSON
        string jsonString = JsonUtility.ToJson(jsonPayload);
        jsonString = InsertCharacterAt(jsonString, '$', 2);

        Debug.Log("Serialized JSON: " + jsonString);

        // Convert JSON string to a byte array
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonString);

        // Create UnityWebRequest with POST method
        UnityWebRequest request = new UnityWebRequest(URL, "POST");

        // Set UploadHandler with raw byte array payload
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);

        // Set DownloadHandler to get the response
        request.downloadHandler = new DownloadHandlerBuffer();

        // Set content type to JSON
        request.SetRequestHeader("Content-Type", "application/json");




        // Send the request and wait for a response
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error: {request.error}");
        }
        else
        {
            Debug.Log($"Response Code: {request.responseCode}");


            string stringOutput = request.downloadHandler.text;
            
            //Debug.Log($"Response: {stringOutput}");
            SimpleJSON.JSONNode stuff = SimpleJSON.JSON.Parse(stringOutput);

            // Debug.Log($"Response: {stuff["text"]}");
            



            // for (int i = 0; i < 10; i++)
            // {
            //     Debug.Log($"Response {i}: {stuff[1][1][0][1][0][0]}");
            // }
             Debug.Log($"Response : {stuff[1][1][0][1][0][0]["text"]}");



            outputTxt.text = stuff[1][1][0][1][0][0]["text"];

        }





        
    }
}

























// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RESTAPI : MonoBehaviour
// {
//     // Start is called before the first frame update


//     private string URL = "https://breadboard-community.wl.r.appspot.com/boards/@AdventurousChimpanzee/chat-with-your-scientist.bgl.api/run"
//     private string API_KEY = "bb-646uc4s282ai2s4a1f214e4z1p681u44l291x6t6r5j4o3d6o2"
    
//     public Text outputTxt;
//     public Text inputTxt;


//     void Start()
//     {
//         StartCoroutine(GetDatas());
//     }

//     // Update is called once per frame
//     // void Update()
//     // {
        
//     // }

//     IEnumerator GetDatas(){
//         using(UnityWebRequest request = UnityWebRequest.Get(URL)){
        
//         yield return request.SendWebRequest();

//         if(request.result == UnityWebRequest.Result.ConnectionError)
//             Debug.LogError(request.error); // if the request does not get through
//         else{

//             string json = request.downloadHandler.text;
//             SimpleJson.JSONNODE stats = SimpleJSON.JSON.Parse(json)

//             LevelText.text = "LEvel"
//         }


//         }
//     }
// }