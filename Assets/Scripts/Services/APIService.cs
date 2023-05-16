using Codice.CM.Common.Replication;
using Cysharp.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.Progress;

public static class APIService
{
    #region Classes
    [System.Serializable]
    public class RequestResult
    {
        public string Code;
        public string Description;
    }

    public enum RequestType
    {
        POST,
        GET,
        PUT,
        DELETE
    }
    #endregion
    public static string HostPortAddress { get; private set; } = "localhost";
    public static string URL => "http://" + HostPortAddress + "/api/";

    public static async UniTask<RequestResult> SendRequest(string url, RequestType requestType, string json = null)
    {
        var uwr = new UnityWebRequest(URL + url, GetRequestType(requestType));
        if (json is not null)
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            uwr.uploadHandler = new UploadHandlerRaw(jsonToSend);
        }
        uwr.downloadHandler = new DownloadHandlerBuffer();

        uwr.SetRequestHeader("Content-Type", "application/json");
        if (AuthController.UserToken is not null)
        {
            uwr.SetRequestHeader("Authorization", "Bearer "+AuthController.UserToken);
        }

        var cts = new CancellationTokenSource();
        var operation = uwr.SendWebRequest();
        try
        {
            await operation.WithCancellation(cts.Token);
        }
        catch
        {
            if (uwr.result == UnityWebRequest.Result.ConnectionError)
                MessageController.Instance.CallNetworkErrorMessage();
        }
        string code = uwr.responseCode.ToString();
        string description = uwr.result != UnityWebRequest.Result.Success ? uwr.error : uwr.downloadHandler.text;

        if (json is not null)
            uwr.uploadHandler.Dispose();
        uwr.downloadHandler.Dispose();
        uwr.Dispose();

        if (code == "0")
            return null;

        RequestResult returningData = new RequestResult();
        returningData.Code = code;
        returningData.Description = description;
        return returningData;
    }
    private static string GetRequestType(RequestType requestType)
    {
        switch (requestType)
        {
            case RequestType.POST:
                return "POST";
            case RequestType.GET:
                return "GET";
            case RequestType.PUT:
                return "PUT";
            case RequestType.DELETE:
                return "DELETE";
        }

        return "";
    }
}