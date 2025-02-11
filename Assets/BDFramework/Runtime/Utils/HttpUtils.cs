using System.Text;
using UnityEngine.Networking;


public class HttpUtils
{
    public delegate void OnHttpCompleted(string err, object body);

    public static void Get(string url, string param, OnHttpCompleted OnCompleted)
    {
        string urlPath = url;
        if (param != null)
        {
            urlPath = url + "?" + param;
        }

        UnityWebRequest wq = UnityWebRequest.Get(urlPath);
        wq.SendWebRequest().completed += _ =>
        {
            if (wq.error != null)
            {
                OnCompleted?.Invoke(wq.error, null);
            }
            else
            {
                if (OnCompleted != null)
                {
                    if (wq.downloadHandler.text != null)
                    {
                        OnCompleted(null, wq.downloadHandler.text);
                    }
                    else
                    {
                        OnCompleted(null, wq.downloadHandler.data);
                    }
                }
            }
            wq.Dispose();
        };
    }

    public static void Post(string url, string param, string jsonBody, OnHttpCompleted OnCompleted)
    {
        string urlPath = url;
        if (param != null)
        {
            urlPath = url + "?" + param;
        }

        var request = new UnityWebRequest(urlPath, "POST");
        request.downloadHandler = new DownloadHandlerBuffer();
        if (!string.IsNullOrEmpty(jsonBody))
        {
            byte[] array = Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(array);
            request.uploadHandler.contentType = "application/json;charset=utf-8";
        }

        // UnityWebRequest wq = UnityWebRequest.Post(urlPath, jsonBody, "application/json");
        request.SendWebRequest().completed += _ =>
        {
            if (request.error != null)
            {
                OnCompleted?.Invoke(request.error, null);
            }
            else
            {
                if (OnCompleted != null)
                {
                    if (request.downloadHandler.text != null)
                    {
                        OnCompleted(null, request.downloadHandler.text);
                    }
                    else
                    {
                        OnCompleted(null, request.downloadHandler.data);
                    }
                }
            }
            request.Dispose();
        };
    }
}
