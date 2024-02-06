//----------------------
// <auto-generated>
//     Generated using the NJsonSchema v10.1.21.0 (Newtonsoft.Json v13.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------
using Elements;
using Elements.GeoJSON;
using Elements.Geometry;
using Elements.Geometry.Solids;
using Elements.Spatial;
using Elements.Validators;
using Elements.Serialization.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Line = Elements.Geometry.Line;
using Polygon = Elements.Geometry.Polygon;

namespace Elements.Fittings
{
    #pragma warning disable // Disable all warnings

    /// <summary>A network of connected pipe segments</summary>
    [JsonConverter(typeof(Elements.Serialization.JSON.JsonInheritanceConverter), "discriminator")]
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.21.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class FittingTree : Element
    {
        [JsonConstructor]
        public FittingTree(IList<StraightSegment> @straightSegments, IList<Fitting> @fittings, string @purpose, System.Guid @id = default, string @name = null)
            : base(id, name)
        {
            this.StraightSegments = @straightSegments;
            this.Fittings = @fittings;
            this.Purpose = @purpose;
            }
        
        // Empty constructor
        public FittingTree()
            : base()
        {
        }
    
        /// <summary>The segments in the network.</summary>
        [JsonProperty("StraightSegments", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public IList<StraightSegment> StraightSegments { get; set; }
    
        /// <summary>The fittings in this network.</summary>
        [JsonProperty("Fittings", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public IList<Fitting> Fittings { get; set; }
    
        /// <summary>The purpose of the fitting system.</summary>
        [JsonProperty("Purpose", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Purpose { get; set; }
    
    
    }
}