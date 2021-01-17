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

namespace Elements
{
    #pragma warning disable // Disable all warnings

    /// <summary>A material.</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.21.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class Material : Element
    {
        [Newtonsoft.Json.JsonConstructor]
        public Material(Color @color, double @specularFactor, double @glossinessFactor, bool @unlit, string @texture, bool @doubleSided, System.Guid @id, string @name)
            : base(id, name)
        {
            var validator = Validator.Instance.GetFirstValidatorForType<Material>();
            if(validator != null)
            {
                validator.PreConstruct(new object[]{ @color, @specularFactor, @glossinessFactor, @unlit, @texture, @doubleSided, @id, @name});
            }
        
            this.Color = @color;
            this.SpecularFactor = @specularFactor;
            this.GlossinessFactor = @glossinessFactor;
            this.Unlit = @unlit;
            this.Texture = @texture;
            this.DoubleSided = @doubleSided;
            
            if(validator != null)
            {
                validator.PostConstruct(this);
            }
        }
    
        /// <summary>The material's color.</summary>
        [Newtonsoft.Json.JsonProperty("Color", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public Color Color { get; set; } = new Color();
    
        /// <summary>The specular factor between 0.0 and 1.0.</summary>
        [Newtonsoft.Json.JsonProperty("SpecularFactor", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Range(0.0D, 1.0D)]
        public double SpecularFactor { get; set; } = 0.1D;
    
        /// <summary>The glossiness factor between 0.0 and 1.0.</summary>
        [Newtonsoft.Json.JsonProperty("GlossinessFactor", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Range(0.0D, 1.0D)]
        public double GlossinessFactor { get; set; } = 0.1D;
    
        /// <summary>Is this material affected by lights?</summary>
        [Newtonsoft.Json.JsonProperty("Unlit", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool Unlit { get; set; } = false;
    
        /// <summary>A relative file path to an image file to be used as a texture.</summary>
        [Newtonsoft.Json.JsonProperty("Texture", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Texture { get; set; }
    
        /// <summary>Is this material to be rendered from both sides?</summary>
        [Newtonsoft.Json.JsonProperty("DoubleSided", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool DoubleSided { get; set; } = false;
    
    
    }
}