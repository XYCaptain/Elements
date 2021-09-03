using System.Collections.Generic;
using Newtonsoft.Json;

namespace Elements.Geometry
{
    /// <summary>
    /// A mesh vertex.
    /// </summary>
    public class Vertex
    {
        /// <summary>The vertex's position.</summary>
        [Newtonsoft.Json.JsonProperty("Position", Required = Newtonsoft.Json.Required.AllowNull)]
        public Vector3 Position { get; set; }

        /// <summary>The vertex's normal.</summary>
        [Newtonsoft.Json.JsonProperty("Normal", Required = Newtonsoft.Json.Required.AllowNull)]
        public Vector3 Normal { get; set; }

        /// <summary>The vertex's color.</summary>
        [Newtonsoft.Json.JsonProperty("Color", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public Color Color { get; set; } = new Color();

        /// <summary>The index of the vertex within a mesh.</summary>
        [Newtonsoft.Json.JsonProperty("Index", Required = Newtonsoft.Json.Required.Always)]
        public int Index { get; set; }

        /// <summary>The vertex's texture coordinate.</summary>
        [Newtonsoft.Json.JsonProperty("UV", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public UV UV { get; set; } = new UV();

        /// <summary>
        /// Construct a vertex.
        /// </summary>
        /// <param name="position">The position of the vertex.</param>
        /// <param name="normal">The normal of the vertex.</param>
        /// <param name="color">The color of the vertex.</param>
        /// <param name="index">The index of the vertex.</param>
        /// <param name="uv">The uv coordinate of the vertex.</param>
        [Newtonsoft.Json.JsonConstructor]
        public Vertex(Vector3 @position, Vector3 @normal, Color @color, int @index, UV @uv)
        {
            this.Position = @position;
            this.Normal = @normal;
            this.Color = @color;
            this.Index = @index;
            this.UV = @uv;
        }

        // The associated triangles are not defined on the 
        // auto-generated vertex class because doing so causes a circular reference
        // during serialization.

        /// <summary>The triangles associated with this vertex.</summary>
        [Newtonsoft.Json.JsonProperty("Triangles", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [JsonIgnore]
        public IList<Triangle> Triangles { get; set; } = new List<Triangle>();

        /// <summary>
        /// Create a vertex.
        /// </summary>
        /// <param name="position">The position of the vertex.</param>
        /// <param name="normal">The vertex's normal.</param>
        /// <param name="color">The vertex's color.</param>
        public Vertex(Vector3 position, Vector3? normal = null, Color color = default(Color))
        {
            this.Position = position;
            this.Normal = normal ?? Vector3.Origin;
            this.Color = color;
        }
    }
}