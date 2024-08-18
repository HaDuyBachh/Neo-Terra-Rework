using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using System.IO;

public class TTSModule : MonoBehaviour
{
    public AudioSource audioSource;

    [Serializable]
    public class TTSRequest
    {
        public Input input;
        public Voice voice;
        public AudioConfig audioConfig;
    }

    [Serializable]
    public class Input
    {
        public string text;
    }

    [Serializable]
    public class Voice
    {
        public string languageCode;
        public string name; // e.g., "en-US-Standard-C"
    }

    [Serializable]
    public class AudioConfig
    {
        public string audioEncoding; // "LINEAR16" for WAV
    }

    [Serializable]
    public class TTSResponse
    {
        public string audioContent;
    }

    public void SpeakText(string text)
    {
        IsComplete = false;
        StartCoroutine(SendTTSRequest(text));
    }

    IEnumerator SendTTSRequest(string text)
    {
        string url = "https://us-central1-texttospeech.googleapis.com/v1beta1/text:synthesize?key=AIzaSyBZ0hLOmDpXJ3qpwCCzsKHngTznWPcx7UA";
        //string apiKey = "AIzaSyBZ0hLOmDpXJ3qpwCCzsKHngTznWPcx7UA"; // Replace with your actual API key

        TTSRequest requestData = new TTSRequest
        {
            input = new Input { text = text },
            voice = new Voice { languageCode = "fr-FR", name = "fr-FR-Standard-A" }, // French voice
            audioConfig = new AudioConfig { audioEncoding = "LINEAR16" }
        };
        string requestJson = JsonUtility.ToJson(requestData);
        Debug.Log(requestJson);
        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            float delayTime = 0;
            while (true)
            {
                webRequest.SetRequestHeader("Content-Type", "application/json");
                webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(requestJson));
                webRequest.downloadHandler = new DownloadHandlerBuffer();
                //webRequest.SetRequestHeader("Authorization", $"key= {apiKey}"); // Add authentication

                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("TTS Error: " + webRequest.error + " " + webRequest.result);
                }
                else
                {


                    string audioContent = JsonUtility.FromJson<TTSResponse>(webRequest.downloadHandler.text).audioContent; // Replace with your actual audio content
                    byte[] audioBytes = Convert.FromBase64String(audioContent);

                    // Convert byte array to AudioClip
                    int sampleCount = audioBytes.Length / 2;
                    float[] audioData = new float[sampleCount];
                    for (int i = 0; i < sampleCount; i++)
                    {
                        audioData[i] = BitConverter.ToInt16(audioBytes, i * 2) / 32768f;
                    }
                    AudioClip audioClip = AudioClip.Create("TTS", sampleCount, 1, 24000, false);
                    audioClip.SetData(audioData, 0);

                    // Play the AudioClip
                    audioSource.clip = audioClip;
                    audioSource.Play();
                    delayTime = audioClip.length;
                    break;
                }
            }
            yield return new WaitForSeconds(delayTime);
            IsComplete = true;
        }
    }

    public bool IsComplete {get; private set;}
}
