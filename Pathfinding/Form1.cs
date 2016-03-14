using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pathfinding {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        const int Anzahl = 20;
        //Tile[,] felder = new Tile[Anzahl, Anzahl];

        MapControl mc = new MapControl();

        private void Form1_Load(object sender, EventArgs e) {
            //for (int i = 0; i < Anzahl; i++) {
            //    for (int j = 0; j < Anzahl; j++) {
            //        felder[i, j] = new Tile();
            //        felder[i, j].Text = String.Format("{0}, {1}", i, j);
            //        felder[i, j].ForeColor = Color.Gray;
            //        felder[i, j].SetBounds(i * 40, j * 40, 40, 40);
            //        felder[i, j].Click += TileClicked;

            //        MapPanel.Controls.Add(felder[i, j]);
            //    }
            //}

            // MapControl            
            mc.Tiles = new Content[Anzahl, Anzahl];
            mc.AutoSize = true;
            mc.Click += mc_Click;
            panel2.Controls.Add(mc);
        }

        void mc_Click(object sender, EventArgs e) {
            // Zeile und Spalte bestimmen, an die geklickt wurde...
            Point p = mc.CoordsToTile(((MouseEventArgs)e).Location);

            // Wenn Klick außerhalb der Tiles...
            if (p.X < 0 || p.Y < 0) {
                return;
            }

            // Durchschalten...
            switch (mc.Tiles[p.X, p.Y]) {
                case Content.Empty:
                    mc.Tiles[p.X, p.Y] = Content.Blocked;
                    break;
                case Content.Start:
                    mc.Tiles[p.X, p.Y] = Content.End;
                    break;
                case Content.End:
                    mc.Tiles[p.X, p.Y] = Content.Empty;
                    break;
                case Content.Blocked:
                    mc.Tiles[p.X, p.Y] = Content.Start;
                    break;
                case Content.Way:
                    mc.Tiles[p.X, p.Y] = Content.Blocked;
                    break;
            }

            mc.Invalidate();
        }

        //private void TileClicked(Object sender, EventArgs e) {
        //    ((Tile)sender).IncreaseState();
        //}

        private void FindWayButton_Click(object sender, EventArgs e) {
            // reset way-Buttons
            //foreach (Tile t in MapPanel.Controls) {
            //    if (t.CurrentState == Content.Way) {
            //        t.CurrentState = Content.Empty;
            //    }
            //}
            for (int i = 0; i <= mc.Tiles.GetUpperBound(0); i++) {
                for (int j = 0; j <= mc.Tiles.GetUpperBound(1); j++) {
                    if (mc.Tiles[i, j] == Content.Way) {
                        mc.Tiles[i, j] = Content.Empty;
                    }
                }
            }

            //Content[,] content = new Content[Anzahl, Anzahl];

            Point Start = new Point(0, 0);
            Point End = new Point(0, 0);

            for (int i = 0; i < Anzahl; i++) {
                for (int j = 0; j < Anzahl; j++) {
                    //content[i, j] = felder[i, j].CurrentState;

                    //if (felder[i, j].CurrentState == Content.Start) {
                    //    Start = new Point(i, j);
                    //}

                    //if (felder[i, j].CurrentState == Content.End) {
                    //    End = new Point(i, j);
                    //}

                    if (mc.Tiles[i, j] == Content.Start) {
                        Start = new Point(i, j);
                    }

                    if (mc.Tiles[i, j] == Content.End) {
                        End = new Point(i, j);
                    }
                }
            }
            

            Dictionary<Point, Point> path;

            Pathfinder<ManhattanDistancePolicy> pf = new Pathfinder<ManhattanDistancePolicy>(mc.Tiles);
            bool foundWay = pf.FindWayBetween(Start, End, out path);

            if (!foundWay) {
                return;
            }

            Point currentPoint = path[End];

            while (currentPoint != Start) {
                //felder[currentPoint.X, currentPoint.Y].CurrentState = Content.Way;
                mc.Tiles[currentPoint.X, currentPoint.Y] = Content.Way;

                currentPoint = path[currentPoint];
            }

            mc.Invalidate();
        }

        private void resetButton_Click(object sender, EventArgs e) {
            for (int i = 0; i < Anzahl; i++) {
                for (int j = 0; j < Anzahl; j++) {
                    //felder[i, j].CurrentState = Content.Empty;
                    mc.Tiles[i, j] = Content.Empty;
                }
            }

            mc.Invalidate();
        }
    }
}
