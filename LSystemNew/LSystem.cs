using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace LSystemNew {
    public class LSystem {
        public readonly string Axiom;
        public readonly ImmutableDictionary<char, string> Rules;
        public readonly int Iterations;
        public readonly string Name;
        public readonly int Distance;
        public readonly Vector StartPos;
        public readonly double Angle;

        public LSystem(string axiom, ImmutableDictionary<char, string> rules, int iterations, double angle, string name, int distance, Vector startPos) {
            this.Axiom = axiom;
            this.Rules = rules;
            this.Iterations = iterations;
            this.Name = name;
            this.Distance = distance;
            this.StartPos = startPos;
            this.Angle = angle;
        }

        public override string ToString() {
            return Name + ": {Axiom = " + Axiom + ", Rules = " + Rules + ", #Iterations = " + Iterations + "}";
        }

        /// <summary>
        /// Does one iteration of the rules to the given state.
        /// </summary>
        /// <param name="state">State string to which the rule should be applied.</param>
        /// <returns>The new state after applying the rules once.</returns>
        public string StepOne(string state) {
            var sb = new StringBuilder(state.Length * 2);
            foreach (char c in state) {
                if (Rules.ContainsKey(c)) {
                    sb.Append(Rules[c]);
                } else {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Returns the state string after <code>this.Iterations</code> iterations.
        /// </summary>
        /// <returns>The full state string.</returns>
        public string Step() {
            var axiomList = Enumerable.Repeat(Axiom, Iterations + 1);
            var result = axiomList.Aggregate((t, _) => StepOne(t));
            return result.ToString();
        }

        public void Draw(WriteableBitmap image) {
            var state = this.Step();
            var start = new Turtle(
                StartPos,
                0.0,
                ImmutableStack<Turtle.TurtleState>.Empty,
                (a, b, c, d, color) => image.DrawLine(a, b, c, d, color),
                Colors.Black);
            state.Aggregate(start, (t, c) => {
                switch (c) {
                    case 'F': return t.Forward(this.Distance);
                    case '+': return t.Rotate(this.Angle);
                    case '-': return t.Rotate(-this.Angle);
                    case '[': return t.Push();
                    case ']': return t.Pop();
                    case '1': return t.ChangeColor(Color.FromRgb(140, 80, 60));
                    case '2': return t.ChangeColor(Color.FromRgb(24, 180, 24));
                    case '3': return t.ChangeColor(Color.FromRgb(48, 220, 48));
                    default: return t;
                }
            });
        }
    }
}
