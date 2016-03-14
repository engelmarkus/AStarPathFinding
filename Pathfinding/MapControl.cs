using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pathfinding {
    public partial class MapControl : Control {
        public MapControl() {
            InitializeComponent();
            DoubleBuffered = true;
        }

        public Content[,] Tiles {
            get;
            set;
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);

            pe.Graphics.Clear(Color.Black);

            Matrix transform = new Matrix();

            // Der Ursprung ist ganz oben, aber von links gesehen auf halber Strecke.
            transform.Translate((Tiles.GetUpperBound(0) + 1) * 32 / 2, 0);

            // Scheren um 32 / 2.
            transform.Shear(-0.5f, 0);

            // 32x32-Quadrat festlegen.
            Point[] rechteck = new Point[] { new Point(0, 0), new Point(0, 32), new Point(32, 32), new Point(32, 0) };

            for (int y = 0; y <= Tiles.GetUpperBound(1); y++) {
                for (int x = 0; x <= Tiles.GetUpperBound(0); x++) {
                    Brush br = Brushes.Black;

                    switch (Tiles[x, y]) {
                        case Content.Blocked:
                            br = Brushes.LightGray;
                            break;
                        case Content.Start:
                            br = Brushes.Green;
                            break;
                        case Content.End:
                            br = Brushes.Red;
                            break;
                        case Content.Way:
                            br = Brushes.Orange;
                            break;
                    }

                    Point[] aktuellesRechteck = (Point[])rechteck.Clone();

                    transform.TransformPoints(aktuellesRechteck);

                    pe.Graphics.FillPolygon(br, aktuellesRechteck);
                    pe.Graphics.DrawPolygon(Pens.White, aktuellesRechteck);

                    transform.Translate(32, 0);
                }

                transform.Translate(-(Tiles.GetUpperBound(0) + 1) * 32, 32);
            }
        }

        public override Size GetPreferredSize(Size proposedSize) {
            // Größe berechnen
            // Ein Tile hat 32 Pixel Grundlinie und ist 32 Pixel hoch
            // die obere Grundlinie ist zur unteren um 16 Pixel verschoben.

            // d. h. die Breite der Map ist die Anzahl der Tiles mal 32 mal 1,5.
            // die Höhe ist die Anzahl der Tiles mal 32.
            return new Size((int)((Tiles.GetUpperBound(0) + 1) * 32 * 1.5), (Tiles.GetUpperBound(1) + 1) * 32);
        }

        public Point CoordsToTile(Point coords) {
            /* Berechnet den Index des Tiles, das sich an den übergebenen Koordinaten innerhalb des
             * Steuerelements befindet.
             * Man nimmt die Transformationsmatrix vom Zeichnen oben und invertiert sie, sodass sie
             * eine "schräge" Koordinate in eine "rechteckige" umwandelt.
             * Dann wendet man sie auf den angeklickten Punkt an und bekommt die Koordinaten,
             * die ohne die Scherung angeklickt worden wären.
             */
            Matrix transform = new Matrix();

            // Der Ursprung ist ganz oben, aber von links gesehen auf halber Strecke.
            transform.Translate((Tiles.GetUpperBound(0) + 1) * 32 / 2, 0);

            // Kippen um 32 / 2.
            transform.Shear(-0.5f, 0);

            transform.Invert();

            Point[] punkt = new Point[] { coords };

            transform.TransformPoints(punkt);

            int column = punkt[0].X;
            int row = punkt[0].Y;

            if (column < 0 || column > (Tiles.GetUpperBound(0) + 1) * 32) {
                column = -1;
            } else {
                column /= 32;
            }

            if (row < 0 || row > (Tiles.GetUpperBound(1) + 1) * 32) {
                row = -1;
            } else {
                row /= 32;
            }
            
            return new Point(column, row);
        }
    }
}
