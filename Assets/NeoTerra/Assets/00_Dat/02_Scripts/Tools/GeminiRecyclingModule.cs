using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GeminiRecyclingModule : MonoBehaviour
{
    // API Key and URL
    private const string ApiKey = "AIzaSyB6IdAQgvMgEVbUC7CBsreRvO09BaDAfgU";
    private const string Url = "https://generativelanguage.googleapis.com/v1/models/gemini-1.5-flash:generateContent?key=";
    private readonly string requestUrl = Url + ApiKey;

    private Texture2D compressedTexture;

    private const string prompt = "Qu'est-ce que c'est ? Comment dois-je recycler ? Que peut-il devenir après recyclage ? Réponse courte";
    private const string prompt1 = "Qu'est-ce que c'est ? Quelle est la traduction en vietnamien ? Réponds la plus courte possible !";

    // Output text property
    public string OutputText;

    // Start is called before the first frame update
    public async Task<string> StartScan(Texture2D texture, bool isLearning = false)
    {
        compressedTexture = texture;
        try
        {
            Debug.Log("Processing image...");
            //Debug.Log($"Input image base64: {InputImageBase64}");
            return await ProcessImageAsync(isLearning);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error processing image: {e.Message}");
            throw new Exception($"Error processing image: {e.Message}");
        }
    }

    // Method to process image
    public async Task<string> ProcessImageAsync(bool isLearning = false)
    {
        using (var client = new HttpClient())
        {
            Texture2D uncompressedTexture = CopyTexture2D(compressedTexture);
            byte[] pngData = uncompressedTexture.EncodeToPNG();
            string base64String = Convert.ToBase64String(pngData);

            var request = new Root
            {
                contents = new List<Content>
                {
                    new Content
                    {
                        parts = new Part[]
                        {
                            new Part
                            {
                                text = isLearning? prompt1 : prompt,
                                inlineData = new InlineData
                                {
                                    mimeType = "image/png",
                                    data = base64String
                                }
                            }
                        }
                    }
                }
            };

            var jsonRequest = ConvertToJson(request);



            // var jsonRequest = @"
            // {
            //     ""contents"": [
            //         {
            //             ""parts"": [
            //                 {
            //                     ""text"": ""Qu'est-ce que c'est? Comment dois-je recycler ? Que peut-il devenir après recyclage ? Réponse courte""
            //                 },
            //                 {
            //                     ""inlineData"": {
            //                         ""mimeType"": ""image/png"",
            //                         ""data"": """ + InputImageBase64 + @"""
            //                     }
            //                 }
            //             ]
            //         }
            //     ]
            // }";

            //request = "{\"contents\":[{\"parts\":[{\"text\":\"Qu'est-ce que c'est? Comment dois-je recycler ? Que peut-il devenir après recyclage ? Réponse courte\",\"inlineData\":{\"mimeType\":\"image/png\",\"data\":\"" + InputImageBase64 + "\"}}]}]}

            // Serialize the request to JSON
            // var jsonRequest = JsonUtility.ToJson(request);

            // Log the request
            Debug.Log($"Request: {jsonRequest}");


            // Make API request
            var response = await client.PostAsync(requestUrl, new StringContent(/*JsonUtility.ToJson(request)*/jsonRequest, Encoding.UTF8, "application/json"));


            // Log the response
            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.Log($"Response: {responseContent}");

            // Process response
            if (response.IsSuccessStatusCode)
            {
                var result = JsonUtility.FromJson<Response>(responseContent);
                return result.candidates[0].content.parts[0].text;
            }
            else
            {
                throw new Exception($"API request failed with status code {response.StatusCode}");
            }
        }
    }

    public string ConvertToJson(Root root)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("{ \"contents\": [ { \"parts\": [ ");
        foreach (var part in root.contents[0].parts)
        {
            if (part.text != null)
            {
                sb.Append("{ \"text\": \"" + part.text + "\" }, ");
            }
            if (part.inlineData != null)
            {
                sb.Append("{ \"inlineData\": { \"mimeType\": \"" + part.inlineData.mimeType + "\", \"data\": \"" + part.inlineData.data + "\" } }, ");
            }
        }
        // Remove trailing comma and space
        sb.Remove(sb.Length - 2, 2);
        sb.Append(" ] } ] }");
        return sb.ToString();
    }

    Texture2D CopyTexture2D(Texture2D copiedTexture)
    {
        // Create a new Texture2D, which will be a copy of the original Texture2D
        Texture2D texture = new Texture2D(copiedTexture.width, copiedTexture.height);
        // Copy the pixels from the original texture to the new one
        texture.SetPixels(copiedTexture.GetPixels());
        // Apply the changes
        texture.Apply();
        return texture;
    }
}

[Serializable]
public class InlineData
{
    public string mimeType;
    public string data;
}

[Serializable]
public class Part
{
    public string text;
    public InlineData inlineData;
}

[Serializable]
public class Content
{
    public Part[] parts;
}

[Serializable]
public class Request
{
    public Content[] contents;
}

[Serializable]
public class Root
{
    public List<Content> contents { get; set; }
}

[Serializable]
public class Candidate
{
    public Content content;
}

[Serializable]
public class Response
{
    public Candidate[] candidates;
}