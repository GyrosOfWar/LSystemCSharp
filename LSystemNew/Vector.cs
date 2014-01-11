using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSystemNew {
    /// <summary>
    /// A 2D Vector with immutable integer values.
    /// </summary>
    public struct Vector {
        public readonly int X, Y;
        public Vector(int X, int Y) {
            this.X = X;
            this.Y = Y;
        }

        public static readonly Vector Zero = new Vector(0, 0);

        public static readonly Vector One = new Vector(1, 1);

        public override bool Equals(object obj) {
            if (obj is Vector) {
                Vector p = (Vector)obj;
                return (p.X == this.X) && (p.Y == this.Y);
            }
            return false;
        }

        public override int GetHashCode() {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override string ToString() {
            return "[" + X + ", " + Y + "]";
        }

        public int Dot(Vector that) {
            return this.X * that.X + this.Y * that.Y;
        }

        public static Vector operator +(Vector a, Vector b) {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector a, Vector b) {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }

        public static Vector operator *(Vector a, Vector b) {
            return new Vector(a.X * b.X, a.Y * b.Y);
        }

        public static Vector operator *(Vector a, int b) {
            return new Vector(a.X * b, a.Y * b);
        }

        public static Vector operator +(Vector a, int b) {
            return new Vector(a.X + b, a.Y + b);
        }

        public static bool operator ==(Vector a, Vector b) {
            return a.Equals(b);
        }

        public static bool operator !=(Vector a, Vector b) {
            return !(a.Equals(b));
        }
    }
}
