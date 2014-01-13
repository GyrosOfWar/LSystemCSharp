using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using System.Windows.Media;

namespace LSystemNew {
    /// <summary>
    /// An immutable turtle graphics turtle. Can move forward, rotate, 
    /// push its current state to a stack and restore its state from the stack.
    /// </summary>
    public class Turtle {
        /// <summary>
        /// Container class to save the state of the turtle on the stack.
        /// </summary>
        public struct TurtleState {
            public readonly Vector Position;
            public readonly double Angle;
            public readonly Color DrawColor;
            public TurtleState(Vector pos, double angle, Color c) {
                this.Position = pos;
                this.Angle = angle;
                this.DrawColor = c;
            }
        }

        public readonly Vector Position;
        public readonly double Angle;
        public readonly ImmutableStack<TurtleState> Stack;
        public readonly Action<int, int, int, int, Color> DrawLine;
        public readonly Color DrawColor;
        public Turtle(Vector pos, double angle, ImmutableStack<TurtleState> stack, Action<int, int, int, int, Color> drawLine, Color c) {
            this.Position = pos;
            this.Angle = angle;
            this.Stack = stack;
            this.DrawLine = drawLine;
            this.DrawColor = c;
        }

        public Turtle Forward(int distance) {
            var newX = Math.Cos(Angle) * distance;
            var newY = Math.Sin(Angle) * distance;
            var newPos = this.Position + new Vector((int)newX, (int)newY);
            DrawLine(Position.X, Position.Y, newPos.X, newPos.Y, DrawColor);
            return new Turtle(newPos, this.Angle, this.Stack, this.DrawLine, this.DrawColor);
        }

        public Turtle Rotate(double rot) {
            return new Turtle(this.Position, this.Angle + rot, this.Stack, this.DrawLine, this.DrawColor);
        }

        public Turtle Pop() {
            if (Stack.IsEmpty) {
                throw new InvalidOperationException("Stack must not be empty when using Pop().");
            }

            var state = Stack.Peek();
            return new Turtle(state.Position, state.Angle, Stack.Pop(), this.DrawLine, state.DrawColor);
        }

        public Turtle Push() {
            var newStack = this.Stack.Push(new TurtleState(this.Position, this.Angle, this.DrawColor));
            return new Turtle(this.Position, this.Angle, newStack, this.DrawLine, this.DrawColor);
        }

        public Turtle ChangeColor(Color c) {
            return new Turtle(this.Position, this.Angle, this.Stack, this.DrawLine, c);
        }
    }

}
