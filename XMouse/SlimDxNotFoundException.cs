using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable()]
class SlimDxNotFoundException : System.Exception
{
    public SlimDxNotFoundException() : base() { }
    public SlimDxNotFoundException(string message) : base(message) { }
    public SlimDxNotFoundException(string message, System.Exception inner) : base(message, inner) { }

    protected SlimDxNotFoundException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) { }
}