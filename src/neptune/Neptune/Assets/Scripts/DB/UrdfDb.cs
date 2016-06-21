using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System;
using System.IO;
#endif

/// <summary>
/// This is the model for URDF interactions which contains actions to interact with the DB
/// </summary>
public class UrdfDb
{
    private readonly string DB_FILENAME = "DB/sensors";
    private string usingFilepath = Application.dataPath + "/Resources/";
    private string firstLine = "";
    private UrdfItemModel[] UrdfItems;
    private bool tryOnce = true;
    private int arrayPadding = 10;
    private int lastIndex = 0;

    /// <summary>
    /// Constructor for the urdf model using the default connection string
    /// </summary>
    /// <param name="connString">string representation to connect to the sqlite db</param>
    public UrdfDb()
    {
        this.usingFilepath += this.DB_FILENAME + ".csv";

        TextAsset dbFile = Resources.Load(this.DB_FILENAME) as TextAsset;
        string[] rows = dbFile.text.Split('\n');
        this.UrdfItems = new UrdfItemModel[rows.Length + arrayPadding];
        UrdfItemModel item;

        int lineCnt = 0;
        foreach(string line in rows)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            if (string.IsNullOrEmpty(this.firstLine))
            {
                this.firstLine = line;
                continue;
            }
            else
            {
                item = new UrdfItemModel();
                item.extract(line.Split('\t'));
                this.UrdfItems[lineCnt++] = item;
                lastIndex = lineCnt;
            }
        }
    }
