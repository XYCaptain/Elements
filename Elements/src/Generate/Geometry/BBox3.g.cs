//----------------------
// <auto-generated>
//     Generated using the NJsonSchema v10.1.21.0 (Newtonsoft.Json v11.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------
using Elements;
using Elements.GeoJSON;
using Elements.Geometry;
using Elements.Geometry.Solids;
using Elements.Validators;
using Elements.Serialization.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using Line = Elements.Geometry.Line;
using Polygon = Elements.Geometry.Polygon;

namespace Elements.Geometry
{
    #pragma warning disable // Disable all warnings

    /// <summary>An axis-aligned bounding box.</summary>
    [Newtonsoft.Json.JsonConverter(typeof(Elements.Serialization.JSON.JsonInheritanceConverter), "discriminator")]
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.21.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial struct BBox3 
    {
        [Newtonsoft.Json.JsonConstructor]
        public BBox3(Vector3 @min, Vector3 @max)
        {
            var validator = Validator.Instance.GetFirstValidatorForType<BBox3>();
            if(validator != null)
            {
                validator.PreConstruct(new object[]{ @min, @max});
            }
        
            this.Min = @min;
            this.Max = @max;
            
            if(validator != null)
            {
                validator.PostConstruct(this);
            }
        }
    
        /// <summary>The minimum extent of the bounding box.</summary>
        [Newtonsoft.Json.JsonProperty("Min", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Vector3 Min { get; set; }
    
        /// <summary>The maximum extent of the bounding box.</summary>
        [Newtonsoft.Json.JsonProperty("Max", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Vector3 Max { get; set; }
    
    
    }
}