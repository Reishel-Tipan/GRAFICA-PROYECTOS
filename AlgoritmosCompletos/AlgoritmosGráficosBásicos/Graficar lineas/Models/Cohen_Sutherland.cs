using System;
using System.Drawing;

namespace Graficar_lineas.Models
{
    public class Cohen_Sutherland
    {
        // Códigos de región (outcode)
        private const int INSIDE = 0; // 0000
        private const int LEFT = 1;   // 0001
        private const int RIGHT = 2;  // 0010
        private const int BOTTOM = 4; // 0100
        private const int TOP = 8;    // 1000

        private readonly Rectangle clipWindow;

        public Cohen_Sutherland(Rectangle window)
        {
            clipWindow = window;
        }

        // Calcula el código de región para un punto
        private int ComputeOutCode(Point p)
        {
            int code = INSIDE;

            if (p.X < clipWindow.Left)
                code |= LEFT;
            else if (p.X > clipWindow.Right)
                code |= RIGHT;

            if (p.Y < clipWindow.Top)
                code |= TOP;
            else if (p.Y > clipWindow.Bottom)
                code |= BOTTOM;

            return code;
        }

        // Implementación del algoritmo de Cohen-Sutherland
        public bool ClipLine(ref Point p1, ref Point p2)
        {
            int outcode1 = ComputeOutCode(p1);
            int outcode2 = ComputeOutCode(p2);
            bool accept = false;

            while (true)
            {
                // Caso trivial: ambos puntos dentro
                if ((outcode1 | outcode2) == 0)
                {
                    accept = true;
                    break;
                }
                // Caso trivial: ambos puntos fuera del mismo lado
                else if ((outcode1 & outcode2) != 0)
                {
                    break;
                }
                // Caso no trivial: calcular intersección
                else
                {
                    int outcodeOut = outcode1 != 0 ? outcode1 : outcode2;
                    Point p = new Point();

                    // Calcular punto de intersección
                    if ((outcodeOut & TOP) != 0)
                    {
                        p.X = p1.X + (p2.X - p1.X) * (clipWindow.Top - p1.Y) / (p2.Y - p1.Y);
                        p.Y = clipWindow.Top;
                    }
                    else if ((outcodeOut & BOTTOM) != 0)
                    {
                        p.X = p1.X + (p2.X - p1.X) * (clipWindow.Bottom - p1.Y) / (p2.Y - p1.Y);
                        p.Y = clipWindow.Bottom;
                    }
                    else if ((outcodeOut & RIGHT) != 0)
                    {
                        p.Y = p1.Y + (p2.Y - p1.Y) * (clipWindow.Right - p1.X) / (p2.X - p1.X);
                        p.X = clipWindow.Right;
                    }
                    else if ((outcodeOut & LEFT) != 0)
                    {
                        p.Y = p1.Y + (p2.Y - p1.Y) * (clipWindow.Left - p1.X) / (p2.X - p1.X);
                        p.X = clipWindow.Left;
                    }

                    // Reemplazar punto fuera por el punto de intersección
                    if (outcodeOut == outcode1)
                    {
                        p1 = p;
                        outcode1 = ComputeOutCode(p1);
                    }
                    else
                    {
                        p2 = p;
                        outcode2 = ComputeOutCode(p2);
                    }
                }
            }

            return accept;
        }
    }
}
