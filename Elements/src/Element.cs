using System;

namespace Elements
{
    /// <summary>
    /// An object which is identified with a unique identifier and a name.
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(Elements.Serialization.JSON.JsonInheritanceConverter), "discriminator")]
    public abstract class Element : System.ComponentModel.INotifyPropertyChanged
    {
        private System.Guid _id;
        private string _name;

        /// <summary>
        /// Construct an element.
        /// </summary>
        /// <param name="id">The unique id of the element.</param>
        /// <param name="name">The name of the element.</param>
        [Newtonsoft.Json.JsonConstructor]
        public Element(System.Guid @id, string @name)
        {
            this.Id = @id;
            this.Name = @name;

            if (Id == default(Guid))
            {
                Id = System.Guid.NewGuid();
            }
        }

        /// <summary>A unique id.</summary>
        [Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public System.Guid Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>A name.</summary>
        [Newtonsoft.Json.JsonProperty("Name", Required = Newtonsoft.Json.Required.AllowNull)]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged();
                }
            }
        }

        private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

        /// <summary>
        /// A collection of additional properties.
        /// </summary>
        [Newtonsoft.Json.JsonExtensionData]
        public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }

        /// <summary>
        /// An event raised when a property is changed.
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise a property change event.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        protected virtual void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

    }
}