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

    /// <summary>A connectable collection of pipes and connections</summary>
    [JsonConverter(typeof(Elements.Serialization.JSON.JsonInheritanceConverter), "discriminator")]
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.21.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class Assembly : Fitting
    {
        [JsonConstructor]
        public Assembly(IList<Port> @externalPorts, IList<Fitting> @internalFittings, IList<StraightSegment> @internalSegments, bool @canBeMoved, FittingLocator @componentLocator, Transform @transform, Material @material, Representation @representation, bool @isElementDefinition, System.Guid @id, string @name)
            : base(canBeMoved, componentLocator, transform, material, representation, isElementDefinition, id, name)
        {
            this.ExternalPorts = @externalPorts;
            this.InternalFittings = @internalFittings;
            this.InternalSegments = @internalSegments;
            }
        
        // Empty constructor
        public Assembly()
            : base()
        {
        }
    
        /// <summary>The external facing Ports</summary>
        [JsonProperty("ExternalPorts", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public IList<Port> ExternalPorts { get; set; }
    
        [JsonProperty("InternalFittings", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public IList<Fitting> InternalFittings { get; set; }
    
        /// <summary>The internal segments in the assembly.</summary>
        [JsonProperty("InternalSegments", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public IList<StraightSegment> InternalSegments { get; set; }
    
    
    }
}