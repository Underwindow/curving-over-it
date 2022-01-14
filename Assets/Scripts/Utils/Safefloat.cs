using System;

public struct SafeFloat
{
    private float offset;
    private float value;

    public SafeFloat(float value = 0)
    {
        offset = UnityEngine.Random.Range(-1000, +1000);
        this.value = value + offset;
    }

    public float GetValue()
    {
        return value - offset;
    }

    public void Dispose()
    {
        offset = 0;
        value = 0;
    }

    public override string ToString()
    {
        return GetValue().ToString();
    }

    public override bool Equals(object obj)
    {
        return obj is SafeFloat @float &&
               offset == @float.offset &&
               value == @float.value;
    }

    public override int GetHashCode()
    {
        int hashCode = -924004060;
        hashCode = hashCode * -1521134295 + offset.GetHashCode();
        hashCode = hashCode * -1521134295 + value.GetHashCode();
        return hashCode;
    }

    public static implicit operator SafeFloat(float f)
    {
        return new SafeFloat(f);
    }

    public static explicit operator float(SafeFloat sf)
    {
        return sf.GetValue();
    }

    public static implicit operator SafeFloat(int i)
    {
        return new SafeFloat(i);
    }

    public static explicit operator int(SafeFloat sf)
    {
        return (int)sf.GetValue();
    }

    public static implicit operator SafeFloat(uint i)
    {
        return new SafeFloat(i);
    }

    public static explicit operator uint(SafeFloat sf)
    {
        return (uint)sf.GetValue();
    }

    public static SafeFloat operator +(SafeFloat f1, SafeFloat f2)
    {
        return new SafeFloat(f1.GetValue() + f2.GetValue());
    }

    public static SafeFloat operator -(SafeFloat f1, SafeFloat f2)
    {
        return new SafeFloat(f1.GetValue() - f2.GetValue());
    }

    public static SafeFloat operator *(SafeFloat f1, SafeFloat f2)
    {
        return new SafeFloat(f1.GetValue() * f2.GetValue());
    }

    public static SafeFloat operator /(SafeFloat f1, SafeFloat f2)
    {
        if (f2.GetValue() == 0)
            throw new DivideByZeroException();
        
        return new SafeFloat(f1.GetValue() / f2.GetValue());
    }

    public static bool operator >(SafeFloat f1, SafeFloat f2)
    {
        return f1.GetValue() > f2.GetValue();
    }
    public static bool operator <(SafeFloat f1, SafeFloat f2)
    {
        return f1.GetValue() < f2.GetValue();
    }
    public static bool operator ==(SafeFloat f1, SafeFloat f2)
    {
        return f1.GetValue() == f2.GetValue();
    }

    public static bool operator !=(SafeFloat f1, SafeFloat f2)
    {
        return f1.GetValue() != f2.GetValue();
    }

    public static bool operator >=(SafeFloat f1, SafeFloat f2)
    {
        return f1.GetValue() >= f2.GetValue();
    }
    public static bool operator <=(SafeFloat f1, SafeFloat f2)
    {
        return f1.GetValue() <= f2.GetValue();
    }

    public static SafeFloat operator %(SafeFloat f1, SafeFloat f2)
    {
        return new SafeFloat(f1.GetValue() % f2.GetValue());
    }
}