using SlothCodeAnalysis.InternalUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis
{
    public partial class ConstantValue
    {
        // base for constant classes that may represent more than one 
        // constant type
        private abstract class ConstantValueDiscriminated : ConstantValue
        {
            private readonly ConstantValueTypeDiscriminator _discriminator;

            public ConstantValueDiscriminated(ConstantValueTypeDiscriminator discriminator)
            {
                _discriminator = discriminator;
            }

            public override ConstantValueTypeDiscriminator Discriminator
            {
                get
                {
                    return _discriminator;
                }
            }

            internal override SpecialType SpecialType
            {
                get { return GetSpecialType(_discriminator); }
            }
        }

        private class ConstantValueOne : ConstantValueDiscriminated
        {
            public static readonly ConstantValueOne Int32 = new ConstantValueOne(ConstantValueTypeDiscriminator.Int32);

            protected ConstantValueOne(ConstantValueTypeDiscriminator discriminator)
                : base(discriminator)
            {
            }


            // all instances of this class are singletons
            public override bool Equals(ConstantValue other)
            {
                return ReferenceEquals(this, other);
            }

            public override int GetHashCode()
            {
                return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(this);
            }
        }

        private sealed class ConstantValueString : ConstantValue
        {
            private readonly string _value;

            public ConstantValueString(string value)
            {
                // we should have just one Null regardless string or object.
                System.Diagnostics.Debug.Assert(value != null, "null strings should be represented as Null constant.");
                _value = value;
            }

            public override ConstantValueTypeDiscriminator Discriminator
            {
                get
                {
                    return ConstantValueTypeDiscriminator.String;
                }
            }

            internal override SpecialType SpecialType
            {
                get { return SpecialType.System_String; }
            }

            public override string StringValue
            {
                get
                {
                    return _value;
                }
            }

            public override int GetHashCode()
            {
                return Hash.Combine(base.GetHashCode(), _value.GetHashCode());
            }

            public override bool Equals(ConstantValue other)
            {
                return base.Equals(other) && _value == other.StringValue;
            }

            internal override string GetValueToDisplay()
            {
                return (_value == null) ? "null" : string.Format("\"{0}\"", _value);
            }
        }

        // default value of a value type constant. (reference type constants use Null as default)
        private class ConstantValueDefault : ConstantValueDiscriminated
        {
            public static readonly ConstantValueDefault Int32 = new ConstantValueDefault(ConstantValueTypeDiscriminator.Int32);

            protected ConstantValueDefault(ConstantValueTypeDiscriminator discriminator)
                : base(discriminator)
            {
            }

            // all instances of this class are singletons
            public override bool Equals(ConstantValue other)
            {
                return ReferenceEquals(this, other);
            }

            public override int GetHashCode()
            {
                return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(this);
            }

            public override bool IsDefaultValue
            {
                get { return true; }
            }
        }

        private sealed class ConstantValueI32 : ConstantValueDiscriminated
        {
            private readonly int _value;

            public ConstantValueI32(int value)
                : base(ConstantValueTypeDiscriminator.Int32)
            {
                _value = value;
            }

            public override int Int32Value
            {
                get
                {
                    return _value;
                }
            }

            public override int GetHashCode()
            {
                return Hash.Combine(base.GetHashCode(), _value.GetHashCode());
            }

            public override bool Equals(ConstantValue other)
            {
                return base.Equals(other) && _value == other.Int32Value;
            }
        }
    }
}
