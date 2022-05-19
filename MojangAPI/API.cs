using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using MojangAPI.DataTypes;
using Newtonsoft.Json;

namespace MojangAPI;

public class API
{
    #region Hood
    private class PostRequest<T>
    {
        public PostRequest(string url, string contentType = "application/json")
        {
            Url = url;
            ContentType = contentType;
        }
        public string Url { get; set; }
        public string ContentType { get; set; }
        public T Send(string data)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpRequest.Method = "POST";
            httpRequest.Accept = httpRequest.ContentType = ContentType;
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                streamWriter.Write(data);
            using (var streamReader = new StreamReader(((HttpWebResponse)httpRequest.GetResponse()).GetResponseStream()))
                return JsonConvert.DeserializeObject<T>(streamReader.ReadToEnd());
        }
    }
    private readonly CookieContainer cookieContainer = new();
    private string GetResponse(string url)
    {
        string site = url.Split('/')[2];
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.CookieContainer = cookieContainer;
        request.Accept = @"text/html, application/xhtml+xml, */*";
        request.Referer = site;
        request.Headers.Add("Accept-Language", "en-GB");
        request.UserAgent = @"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)";
        request.Host = site;
        return new StreamReader(((HttpWebResponse)request.GetResponse()).GetResponseStream()).ReadToEnd();
    }
    private string DecodeBase64(string base64) => Encoding.UTF8.GetString(Convert.FromBase64String(base64));
    #endregion
    public BaseUser[] UsernamesToUUID(params string[] nicknames) => new PostRequest<BaseUser[]>("https://api.mojang.com/profiles/minecraft").Send(JsonConvert.SerializeObject(nicknames));
    public NameHistoryUnit[] GetNameHistoryByUUID(string uuid) => JsonConvert.DeserializeObject<NameHistoryUnit[]>(GetResponse($"https://api.mojang.com/user/profiles/<{uuid}>/names"));
    public ModelProfile GetModelProfileByUUID(string uuid)
    {
        RawModelProfile rawModel = JsonConvert.DeserializeObject<RawModelProfile>(GetResponse($"https://sessionserver.mojang.com/session/minecraft/profile/{uuid}"));
        RawSkinData rawSkin = JsonConvert.DeserializeObject<RawSkinData>(DecodeBase64(rawModel.SkinData[0].Value));
        return new ModelProfile()
        {
            UUID = uuid,
            Name = rawSkin.Name,
            SignatureRequired = rawSkin.SignatureRequired,
            TimeStamp = new DateTime(1970, 1, 1) + TimeSpan.FromMilliseconds(double.Parse(rawSkin.TimeStamp.ToString())),
            Skin = rawSkin.SkinTextures.Skin.Url,
            Cape = rawSkin.SkinTextures.Cape.Url,
        };
    }
    public string[] GetUUIDBlockedServers() => GetResponse("https://sessionserver.mojang.com/blockedservers").Split('\n');
    public BaseUser GetUserByName(string name) => JsonConvert.DeserializeObject<BaseUser>(GetResponse("https://api.mojang.com/users/profiles/minecraft/" + name));
}