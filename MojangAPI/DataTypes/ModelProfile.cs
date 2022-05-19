using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojangAPI.DataTypes;

public class ModelProfile
{
    public string UUID { get; set; }
    public string Name { get; set; }
    public DateTime TimeStamp { get; set; }
    public bool? SignatureRequired { get; set; }
    public string Skin { get; set; }
    public string Cape { get; set; }
}
public class RawModelProfile 
{
    [JsonProperty("id")]
    public string UUID { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("properties")]
    public RawSkinProperties[] SkinData { get; set; }
}
public class RawSkinProperties
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("value")]
    public string Value { get; set; }
    [JsonProperty("signature")]
    public string Signature { get; set; }
}
public class RawSkinData
{
    [JsonProperty("timestamp")]
    public long TimeStamp { get; set; }
    [JsonProperty("profileId")]
    public string Name { get; set; }

    [JsonProperty("profileName")]
    public string UUID { get; set; }

    [JsonProperty("signatureRequired")]
    public bool SignatureRequired { get; set; }
    [JsonProperty("textures")]
    public RawSkinTextures SkinTextures { get; set; }
}
public class RawSkinTextures
{
    [JsonProperty("SKIN")]
    public RawSkin Skin { get; set; }
    [JsonProperty("CAPE")]
    public RawCape Cape { get; set; }
}
public class RawSkin
{
    [JsonProperty("url")]
    public string Url { get; set; }
}
public class RawCape
{
    [JsonProperty("url")]
    public string Url { get; set; }
}