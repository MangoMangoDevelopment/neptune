using System.Collections;
using UnityEngine;

public class EmailHandler {
    private const string TEMPLATE_FILE = @"Templates/emailTemplate";
    private string emailEndPoint = "http://54.148.182.29/endpoint.php";
    private string uploadEndPoint = "http://54.148.182.29/upload.php";

    private const string CONTACT_NAME = "%%CONTACT_NAME%%";
    private const string ROBOT_LINK = "%%ROBOT_LINK%%";

    private string uploadFilename = "capture.png";
    private string uploadFileType = "image/png";

    private string imgUrl;

    public string UploadFilename 
    {
        get
        {
            return uploadFilename;
        }
        set
        {
            string tmpValue = value;
            if (!value.Substring(-4).Equals(".png"))
            {
                tmpValue += ".png";
            }
            uploadFilename = tmpValue;
        }
    }

    public IEnumerator SendEmail(string name)
    {
        yield return this.UploadScreen();

        WWWForm form = new WWWForm();
        string message = getMessage();
        message = message.Replace("%%CONTACT_NAME%%", name);
        message = message.Replace("%%ROBOT_LINK%%", this.imgUrl);
        form.AddField("message", message);
        form.AddField("attachment_url", this.imgUrl);
        WWW www = new WWW(emailEndPoint, form);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Email Request Completed.");
            Debug.Log(www.text);
        }
    }

    public string getMessage ()
    {
        TextAsset template = Resources.Load(TEMPLATE_FILE) as TextAsset;
        return template.text;
    }

    public string getUploadUrl()
    {
        return this.uploadEndPoint + "/" + this.uploadFilename;
    }

    public IEnumerator UploadScreen ()
    {
        // We should only read the screen after all rendering is complete
        yield return new WaitForEndOfFrame();

        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        Texture2D.Destroy(tex);

        // Create a Web Form
        WWWForm form = new WWWForm();
        form.AddField("frameCount", Time.frameCount.ToString());
        form.AddBinaryData("fileUpload", bytes, this.uploadFilename, this.uploadFileType);

        // Upload to a cgi script
        WWW www = new WWW(this.uploadEndPoint, form);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Finished Uploading Screenshot");
            this.imgUrl = www.text; //expects the server to return the url of img uploaded.
        }
    }
}