#if UNITY_EDITOR
    /// <summary>
    /// Take changes done to the sensors and saves them back in the csv file.
    /// </summary>
    public void Save()
    {
        try
        {
            using (StreamWriter file = new StreamWriter(this.usingFilepath))
            {
                file.WriteLine(firstLine);
                foreach(UrdfItemModel item in this.UrdfItems)
                {
                    if (item == null)
                    {
                        continue;
                    }
                    file.WriteLine(item.GetCSV());
                }
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        catch (IOException)
        {
            // file is open as read-only create new file for back
            this.usingFilepath = this.usingFilepath + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss.csv");
            if (tryOnce)
            {
                tryOnce = false;
                Save();
            }
        }
    }
#endif

    /// <summary>
    /// This will query for all urdfs in the database. 
    /// </summary>
    /// <returns>A list of all urdfs in the database.</returns>
    public UrdfItemModel[] GetUrdfs()
    {
        return this.UrdfItems;
    }


    /// <summary>
    /// This will query for all usable sensors in the database by default unless otherwise specified. 
    /// A sensor is usable if it has an appropriate mesh and data associated to it. 
    /// </summary>
    /// <param name="all">Inclusion of sensors that are not usable</param>
    /// <returns>A list of all sensors/urdfs in the database.</returns>
    public UrdfItemModel[] GetSensors(bool all = false)
    {
        UrdfItemModel[] sensors = new UrdfItemModel[this.UrdfItems.Length];
        int index = 0;
        foreach (UrdfItemModel item in this.UrdfItems)
        {
            if (item == null)
            {
                continue;
            }
            if (item.type.Equals("sensor"))
            {
                sensors[index++] = item;
            }
        }
        UrdfItemModel[] actual = new UrdfItemModel[index];
        for (int i = 0; i < index; i++)
        {
            actual[i] = sensors[i];
        }
        return actual;
    }
    /// <summary>
    /// This will query for all usable robots in the database by default unless otherwise specified. 
    /// A robot is usable if it has an appropriate mesh and data associated to it. 
    /// </summary>
    /// <returns>A list of all robots/urdfs in the database.</returns>
    public UrdfItemModel[] GetRobots()
    {
        UrdfItemModel[] robots = new UrdfItemModel[this.UrdfItems.Length];
        int index = 0;
        foreach (UrdfItemModel item in this.UrdfItems)
        {
            if (item == null)
            {
                continue;
            }
            if (item.type.Equals("sensor"))
            {
                robots[index++] = item;
            }
        }
        UrdfItemModel[] actual = new UrdfItemModel[index];
        for (int i = 0; i < index; i++)
        {
            actual[i] = robots[i];
        }
        return actual;
    }

    /// <summary>
    /// This will grab all the urdf types that is known in the database
    /// </summary>
    /// <returns>A list of type of urdf in the database.</returns>
    public string[] GetUrdfTypes()
    {
        // this is hard coded for now due to memory constraints on WebGL.
        string[] list = new string[this.UrdfItems.Length];
        int index = 0;
        foreach (UrdfItemModel model in this.UrdfItems)
        {
            if (model == null)
            {
                continue;
            }
            if (!this.Contains(model.type,list))
            {
                list[index++] = model.type;
            }
        }
        string[] actual = new string[index];
        for (int i = 0; i < index; i++)
        {
            actual[i] = list[i];
        }
        return actual;
    }

    /// <summary>
    /// Retrieves the categories the list of categories within the database.
    /// </summary>
    /// <returns>A List of categories found. List may be empty.</returns>
    public string[] GetSensorCategories()
    {
        string[] list = new string[this.UrdfItems.Length];
        int index = 0;
        foreach(UrdfItemModel model in this.UrdfItems)
        {
            if (model == null)
            {
                continue;
            }
            if (!this.Contains<string>(model.category, list))
            {
                list[index++] = (model.category);
            }
        }
        string[] actual = new string[index];
        for (int i = 0; i < index; i++)
        {
            actual[i] = list[i];
        }

        return actual;
    }

    /// <summary>
    /// This will add a given UrdfItemModel into the database with provided details.
    /// </summary>
    /// <param name="item">UrdfItemModel to be added into the database</param>
    /// <returns>The insert id of the newely added id. 0 if it was not added successfully.</returns>
    public int AddSensor(UrdfItemModel item)
    {
        if (this.lastIndex == this.UrdfItems.Length)
        {
            UrdfItemModel[] extend = new UrdfItemModel[this.UrdfItems.Length + arrayPadding];
            for( int i = 0; i < this.UrdfItems.Length; i++)
            {
                extend[i] = this.UrdfItems[i];
            }
            this.UrdfItems = extend;
        }
        item.uid = this.UrdfItems[this.lastIndex - 1].uid + 1;
        this.UrdfItems[this.lastIndex++] = item;
        return item.uid;
    }

    /// <summary>
    /// Update the given UrdfItemModel in the database with the given values.
    /// </summary>
    /// <param name="item">The UrdfItemModel that is to be updated in the database.</param>
    /// <returns>The number of rows affected by the update. 0 if the id was not found.</returns>
    public int UpdateSensor(UrdfItemModel item)
    {
        int index = this.FindUidIndex(this.UrdfItems, item.uid);
        if (index > -1)
        {
            this.UrdfItems[item.uid-1] = item;
            return 1;
        }
        return 0;
    }

    /// <summary>
    /// This allows for flexibilty of deleting a UrdfItemModel with the object
    /// </summary>
    /// <param name="item">UrdfItemModel that is to be removed in the database</param>
    /// <returns>The number of rows affected. 0 if the id was not found</returns>
    public int DeleteSensor(UrdfItemModel item)
    {
        return DeleteSensor(item.uid);
    }

    /// <summary>
    /// Give the unique id of the sensor this method will remove it from the DB if it exists.
    /// </summary>
    /// <param name="UrdfItemModelId">int representation of the sensor id in the database</param>
    /// <returns>The number of rows there were affected by the delete statement, 0 if the id was not found</returns>
    public int DeleteSensor(int UrdfItemModelId)
    {
        
        int index = this.FindUidIndex(this.UrdfItems, UrdfItemModelId);
        if (index >= 0)
        {
            for (int i = index; i < this.UrdfItems.Length - 1; i++)
            {
                this.UrdfItems[i] = this.UrdfItems[i + 1];
            }
            this.lastIndex--;
            return 1;
        }
        return 0;
    }

    public bool Contains <T>(T source, T[] values)
    {
        foreach (T value in values)
        {
            if (source.Equals(value))
                return true;
        }
        return false;
    }

    public int FindUidIndex(UrdfItemModel[] array, int uid)
    {
        int index = -1;
        int cnt = 0;
        foreach (UrdfItemModel model in array)
        {
            if(model.uid == uid)
            {
                index = cnt;
                break;
            }
            cnt++;
        }
        return index;
    }

    public int FindStringIndex(string[] strings, string value)
    {
        int index = -1;
        int cnt = 0;
        foreach (string item in strings)
        {
            if (item.Equals(value))
            {
                index = cnt;
                break;
            }
            cnt++;
        }
        return index;
    }
}
