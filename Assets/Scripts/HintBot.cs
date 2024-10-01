using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;  // For UI elements like Text
// using Newtonsoft.Json;
using TMPro;

using SimpleJSON; 

public class HintBot : MonoBehaviour
{
    private string URL = "https://breadboard-community.wl.r.appspot.com/boards/@AdventurousChimpanzee/game-rules.bgl.api/run";
    private string API_KEY = "bb-646uc4s282ai2s4a1f214e4z1p681u44l291x6t6r5j4o3d6o2";
    public TextMeshProUGUI outputTxt;  // Reference for displaying the result in the UI

    public TextMeshProUGUI element;   //input element 2 (change to enum)



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
        Debug.Log("started!");

    }






    void OnButtonClick()
    {
        
        



        string msg = element.text;



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
        var jsonPayload = new Payload
        {
            key = API_KEY,
            text = new TextData
            {
                role = "user",
                parts = new Part[]
                {
                    new Part{ text = msg }
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


            Debug.Log($"Type: {request.downloadHandler.text.GetType()}");
            string stringOutput = request.downloadHandler.text;
            Debug.Log($"Response: {stringOutput}");
            
            //Debug.Log($"Response: {stringOutput}");
            SimpleJSON.JSONNode stuff = SimpleJSON.JSON.Parse(stringOutput);
            // Debug.Log($"Response: {stuff["text"]}");
            



            // for (int i = 0; i < 10; i++)
            // {
            //     Debug.Log($"Response {i}: {stuff[1][1][0][1][0][0]}");
            // }
            //  Debug.Log($"Response 1: {stuff}");
            //  Debug.Log($"Response 2: {stuff[1][1]}");
            //  Debug.Log($"Response 3: {stuff[1][1][0]}");
            //  Debug.Log($"Response 4: {stuff[1][1][0][1]}");
            //  Debug.Log($"Response 5: {stuff[1][1][0][1][0]}");
            //  Debug.Log($"Response 6: {stuff[1][1][0][1][0][0]}");



            outputTxt.text = stuff[1][1][0][1][0][0]["text"];

        }


    }
}