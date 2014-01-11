using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LSystemNew;
using System.Collections.Immutable;
using System.Windows.Media;

namespace LSystemNewTest {
    [TestClass]
    public class TurtleTest {
        Action<int, int, int, int, Color> dummy = (a, b, c, d, color) => Console.WriteLine(a + ", " + b + ", " + c + ", " + d);
        [TestMethod]
        public void TestForward() {
            var turtle = new Turtle(new Vector(0, 0), 0.0, ImmutableStack<Turtle.TurtleState>.Empty, dummy, Colors.Black);
            var forward = turtle.Forward(15);

            var expected = new Vector(15, 0);
            var actual = forward.Position;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestRotate() {
            var turtle = new Turtle(new Vector(0, 0), 0.0, ImmutableStack<Turtle.TurtleState>.Empty, dummy, Colors.Black);
            var rotated = turtle.Rotate(Math.PI / 2);

            var expected = Math.PI / 2;
            var actual = rotated.Angle;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestPush() {
            var turtle = new Turtle(new Vector(0, 0), 0.0, ImmutableStack<Turtle.TurtleState>.Empty, dummy, Colors.Black);
            var pushed = turtle.Push();
            var first = pushed.Stack.Peek();
            var pos = first.Position;
            var angle = first.Angle;
            var color = first.DrawColor;

            Assert.AreEqual(turtle.Position, pos);
            Assert.AreEqual(turtle.Angle, angle);
            Assert.AreEqual(turtle.DrawColor, color);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Used Pop on an empty stack.")]
        public void TestBadPop() {
            var turtle = new Turtle(new Vector(0, 0), 0.0, ImmutableStack<Turtle.TurtleState>.Empty, dummy, Colors.Black);
            turtle.Pop();
        }
        [TestMethod]
        public void TestPop() {
            var state = new Turtle.TurtleState(new Vector(2, 2), Math.PI / 4, Colors.Black);
            var stack = ImmutableStack<Turtle.TurtleState>.Empty.Push(state);
            var turtle = new Turtle(new Vector(0, 0), 0.0, stack, dummy, Colors.Black);
            var popped = turtle.Pop();

            Assert.AreEqual(popped.Position, state.Position);
            Assert.AreEqual(popped.Angle, state.Angle);
            Assert.AreEqual(popped.DrawColor, state.DrawColor);
        }
        [TestMethod]
        public void TestChangeColor() {
            var turtle = new Turtle(new Vector(0, 0), 0.0, ImmutableStack<Turtle.TurtleState>.Empty, dummy, Colors.Black);
            var colorChanged = turtle.ChangeColor(Colors.Green);

            Assert.AreEqual(colorChanged.DrawColor, Colors.Green);
        }

    }
}
