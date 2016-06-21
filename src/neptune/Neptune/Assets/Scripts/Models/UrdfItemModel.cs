/// <summary>
/// This is a model of urdfs stored in the database
/// </summary>
public class UrdfItemModel
{
    private static int unknownNameCount = 0;
    private readonly int propertyCount = 15; // update this when new property is added
    public int uid;
    public string name;
    public string modelNumber;
    public float internalCost;
    public float externalCost;
    public float weight;
    public float powerUsage;
    public string notes;
    public int visibility;
    public string type;
    public string category;
    public string urdfFilename;
    public string prefabFilename;
    public int usable;
    public float time;
    // TODO: Add validation code

    public bool checkPropertyCount(int length)
    {
        if(this.propertyCount != length )
        {
            return false;
        }
        return true;
    }

    public string GetCSV()
    {
        return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}",
            this.uid,
            this.name,
            this.modelNumber,
            this.internalCost,
            this.externalCost,
            this.weight,
            this.powerUsage,
            this.notes,
            this.visibility,
            this.type,
            this.category,
            this.urdfFilename,
            this.prefabFilename,
            this.usable,
            this.time);
    }

    public void copy(UrdfItemModel item)
    {
        this.uid = item.uid;
        this.name = item.name;
        this.modelNumber = item.modelNumber;
        this.internalCost = item.internalCost;
        this.externalCost = item.externalCost;
        this.weight = item.weight;
        this.powerUsage = item.powerUsage;
        this.type = item.type;
        this.category = item.category;
        this.usable = item.usable;
        this.visibility = item.visibility;
        this.urdfFilename = item.urdfFilename;
        this.prefabFilename = item.prefabFilename;
        this.notes = item.notes;
        this.time = item.time;
    }

    public void extract(string[] items)
    {
        checkPropertyCount(items.Length);

        if (!int.TryParse(items[0], out this.uid))
        {
            this.uid = 0;
        }
        if (string.IsNullOrEmpty(items[1]))
        {
            this.name = string.Format("Unknown {0}", unknownNameCount++);
        }
        else
        {
            this.name = items[1];
        }
        if (string.IsNullOrEmpty(items[2]))
        {
            this.modelNumber = "Unknown";
        }
        else
        {
            this.modelNumber = items[2];
        }
        if (!float.TryParse(items[3], out this.internalCost))
        {
            this.internalCost = 0;
        }
        if (!float.TryParse(items[4], out this.externalCost))
        {
            this.externalCost = 0;
        }
        if (!float.TryParse(items[5], out this.weight))
        {
            this.weight = 0;
        }
        if (!float.TryParse(items[6], out this.powerUsage))
        {
            this.powerUsage = 0;
        }
        this.notes = items[7];
        if (!int.TryParse(items[8], out this.visibility))
        {
            this.visibility = 0;
        }
        if (string.IsNullOrEmpty(items[9]))
        {
            this.type = "sensor";
        }
        else
        {
            this.type = items[9];
        }
        if (string.IsNullOrEmpty(items[10]))
        {
            this.category = "Unknown";
        }
        else
        {
            this.category = items[10];
        }
        
        this.urdfFilename = items[11];
        this.prefabFilename = items[12];
        if (!int.TryParse(items[13],out this.usable))
        {
            this.usable = 0;
        }
        if (!float.TryParse(items[14], out this.time))
        {
            this.time = 0;
        }
    }
}
