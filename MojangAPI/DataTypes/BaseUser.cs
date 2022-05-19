using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MojangAPI.DataTypes;

public class BaseUser
{
    [JsonProperty("id")]
    public string UUID { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
}