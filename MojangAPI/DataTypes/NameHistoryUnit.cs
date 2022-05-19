using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojangAPI.DataTypes;

public class NameHistoryUnit
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("changedToAt")]
    public long? ChangeAtTo { get; set; }
}