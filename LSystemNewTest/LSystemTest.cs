using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LSystemNew;
using System.Collections.Immutable;
using System.Collections.Generic;

namespace LSystemNewTest {
    [TestClass]
    public class LSystemTest {
        public LSystem lsys;
        public LSystemTest() {
            var m = new Dictionary<char, string>();
            m.Add('X', "X+YF+");
            m.Add('Y', "-FX-Y");
            var rules = ImmutableDictionary.ToImmutableDictionary(m);
            this.lsys = new LSystem("FX", rules, 3, Math.PI / 2, "DragonCurve", 5, new Vector(2, 2));
        }

        [TestMethod]
        public void TestStepOne() {
            var actual = "FX+YF+";
            var expected = lsys.StepOne(lsys.Axiom);
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestStep() {
            var expected = lsys.Step();
            var actual = "FX+YF++-FX-YF++-FX+YF+--FX-YF+";
            Assert.AreEqual(expected, actual);
        }
    }
}
