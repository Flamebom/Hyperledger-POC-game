    [System.Serializable]
public class WebOutput
{
    public string id;
    public string space;
    public string name;
    public string created;
    public string updated;
    public string collectionId;
    public string templateId;
    public string metadataComponent;
    public Values values;
    public string valuesHash;
    public string allocatedTo;
    public string uri;
    public object metadata;
    public string mintTransactionId;
    public string mintSigningKey;
    public string mintedTo;
    public PublishInfo publishInfo;
}

public class ImageName
{
    public string name;
}

public class Values
{
    public string description;
    public string external_url;
    public ImageData image;
}

public class ImageData
{
    public string id;
    public ImageName value;
    public string name;
    public string blobHash;
    public string dataId;
    public string dataHash;
    public string contentHash;
    public string contentReference;
}

public class Metadata
{
    public string dataId;
    public string dataHash;
    public string contentHash;
    public string contentReference;
}

public class PublishInfo
{
    public ImageData image;
    public Metadata metadata;
}
