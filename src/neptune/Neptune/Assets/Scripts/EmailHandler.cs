using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class EmailHandler {
    private const string TEMPLATE_FILE = @"Templates/emailTemplate";
    private string emailEndPoint = "https://50.112.139.235/endpoint.php";
    private string uploadEndPoint = "https://50.112.139.235/upload.php";

    private const string CONTACT_NAME = "%%CONTACT_NAME%%";
    private const string SENSOR_LIST = "%%SENSOR_LIST%%";
    private const string ROBOT_IMAGES = "%%ROBOT_IMAGES%%";

    private string uploadFilename = "capture.png";
    private string uploadFileType = "image/png";

    private string email = "";
    private string images = "";

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

    public IEnumerator SendEmail(string email, string first, string last, string organization, string state, string country, string industry, string[] parts)
    {
        this.email = email;
        yield return this.UploadScreen();

        string sensors = "";
        List<string> sensorList = GameObject.FindGameObjectWithTag(UIManager.TAG).GetComponent<UIManager>().GetParts();
        foreach (string s in sensorList)
        {
            sensors += s + "<br>";
        }

        WWWForm form = new WWWForm();
		int num_captures = 4;
        string message = getMessage();
        message = message.Replace(CONTACT_NAME, first);
        message = message.Replace(SENSOR_LIST, string.Join(",<br/>",parts));
        message = message.Replace(ROBOT_IMAGES, images);
        form.AddField("email", email);
		form.AddField("firstname", first);
		form.AddField("lastname", last);
		form.AddField("organization", organization);
		form.AddField("state", state);
		form.AddField("country", country);
        form.AddField("message", message);
		form.AddField("num_screenshots", num_captures);
		form.AddField("parts_list", string.Join(",",parts));
        form.AddField("message", message);
		for(int i = 0; i < num_captures; i++) {
			form.AddField("ssName[]", "ROBOT-IMAGE-" + i + ".png");
		}
		
        WWW www = new WWW(emailEndPoint, form);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            DialogManager.instance.ShowDialog(SmartLocalization.LanguageManager.Instance.GetTextValue("Email.Error"), "", DialogManager.ButtonType.Okay);
            GameObject.FindGameObjectWithTag(EditorManager.TAG).GetComponent<EditorManager>().Ruler.SetActive(true);
        }
        else
        {
            Debug.Log("Email Request Completed.");
            Debug.Log(www.text);
            DialogManager.instance.ShowDialog(SmartLocalization.LanguageManager.Instance.GetTextValue("Email.Success"), "", DialogManager.ButtonType.Okay);
            GameObject.FindGameObjectWithTag(EditorManager.TAG).GetComponent<EditorManager>().Ruler.SetActive(true);
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

        images = "";
        UIManager ui = (GameObject.FindGameObjectWithTag(UIManager.TAG) as GameObject).GetComponent<UIManager>();
        EditorManager editor = (GameObject.FindGameObjectWithTag(EditorManager.TAG) as GameObject).GetComponent<EditorManager>();

        Vector3 mainCameraPositionCache = new Vector3(editor.MainCamera.transform.position.x,
                                                      editor.MainCamera.transform.position.y,
                                                      editor.MainCamera.transform.position.z);
        Vector3 mainCameraRotationCache = new Vector3(editor.MainCamera.transform.rotation.eulerAngles.x,
                                                      editor.MainCamera.transform.rotation.eulerAngles.y,
                                                      editor.MainCamera.transform.rotation.eulerAngles.z);
        editor.HandleCamera.gameObject.SetActive(false);

        int i = 0;
        foreach (Camera cam in ui.ScreenshotCameras)
        {
            editor.MainCamera.transform.position = cam.transform.position;
            editor.MainCamera.transform.rotation = cam.transform.rotation;
            // Create a texture the size of the screen, RGB24 format
            int width = cam.targetTexture.width;
            int height = cam.targetTexture.height;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

            cam.Render();
            RenderTexture.active = cam.targetTexture;
            // Read screen contents into the texture
            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tex.Apply();

            // Encode texture into PNG
            byte[] bytes = tex.EncodeToPNG();
            Texture2D.Destroy(tex);

            uploadFilename = "ROBOT-IMAGE-" + i + ".png";

            // Create a Web Form
            WWWForm form = new WWWForm();
            form.AddField("frameCount", Time.frameCount.ToString());
            form.AddField("email", email);
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
                images += "<a href='" + www.text + "'>Robot Image " + (i + 1) + "</a><br>";
            }

            cam.gameObject.SetActive(false);
            i++;
			
            yield return new WaitForSeconds(1);
        }

        editor.MainCamera.transform.position = mainCameraPositionCache;
        editor.MainCamera.transform.rotation = Quaternion.Euler(mainCameraRotationCache);
        editor.HandleCamera.gameObject.SetActive(true);
    }
}
