using Elements.Geometry;
using Elements.Geometry.Solids;
using Elements.Tests;
using Newtonsoft.Json;
using SixLabors.ImageSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Xunit;
using Xunit.Abstractions;
using static Antlr4.Runtime.Atn.SemanticContext;

namespace Elements
{
	public class RebarTests : ModelTest
	{
		private readonly ITestOutputHelper output;
		public RebarTests(ITestOutputHelper output)
		{
			this.output = output;
			this.GenerateIfc = false;

			if (!Directory.Exists("models"))
			{
				Directory.CreateDirectory("models");
			}
		}

		[Fact]
		public void Rebar()
		{
			this.Name = "Rebar_Test";
			var line1 = new Line(new Vector3(0, 0, 0), new Vector3(100, 0, 0));
			var line2 = new Line(new Vector3(100, 0, 0), new Vector3(100, 0, 100));
			var arc = line1.Fillet(line2, 50);
			if (arc.Start.DistanceTo(line1).ApproximatelyEquals(0))
			{
				this.Model.AddElement(new ModelCurve(new Line(line1.Start, arc.Start), material: BuiltInMaterials.Black));
			}
			if (arc.End.DistanceTo(line1).ApproximatelyEquals(0))
			{
				this.Model.AddElement(new ModelCurve(new Line(line1.Start, arc.End), material: BuiltInMaterials.Black));
			}
			this.Model.AddElement(new ModelCurve(arc, material: BuiltInMaterials.Black));
			if (arc.Start.DistanceTo(line2).ApproximatelyEquals(0))
			{
				this.Model.AddElement(new ModelCurve(new Line(arc.Start, line2.End), material: BuiltInMaterials.Black));
			}
			if (arc.End.DistanceTo(line2).ApproximatelyEquals(0))
			{
				this.Model.AddElement(new ModelCurve(new Line(arc.End, line2.End), material: BuiltInMaterials.Black));
			}
		}

		[Fact]
		public void RedJson()
		{
			this.Name = "Rebar_Json";
			this.GenerateIfc = true;
			var isCurves = true;

			var jstr = System.IO.File.ReadAllText(@"C:\Users\Liuxinyu\Documents\WeChat Files\liuxinyu985\FileStorage\File\2023-07\铁设数据1.json");
			var jsonObj = JsonConvert.DeserializeObject<InputJson>(jstr);

			foreach (var path in jsonObj.RebarLineCoordinates)
			{
				var pl = new Polyline(path.Select(x => new Vector3(x.X / 1000d, x.Y / 1000d, x.Z / 1000d)).ToArray());
				var outer = new Circle(new Transform() { Matrix = new Matrix() { } }, 1).ToPolygon();
				var solid = Solid.SweepFaceAlongCurve(outer, null, pl);
				var rep = new Representation(new List<SolidOperation>() { new ConstructedSolid(solid) });
				var solidElement = new GeometricElement(representation: rep, isElementDefinition: false);

				this.Model.AddElement(solidElement);
				var color = new Color(Random.Shared.NextDouble(), Random.Shared.NextDouble(), Random.Shared.NextDouble(), 1);
				this.Model.AddElement(new ModelCurve(pl, new Material(Guid.NewGuid().ToString(), color)));
			}
			var o = this.Model.Origin;
		}

