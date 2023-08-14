using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDemo.GetHashCode
{
    public class PersonRef
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;

            PersonRef other = obj as PersonRef;
            if (other == null) return false;

            return Equals(this.Name, other.Name)
                && Equals(this.Email, other.Email)
                && this.Age == other.Age;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Email, Age);
        }
    }
}
