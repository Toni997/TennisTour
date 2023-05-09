using System.Text;
using Newtonsoft.Json;

namespace TennisTour.Frontend.Helpers;

public class JsonNewtonContent : StringContent
{
    public JsonNewtonContent(object obj) : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json") { }
}
