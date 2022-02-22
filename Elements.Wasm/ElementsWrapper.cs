using System.Linq;
using System.Threading.Tasks;
using Elements;
using Elements.Geometry;
using Elements.Serialization.glTF;
using Elements.Spatial;
using Elements.Validators;
using Microsoft.JSInterop;
using System.Diagnostics;
using System.Text;

public static class ElementsAPI
{
    [JSInvokable]
    public static Task<Model> ModelFromJson(string json)
    {
        return Task.FromResult(Model.FromJsonNew(json));
    }

    [JSInvokable]
    public static Task<string> ModelToGlbBase64(string json)
    {
        Validator.DisableValidationOnConstruction = true;

        var model = Model.FromJsonNew(json);
        return Task.FromResult(model.ToBase64String());
    }

    [JSInvokable]
    public static Task<TestResult> Test()
    {
        Validator.DisableValidationOnConstruction = true;

        var sw = new Stopwatch();
        sw.Start();
        var sb = new StringBuilder();

        var model = new Model();
        var r = new Random();
        var size = 10;
        var profile = Polygon.L(0.1, 0.1, 0.05);
        for (var i = 0; i < 100; i++)
        {
            var start = new Vector3(r.NextDouble() * size, r.NextDouble() * size, r.NextDouble() * size);
            var end = new Vector3(r.NextDouble() * size, r.NextDouble() * size, r.NextDouble() * size);
            var line = new Line(start, end);
            var beam = new Beam(line, profile);
            model.AddElement(beam);
        }
        sb.AppendLine($"{sw.ElapsedMilliseconds}ms for creating test beams.");
        sw.Restart();

        var json = model.ToJsonNew();
        sb.AppendLine($"{sw.ElapsedMilliseconds}ms for serializing model.");
        sw.Restart();

        var newModel = Model.FromJsonNew(json);
        sb.AppendLine($"{sw.ElapsedMilliseconds}ms for deserializing model.");
        sw.Restart();

        var result = newModel.ToGlTF();
        sb.AppendLine($"{sw.ElapsedMilliseconds}ms for creating the glb.");
        return Task.FromResult<TestResult>(new TestResult()
        {
            Glb = result,
            Results = sb.ToString()
        });
    }

    public class TestResult
    {
        public byte[] Glb { get; set; }
        public string Results { get; set; }
    }

    [JSInvokable]
    public static Task<byte[]> ModelToGlbBytes(string json)
    {
        Validator.DisableValidationOnConstruction = true;

        var sw = new Stopwatch();
        sw.Start();
        var model = Model.FromJsonNew(json);
        Console.WriteLine($"{sw.ElapsedMilliseconds}ms for creating the model from json.");
        sw.Restart();
        var result = Task.FromResult(model.ToGlTF());
        Console.WriteLine($"{sw.ElapsedMilliseconds}ms for creating the glb from json.");
        return result;
    }
}