		[Fact]
		public void RedXML()
		{
			this.Name = "Rebar_xml";
			this.GenerateIfc = true;
			var isCurves = false;
			XMLHelper xml = new XMLHelper();
			xml.Open(@"C:\Users\Liuxinyu\Documents\WeChat Files\liuxinyu985\FileStorage\File\2023-07\CQ-4.6fangpoduan(DK115+550-DK115+630) A.1_RebarPathInfo.xml");
			xml.Ref();

			Dictionary<string, GeometricElement> refeles = new Dictionary<string, GeometricElement>();
			Dictionary<string, Polyline> refcurs = new Dictionary<string, Polyline>();

			foreach (var rebar in xml.RebarRefs)
			{
				RebarRef rebarRef = new RebarRef(rebar.Value);

				var vs = new List<Vector3>();
				Vector3? p = null;
				for (int i = 0; i < rebarRef.Segs.Count; i++)
				{
					if (rebarRef.Segs[i] is Line)
					{
						vs.Add(rebarRef.Segs[i].Start);
						vs.Add(rebarRef.Segs[i].End);
					}
					else
					{
						var pts = rebarRef.Segs[i].ToPolyline().Vertices.ToList();
						if (p != null && !p.Value.Equals(rebarRef.Segs[i].Start))
							pts.Reverse();
						vs.AddRange(pts);
					}

					p = vs.Last();
				}

				vs = vs.Distinct().ToList();
				var path = new Polyline(vs);
				var outer = new Circle(new Transform() { Matrix = new Matrix() { } }, xml.RebarInfos[rebar.Key].Diameter / 2).ToPolygon();
				var solid = Solid.SweepFaceAlongCurve(outer, null, path);

				var rep = new Representation(new List<SolidOperation>() { new ConstructedSolid(solid) });
				var solidElement = new GeometricElement(representation: rep, isElementDefinition: true, name: xml.RebarInfos[rebar.Key].Name);
				//var modelcurrencies = new ModelCurve(path, transform: new Transform(), material: BuiltInMaterials.Black, isElementDefinition: true);
				refcurs.Add(rebar.Key, path);
				refeles.Add(rebar.Key, solidElement);
			}

			foreach (var item in xml.RebarIns)
			{
				foreach (var ins in item.Value)
				{
					if (isCurves)
					{
						this.Model.AddElement(new ModelCurve(refcurs[ins.RebarRefName], transform: new Transform(ins.Matrix), material: BuiltInMaterials.Black));
					}
					else
					{
						var instance = refeles[ins.RebarRefName].CreateInstance(new Transform(ins.Matrix), name: ins.Name);
						this.Model.AddElement(instance);
					}

				}
			}
			var t = new Matrix();
			t.SetIdentity();
			t.SetTranslation(xml.RebarIns.First().Value.First().Matrix.Translation * -1);
			this.Model.Transform = new Transform(matrix: t);
		}
	}

	internal class RebarRef
	{
		public List<BoundedCurve> Segs = new List<BoundedCurve>();

		public RebarRef(List<InputRef> inputRefs)
		{
			var vs = inputRefs.OrderBy(x => x.Index).Select(x => x.Vector);
			List<Line> Lines = new List<Line>();

			for (int i = 0; i < inputRefs.Count; i++)
			{
				var star = inputRefs[i].Vector;

				if (inputRefs[i].bugle == 0 && i + 1 < inputRefs.Count)
				{
					var end = inputRefs[i + 1].Vector;
					if (star.Equals(end))
						continue;
					var line = new Line(star, end);
					Lines.Add(line);


					if (Lines.Count == 2)
					{
						if (!Lines[0].Direction().IsParallelTo(Lines[1].Direction()) && !Lines[0].End.Equals(Lines[1].Start))
						{
							var plane = new Plane(Lines[0].Start, Lines[0].End, Lines[1].End);
							var l1 = Lines[0].Projected(plane);
							var l2 = Lines[1].Projected(plane);

							var pnormal = plane.Normal;

							var l1normal = Lines[0].Direction().Cross(pnormal);
							var l2normal = Lines[1].Direction().Cross(pnormal);

							var line1 = new Line(Lines[0].End, Lines[0].End + l1normal);
							var line2 = new Line(Lines[1].Start, Lines[1].Start + l2normal);


							line1.Intersects(line2, out var center, true);
							var radius = (center - line1.Start).Length();
							if (radius == 0)
								throw new Exception("半径是0");
							var arc0 = Lines[0].Fillet(Lines[1], radius);
							//if (arc0.Start.Equals(Lines[0].End))
							//	arc0.Reversed();
							Segs.Add(arc0);
						}
						else
						{
							break;
						}
						Lines.Remove(Lines[0]);
					}

					if (Lines.Count == 1)
						Segs.Add(Lines[0]);
				}
			}
		}

		public double CalBulgeRadius(Vector3 point1, Vector3 point2, double bulge)
		{
			//计算顶点角度
			double cicleAngle = Math.Atan(bulge) * 4;
			//两点之间的距离
			double pointLen = (point1 - point2).Length();
			//根据正玄值反推
			double radius = (pointLen / 2) / Math.Sin(cicleAngle / 2);
			return Math.Abs(radius);
		}
	}

	internal class RebarIns
	{

	}

	internal class XMLHelper
	{
		XmlDocument doc;
		public Dictionary<string, List<InputRef>> RebarRefs = new Dictionary<string, List<InputRef>>();
		public Dictionary<string, List<InputIns>> RebarIns = new Dictionary<string, List<InputIns>>();
		public Dictionary<string, Rebar> RebarInfos = new Dictionary<string, Rebar>();

		public void Open(string path)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			doc = new XmlDocument();
			doc.Load(path);
		}

