using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class GeminiRecyclingModule : MonoBehaviour
{
    // API Key and URL
    // private const string ApiKey = "AIzaSyB6IdAQgvMgEVbUC7CBsreRvO09BaDAfgU";
    // private const string Url = "https://generativelanguage.googleapis.com/v1/models/gemini-1.5-flash:generateContent?key=";

    // private const string ChatGPTKey = "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";
    // private const string ChatGPTUrl = "https://api.openai.com/v1/chat/completions";

    private const string prompt = "trả lời theo dạng json sau: { \"value\" : \"String\", \"type\"  : \"Angry\" } với value là câu trả lời câu hỏi dưới đây (trả lời trong 30 từ), type là ngữ điệu của robot khi trả lời (bạn hãy tự xem xét và chọn type trong các type sau : HandWave (Vẫy tay xin chào), HeadShake (lắc đầu từ chối), Nod (gật đầu đồng ý), Angry (tức giận), Explain (Giải thích câu hỏi) Câu hỏi: ";
    private readonly string requestUrl = ChatGPTUrl;

    private Texture2D compressedTexture;

    //private const string prompt = "Qu'est-ce que c'est ? Comment dois-je recycler ? Que peut-il devenir après recyclage ? Réponse courte";
    private const string prompt1 = "Qu'est-ce que c'est ? Quelle est la traduction en vietnamien ? Réponds la plus courte possible !";

    //Test Input with Text
    public string inputText;

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

            // Log the request
            Debug.Log($"Request: {jsonRequest}");

            // Make API request with retry mechanism
            return await SendRequestWithRetry(client, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
        }
    }

    public async Task<string> ProcessStringAsync(string question)
    {
        using (var client = new HttpClient())
        {
            // Cấu trúc request theo ChatGPT API
            var request = new
            {
                model = "gpt-3.5-turbo",
                messages = new List<object>
                {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = question }
                }
            };

            var jsonRequest = JsonConvert.SerializeObject(request);

            // Log request
            Debug.Log($"Request: {jsonRequest}");

            // Thêm API key vào header
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ChatGPTKey}");
            return await SendRequestWithRetry(client, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
        }
    }

    private async Task<string> SendRequestWithRetry(HttpClient client, StringContent content, int maxRetries = 3)
    {
        int retryCount = 0;

        while (retryCount < maxRetries)
        {
            var response = await client.PostAsync(requestUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.Log($"Response: {responseContent}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize response JSON
                var result = JsonConvert.DeserializeObject<Response>(responseContent);
                return result.choices[0].message.content;
            }
            else if (response.StatusCode == (HttpStatusCode)429) // Quá nhiều request
            {
                retryCount++;
                int delay = (int)Math.Pow(2, retryCount) * 1000; // Exponential backoff
                Debug.LogWarning($"Quota exceeded. Retrying in {delay / 1000} seconds...");
                await Task.Delay(delay);
            }
            else
            {
                throw new Exception($"API request failed with status code {response.StatusCode}");
            }
        }
        return "Có vấn đề khi trong quá trình xử lý câu hỏi. Bạn hãy thử lại sau nhé";
    }

    // Các lớp dữ liệu cho response
    private class Response
    {
        public List<Choice> choices { get; set; }
    }

    private class Choice
    {
        public Message message { get; set; }
    }

    private class Message
    {
        public string role { get; set; }
        public string content { get; set; }
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

    public async void Test()
    {
        string result = await ProcessStringAsync(inputText);
        Debug.Log(result);
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


#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(GeminiRecyclingModule))]
public class RecyclingModuleEditor : UnityEditor.Editor{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Process"))
        {
            GeminiRecyclingModule module = (GeminiRecyclingModule)target;
            module.Test();
        }
    }
}
#endif