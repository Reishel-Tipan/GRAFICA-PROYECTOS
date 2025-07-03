using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Graficar_lineas.Models
{
    public class Sutherland_Hodgman
    {
        // Clase auxiliar para representar un borde del polígono de recorte
        private class Edge
        {
            public Point Start { get; set; }
            public Point End { get; set; }

            public Edge(Point start, Point end)
            {
                Start = start;
                End = end;
            }
        }

        private Rectangle clipWindow;
        private List<Edge> clipEdges;

        public Sutherland_Hodgman(Rectangle window)
        {
            clipWindow = window;
            InitializeClipEdges();
        }

        private void InitializeClipEdges()
        {
            clipEdges = new List<Edge>
            {
                new Edge(new Point(clipWindow.Left, clipWindow.Top), new Point(clipWindow.Right, clipWindow.Top)),    // Arriba
                new Edge(new Point(clipWindow.Right, clipWindow.Top), new Point(clipWindow.Right, clipWindow.Bottom)), // Derecha
                new Edge(new Point(clipWindow.Right, clipWindow.Bottom), new Point(clipWindow.Left, clipWindow.Bottom)), // Abajo
                new Edge(new Point(clipWindow.Left, clipWindow.Bottom), new Point(clipWindow.Left, clipWindow.Top))    // Izquierda
            };
        }

        // Verifica si un punto está dentro de la ventana de recorte
        private bool IsPointInClipWindow(Point p)
        {
            return p.X >= clipWindow.Left && p.X <= clipWindow.Right &&
                   p.Y >= clipWindow.Top && p.Y <= clipWindow.Bottom;
        }

        // Verifica si un segmento de línea cruza la ventana de recorte
        private bool LineIntersectsWindow(Point p1, Point p2)
        {
            // Si ambos puntos están en el mismo lado de la ventana, no hay intersección
            if ((p1.X < clipWindow.Left && p2.X < clipWindow.Left) ||
                (p1.X > clipWindow.Right && p2.X > clipWindow.Right) ||
                (p1.Y < clipWindow.Top && p2.Y < clipWindow.Top) ||
                (p1.Y > clipWindow.Bottom && p2.Y > clipWindow.Bottom))
            {
                return false;
            }

            // Verificar si la línea cruza la ventana usando el algoritmo de Cohen-Sutherland
            int code1 = ComputeOutCode(p1);
            int code2 = ComputeOutCode(p2);
            
            // Si ambos puntos están fuera y no hay intersección con la ventana
            if ((code1 & code2) != 0)
                return false;
                
            return true;
        }

        // Códigos de región para el algoritmo de Cohen-Sutherland
        private const int INSIDE = 0; // 0000
        private const int LEFT = 1;   // 0001
        private const int RIGHT = 2;  // 0010
        private const int BOTTOM = 4; // 0100
        private const int TOP = 8;    // 1000

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

        // Algoritmo de Sutherland-Hodgman para recortar un polígono
        public List<Point> ClipPolygon(List<Point> polygon)
        {
            if (polygon == null || polygon.Count < 3)
                return new List<Point>();

            // Primero verificamos si el polígono está completamente fuera de la ventana
            bool isCompletelyOutside = true;
            foreach (var point in polygon)
            {
                if (IsPointInClipWindow(point))
                {
                    isCompletelyOutside = false;
                    break;
                }
            }

            // Si está completamente fuera, verificamos si alguna arista cruza la ventana
            if (isCompletelyOutside)
            {
                bool anyEdgeIntersects = false;
                for (int i = 0; i < polygon.Count; i++)
                {
                    Point p1 = polygon[i];
                    Point p2 = polygon[(i + 1) % polygon.Count];

                    // Si la línea está completamente a la izquierda, derecha, arriba o abajo de la ventana
                    if ((p1.X < clipWindow.Left && p2.X < clipWindow.Left) ||
                        (p1.X > clipWindow.Right && p2.X > clipWindow.Right) ||
                        (p1.Y < clipWindow.Top && p2.Y < clipWindow.Top) ||
                        (p1.Y > clipWindow.Bottom && p2.Y > clipWindow.Bottom))
                    {
                        continue; // Esta arista no cruza la ventana
                    }

                    // Si llegamos aquí, la arista podría cruzar la ventana
                    anyEdgeIntersects = true;
                    break;
                }

                // Si no hay aristas que crucen, devolvemos lista vacía
                if (!anyEdgeIntersects)
                    return new List<Point>();
            }

            // Si llegamos aquí, el polígono está al menos parcialmente dentro o cruza la ventana
            // Asegurarse de que el polígono esté cerrado
            if (polygon[0] != polygon[polygon.Count - 1])
            {
                var closedPolygon = new List<Point>(polygon);
                closedPolygon.Add(closedPolygon[0]);
                polygon = closedPolygon;
            }

            List<Point> output = new List<Point>(polygon);

            // Recortar contra cada borde del polígono de recorte
            foreach (var edge in clipEdges)
            {
                if (output.Count < 2)
                    return new List<Point>();

                output = ClipAgainstEdge(output, edge);

                if (output.Count < 3)
                    return new List<Point>();
            }

            // Verificar si el polígono resultante es válido
            if (output.Count >= 3)
            {
                // Verificar si el polígono tiene área (no es colineal)
                double area = 0;
                int n = output.Count;
                for (int i = 0; i < n; i++)
                {
                    int j = (i + 1) % n;
                    area += output[i].X * output[j].Y - output[j].X * output[i].Y;
                }
                area = Math.Abs(area) / 2.0;

                if (area < 1.0) // Si el área es muy pequeña, no es un polígono válido
                    return new List<Point>();

                return output;
            }

            return new List<Point>();
        }

        // Determina si un punto está dentro del borde del polígono de recorte
        private bool IsInside(Point p, Edge edge)
        {
            // Usamos el producto cruz para determinar la posición relativa del punto respecto al borde
            return (edge.End.X - edge.Start.X) * (p.Y - edge.Start.Y) > 
                   (edge.End.Y - edge.Start.Y) * (p.X - edge.Start.X);
        }

        // Calcula la intersección entre el segmento AB y el borde de recorte
        private Point ComputeIntersection(Point a, Point b, Edge edge)
        {
            // Fórmula de intersección de líneas
            int x1 = a.X, y1 = a.Y;
            int x2 = b.X, y2 = b.Y;
            int x3 = edge.Start.X, y3 = edge.Start.Y;
            int x4 = edge.End.X, y4 = edge.End.Y;

            int den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            
            if (den == 0) // Líneas paralelas o coincidentes
                return a; // Devolver un punto por defecto

            int numX = (x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4);
            int numY = (x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4);

            int x = numX / den;
            int y = numY / den;

            return new Point(x, y);
        }

        private List<Point> ClipAgainstEdge(List<Point> polygon, Edge clipEdge)
        {
            List<Point> output = new List<Point>();
            
            // Si no hay suficientes puntos para formar un segmento, devolver lista vacía
            if (polygon.Count < 2)
                return output;
                
            Point S = polygon[polygon.Count - 1]; // Último punto del polígono
            bool sInside = IsInside(S, clipEdge);

            foreach (Point E in polygon)
            {
                bool eInside = IsInside(E, clipEdge);

                if (eInside)
                {
                    // Si el punto actual está dentro y el anterior no, agregar intersección
                    if (!sInside)
                    {
                        Point intersection = ComputeIntersection(S, E, clipEdge);
                        if (!output.Contains(intersection)) // Evitar duplicados
                            output.Add(intersection);
                    }
                    // Agregar el punto actual si está dentro
                    if (!output.Contains(E)) // Evitar duplicados
                        output.Add(E);
                }
                else if (sInside)
                {
                    // Si el punto anterior estaba dentro y el actual no, agregar intersección
                    Point intersection = ComputeIntersection(S, E, clipEdge);
                    if (!output.Contains(intersection)) // Evitar duplicados
                        output.Add(intersection);
                }
                
                S = E;
                sInside = eInside;
            }

            return output;
        }
    }
}