		public void Ref()
		{
			var nodes = doc.GetElementsByTagName("RebarRefPtsNode");
			foreach (XmlNode item in nodes)
			{
				if (!RebarRefs.ContainsKey(item.Attributes["RebarName"].Value))
				{
					RebarRefs.Add(item.Attributes["RebarName"].Value, new List<InputRef>());
				}
				RebarRefs[item.Attributes["RebarName"].Value].Add(new InputRef()
				{
					Index = short.Parse(item.Attributes["PtIndex"].Value),
					Vector = new Vector3(
						double.Parse(item.Attributes["x"].Value),
						double.Parse(item.Attributes["y"].Value),
						double.Parse(item.Attributes["z"].Value)),
					Orient = new Vector3(
						double.Parse(item.Attributes["dx"].Value),
						double.Parse(item.Attributes["dy"].Value),
						double.Parse(item.Attributes["dz"].Value)),
					bugle = double.Parse(item.Attributes["bugle"].Value),
				});


			}

			var instances = doc.GetElementsByTagName("RebarIns");
			foreach (XmlNode item in instances)
			{
				if (!RebarIns.ContainsKey(item.Attributes["Name"].Value))
				{
					RebarIns.Add(item.Attributes["Name"].Value, new List<InputIns>());
				}

				RebarIns[item.Attributes["Name"].Value].Add(new InputIns()
				{
					//Matrix = new Matrix(new double[] {
					//	double.Parse(item.Attributes["MatrixIndex0"].Value),
					//	double.Parse(item.Attributes["MatrixIndex1"].Value),
					//	double.Parse(item.Attributes["MatrixIndex2"].Value),
					//	double.Parse(item.Attributes["MatrixIndex12"].Value),
					//	double.Parse(item.Attributes["MatrixIndex4"].Value),
					//	double.Parse(item.Attributes["MatrixIndex5"].Value),
					//	double.Parse(item.Attributes["MatrixIndex6"].Value),
					//	double.Parse(item.Attributes["MatrixIndex13"].Value),
					//	double.Parse(item.Attributes["MatrixIndex8"].Value),
					//	double.Parse(item.Attributes["MatrixIndex9"].Value),
					//	double.Parse(item.Attributes["MatrixIndex10"].Value),
					//	double.Parse(item.Attributes["MatrixIndex14"].Value),
					//}),
					Matrix = new Matrix(new double[] {
						double.Parse(item.Attributes["MatrixIndex0"].Value),
						double.Parse(item.Attributes["MatrixIndex4"].Value),
						double.Parse(item.Attributes["MatrixIndex8"].Value),
						double.Parse(item.Attributes["MatrixIndex12"].Value),
						double.Parse(item.Attributes["MatrixIndex1"].Value),
						double.Parse(item.Attributes["MatrixIndex5"].Value),
						double.Parse(item.Attributes["MatrixIndex9"].Value),
						double.Parse(item.Attributes["MatrixIndex13"].Value),
						double.Parse(item.Attributes["MatrixIndex2"].Value),
						double.Parse(item.Attributes["MatrixIndex6"].Value),
						double.Parse(item.Attributes["MatrixIndex10"].Value),
						double.Parse(item.Attributes["MatrixIndex14"].Value),
					}),
					RebarRefName = item.Attributes["RebarRefName"].Value,
					Name = item.Attributes["Name"].Value
				});
			}

			var rebarinfos = doc.GetElementsByTagName("Rebar");

			foreach (XmlNode item in rebarinfos)
			{
				if (!RebarInfos.ContainsKey(item.Attributes["Name"].Value))
				{
					try
					{
						RebarInfos.Add(item.Attributes["Name"].Value, new Rebar
						{
							Name = item.Attributes["名称"].Value,
							Diameter = double.Parse(item.Attributes["钢筋直径"].Value.Replace("m", "")) * 1000
						});
					}
					catch (Exception)
					{

						throw;
					}
				}
			}
		}
	}

	internal class Rebar
	{
		public string Name { get; set; }
		public double Diameter { get; set; }
	}

	internal class InputRef
	{
		public short Index { get; set; }
		public Vector3 Vector { get; set; }
		public Vector3 Orient { get; set; }
		public double bugle { get; set; }
	}

	internal class InputIns
	{
		public string Name { get; set; }
		public string RebarRefName { get; set; }
		public Matrix Matrix { get; set; }
	}

	internal class InputJson
	{
		public double Diameter { get; set; }
		public object RebarProperties { get; set; }
		public string Name { get; set; }
		public List<Vector3[]> RebarLineCoordinates { get; set; }
	}
}
