using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Im Spiel wird der Path immer nur zu Beginn eines Bewegungskommandos berechnet, nachher nicht mehr.
 * Die Welt ändert sich i. d. R. eh nicht.
 * Gibt es irgendwie eine Kollision, dann läuft ein Objekt einfach um ein anderes rum und geht den Weg weiter.
 * Wenn es wirklich nicht mehr weitergeht, dann bleibt das Objekt stehen und tut nichts, das ist verkraftbar.
 */

namespace Pathfinding {
    interface IHeuristicPolicy {
        int GetDistance(Point a, Point b);
    }

    class EuclideanDistancePolicy : IHeuristicPolicy {
        public int GetDistance(Point a, Point b) {
            return (int)Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }
    }

    class ManhattanDistancePolicy : IHeuristicPolicy {
        public int GetDistance(Point a, Point b) {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }
    }

    interface ICollisionDetector {
        bool IsTileBlocked(Content[,] map, Point p);
    }

    class CollisionDetector1 : ICollisionDetector {
        public bool IsTileBlocked(Content[,] map, Point p) {
            return map[p.X, p.Y] == Content.Blocked;
        }
    }

    
    class Pathfinder<THeuristic> where THeuristic : IHeuristicPolicy, new() {
        THeuristic heuristic = new THeuristic();

        public struct Node {
            public Point Point;
            public int G;

            public Node(Point p, int g) {
                Point = p;
                G = g;
            }

            public override bool Equals(object obj) {
                return this.Point.Equals(((Node)obj).Point);
            }

            public override int GetHashCode() {
                return base.GetHashCode();
            }
        }

        
        private Content[,] map;
        ICollisionDetector collisionDetector = new CollisionDetector1();
        
        public Pathfinder(Content[,] map) {
            this.map = (Content[,])map.Clone();
        }

        // Ermittelt die "Nachfolgeknoten", d. h. die Elemente
        // im Array ringsrum.
        private List<Point> GetSuccessors(Point p) {
            List<Point> list = new List<Point>();

            if (p.X - 1 > 0 && !collisionDetector.IsTileBlocked(map, new Point(p.X - 1, p.Y))) {
                list.Add(new Point(p.X - 1, p.Y));
            }

            if (p.X + 1 <= map.GetUpperBound(0) && !collisionDetector.IsTileBlocked(map, new Point(p.X + 1, p.Y))) {
                list.Add(new Point(p.X + 1, p.Y));
            }

            if (p.Y - 1 > 0 && !collisionDetector.IsTileBlocked(map, new Point(p.X, p.Y - 1))) {
                list.Add(new Point(p.X, p.Y - 1));
            }

            if (p.Y + 1 <= map.GetUpperBound(1) && !collisionDetector.IsTileBlocked(map, new Point(p.X, p.Y + 1))) {
                list.Add(new Point(p.X, p.Y + 1));
            }

            return list;

            //List<Point> list = new List<Point>();

            //if (p.X - 1 > 0) {
            //    if (p.Y - 1 > 0) {
            //        list.Add(new Point(p.X - 1, p.Y - 1));
            //    }

            //    list.Add(new Point(p.X - 1, p.Y));

            //    if (p.Y + 1 <= map.GetUpperBound(1)) {
            //        list.Add(new Point(p.X - 1, p.Y + 1));
            //    }
            //}

            //if (p.X + 1 <= map.GetUpperBound(0)) {
            //    if (p.Y - 1 > 0) {
            //        list.Add(new Point(p.X + 1, p.Y - 1));
            //    }

            //    list.Add(new Point(p.X + 1, p.Y));

            //    if (p.Y + 1 <= map.GetUpperBound(1)) {
            //        list.Add(new Point(p.X + 1, p.Y + 1));
            //    }
            //}

            //if (p.Y - 1 > 0) {
            //    list.Add(new Point(p.X, p.Y - 1));
            //}

            //if (p.Y + 1 <= map.GetUpperBound(1)) {
            //    list.Add(new Point(p.X, p.Y + 1));
            //}

            //return list;

        }

        // Berechnet die geschätzte Entfernung zwischen zwei Knoten, diese
        // wird im Algorithmus als Heuristik verwendet.
        private int H(Point start, Point end) {
            // euklidscher Abstand
            //return (int)Math.Sqrt((start.X - end.X) * (start.X - end.X) + (start.Y - end.Y) * (start.Y - end.Y));

            // Manhattan-Metrik
            //return Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);

            return heuristic.GetDistance(start, end);
        }

        /// <summary>
        /// Ermittelt den kürzesten Weg vom Start- zum Endpunkt auf der im Kostruktor übergebenen Karte.
        /// </summary>
        /// <param name="start">Die Startkoordinaten auf der Karte.</param>
        /// <param name="end">Die Endkoordinaten auf der Karte.</param>
        /// <param name="path">Ein Array der Größe der Karte mit den abzulaufenden Tiles.</param>
        /// <returns>Gibt zurück, ob ein Weg gefunden werden konnte.</returns>
        public bool FindWayBetween(Point start, Point end, out Dictionary<Point, Point> path) {

            BinaryHeap<Node> openList = new BinaryHeap<Node>();
            HashSet<Point> closedList = new HashSet<Point>();

            Dictionary<Point, Point> vorgänger = new Dictionary<Point, Point>();

            openList.Insert(0, new Node(start, 0));
            
            do {
                Node currentNode = openList.ExtractMin().Value;

                if (currentNode.Point == end) {
                    path = vorgänger;
                    return true;
                }

                closedList.Add(currentNode.Point);


                // expandNode
                foreach (Point successor in GetSuccessors(currentNode.Point)) {
                    if (closedList.Contains(successor)) {
                        continue;
                    }

                    int tentative_g = currentNode.G + 1;

                    if (successor.X != currentNode.Point.X && successor.Y != currentNode.Point.Y) {
                        tentative_g++;
                    }


                    Node sucNode = new Node(successor, tentative_g);

                    if (openList.Contains(sucNode) && tentative_g >= sucNode.G) {
                        continue;
                    }
                    // Kann momentan ja eh nicht eintreten, wenn die Kosten von einem
                    // zum andern immer 1 sind?

                    vorgänger[sucNode.Point] = currentNode.Point;

                    int f = tentative_g + H(sucNode.Point, end);

                    if (openList.Contains(sucNode)) {
                        openList.Decrease(openList.IndexOf(sucNode), f);
                    } else {
                        openList.Insert(f, sucNode);
                    }

                }

            } while (!openList.IsEmpty);

            path = vorgänger;
            return false;
        }
    }
}
