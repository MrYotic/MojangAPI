using MojangAPI;
using MojangAPI.DataTypes;
using System.Net;

API api = new API();
ModelProfile profile = api.GetModelProfileByUUID("4566e69fc90748ee8d71d7ba5aa00d20");
int i = 0;