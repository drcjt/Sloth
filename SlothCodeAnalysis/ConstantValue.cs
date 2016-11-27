using SlothCodeAnalysis.Collections;
using SlothCodeAnalysis.InternalUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis
{
    public enum ConstantValueTypeDiscriminator : byte
    {
        Nothing,
        Bad,
        Int32,
        String,
    }

    public abstract partial class ConstantValue : IEquatable<ConstantValue>
    {
        public abstract ConstantValueTypeDiscriminator Discriminator { get; }
        internal abstract SpecialType SpecialType { get; }

        public virtual string StringValue { get { throw new InvalidOperationException(); } }
        public virtual int Int32Value { get { throw new InvalidOperationException(); } }

        // returns true if value is in its default (zero-inited) form.
        public virtual bool IsDefaultValue { get { return false; } }

        public static ConstantValue Create(string value)
        {
            if (value == null)
            {
                throw ExceptionUtilities.UnexpectedValue(value);
            }

            return new ConstantValueString(value);
        }

        public static ConstantValue Create(Int32 value)
        {
            if (value == 0)
            {
                return ConstantValueDefault.Int32;
            }
            else if (value == 1)
            {
                return ConstantValueOne.Int32;
            }

            return new ConstantValueI32(value);
        }

        public static ConstantValue Create(object value, ConstantValueTypeDiscriminator discriminator)
        {
            switch (discriminator)
            {
                case ConstantValueTypeDiscriminator.Int32: return Create((int)value);
                case ConstantValueTypeDiscriminator.String: return Create((string)value);
                default:
                    throw new InvalidOperationException();  //Not using ExceptionUtilities.UnexpectedValue() because this failure path is tested.
            }
        }

        public static ConstantValue Create(object value, SpecialType st)
        {
            var discriminator = GetDiscriminator(st);
            return Create(value, discriminator);
        }

        internal static ConstantValueTypeDiscriminator GetDiscriminator(SpecialType st)
        {
            switch (st)
            {
                case SpecialType.System_Int32: return ConstantValueTypeDiscriminator.Int32;
                case SpecialType.System_String: return ConstantValueTypeDiscriminator.String;
            }

            return ConstantValueTypeDiscriminator.Bad;
        }

        private static SpecialType GetSpecialType(ConstantValueTypeDiscriminator discriminator)
        {
            switch (discriminator)
            {
                case ConstantValueTypeDiscriminator.Int32: return SpecialType.System_Int32;
                case ConstantValueTypeDiscriminator.String: return SpecialType.System_String;
                default: return SpecialType.None;
            }
        }

        public object Value
        {
            get
            {
                switch (this.Discriminator)
                {
                    case ConstantValueTypeDiscriminator.Int32: return Boxes.Box(Int32Value);
                    case ConstantValueTypeDiscriminator.String: return StringValue;
                    default: throw ExceptionUtilities.UnexpectedValue(this.Discriminator);
                }
            }
        }

        public bool IsNumeric
        {
            get
            {
                switch (this.Discriminator)
                {
                    case ConstantValueTypeDiscriminator.Int32:
                        return true;

                    default:
                        return false;
                }
            }
        }

        public bool IsString
        {
            get
            {
                return this.Discriminator == ConstantValueTypeDiscriminator.String;
            }
        }

        public override string ToString()
        {
            string valueToDisplay = this.GetValueToDisplay();
            return String.Format("{0}({1}: {2})", this.GetType().Name, valueToDisplay, this.Discriminator);
        }

        internal virtual string GetValueToDisplay()
        {
            return this.Value.ToString();
        }

        // equal constants must have matching discriminators
        // derived types override this if equivalence is more than just discriminators match. 
        // singletons also override this since they only need a reference compare.
        public virtual bool Equals(ConstantValue other)
        {
            if (ReferenceEquals(other, this))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return this.Discriminator == other.Discriminator;
        }

        public static bool operator ==(ConstantValue left, ConstantValue right)
        {
            if (ReferenceEquals(right, left))
            {
                return true;
            }

            if (ReferenceEquals(left, null))
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(ConstantValue left, ConstantValue right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.Discriminator.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ConstantValue);
        }
    }
}
