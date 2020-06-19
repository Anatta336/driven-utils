using System.Collections.Generic;
using UnityEngine;

namespace SamDriver.Util
{
    public static class CubicSolver
    {
        const float squareRoot3 = 1.7320508075688772935274463415059f;

        public static float SimpleCubeRoot(float x)
        {
            if (x < 0f)
            {
                return -Mathf.Pow(-x, 1f / 3f);
            }
            else
            {
                return Mathf.Pow(x, 1f / 3f);
            }
        }

        public static IEnumerable<float> Solve(float a, float b, float c, float d, bool isLogging = false)
        {
            if (isLogging) Debug.Log($"solving {a.ToString("F4")}x³ + {b.ToString("F4")}x² + {c.ToString("F4")}x + {d.ToString("F4")} = 0");

            // based on http://www.1728.org/cubic2.htm
            float f = (c / a) - (b * b) / (3f * a * a);
            // if (isLogging) Debug.Log($"{nameof(f)} = {f.ToString("F4")}");

            float g = (1f / 27f) * (
                ((2f * b * b * b) / (a * a * a)) -
                ((9f * b * c) / (a * a)) +
                (27f * d / a)
            );
            // if (isLogging) Debug.Log($"{nameof(g)} = {g.ToString("F4")}");

            float h = ((g * g) / 4f) + ((f * f * f) / 27f);
            // if (isLogging) Debug.Log($"{nameof(h)} = {h.ToString("F4")}");

            bool isAllZero = Mathf.Approximately(f, 0f) && 
                Mathf.Approximately(g, 0f) &&
                Mathf.Approximately(h, 0f);
            
            if (h > 0f)
            {
                // only 1 root is real, other 2 imaginary
                if (isLogging) Debug.Log("1 real root, 2 imaginary");

                float squareRootH = Mathf.Sqrt(h);

                float r = -g / 2f + squareRootH;
                // if (isLogging) Debug.Log($"{nameof(r)} = {r.ToString("F4")}");
                float s = SimpleCubeRoot(r);
                // if (isLogging) Debug.Log($"{nameof(s)} = {s.ToString("F4")}");

                float t = -g / 2f - squareRootH;
                // if (isLogging) Debug.Log($"{nameof(t)} = {t.ToString("F4")}");
                float u = SimpleCubeRoot(t);
                // if (isLogging) Debug.Log($"{nameof(u)} = {u.ToString("F4")}");
                
                yield return s + u - (b / (3f * a));
                // imaginary roots would be:
                // realPart = -(s + u) / 2f - (b / (3f * a)), imaginary = 0.5f * (s - u) * squareRoot3
                // realPart = -(s + u) / 2f - (b / (3f * a)), imaginary = -0.5f * (s - u) * squareRoot3
            }
            else if (isAllZero)
            {
                // only 1 root, no imaginary roots
                if (isLogging) Debug.Log("1 real root, no imaginary");

                yield return -SimpleCubeRoot(d / a);
            }
            else // h <= 0, at least one of f and g is not 0
            {
                // all 3 roots are real
                if (isLogging) Debug.Log("3 real roots");

                float i = Mathf.Sqrt((g * g) / 4f - h);
                float j = SimpleCubeRoot(i);
                // if (isLogging) Debug.Log($"{nameof(j)} = {j.ToString("F4")}");

                float k = Mathf.Acos(-g / (2f * i));
                // float l = -j;
                float m = Mathf.Cos(k / 3f);
                // if (isLogging) Debug.Log($"{nameof(m)} = {m.ToString("F4")}");

                float n = squareRoot3 * Mathf.Sin(k / 3f);
                // if (isLogging) Debug.Log($"{nameof(n)} = {n.ToString("F4")}");

                float p = -b / (3f * a);
                // if (isLogging) Debug.Log($"{nameof(p)} = {p.ToString("F4")}");

                yield return 2f * j * m + p;
                yield return -j * (m + n) + p;
                yield return -j * (m - n) + p;
            }   
        }

        //TODO: tests
        /*
        CubicSolver.Solve(2f, -4f, -22f, 24f)
        3 real roots: 4, -3, 1

        CubicSolver.Solve(3f, -10f, +14f, 27f)
        1 real root, 2 imaginary: -1
        
        CubicSolver.Solve(1f, 6f, 12f, 8f)
        3 equal real roots: -2
        */
    }
}