using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pathfinding {
    class Map {
        /*
         * Was muss denn eine Map alles haben?
         *  - Liste der Tiles, 2D-Array; die werden dann parallelogrammförmig gezeichnet.
         *    Die Tiles können sich im Laufe des Spiels ändern, z. B. durch Schnee schmelzen.
         *  - Eine Liste der temporär veränderten Tiles, das betrifft nur Trampelpfade; diese
         *    wachsen wieder zu wenn sie eine Zeit nicht benutzt wurden.
         *  - Eine Erzkarte; dass das nur unter Bergen liegen kann stellt schon der Editor sicher?
         *    die Karte speichert auch, welche Teile schon entdeckt wurden durch Geologen;
         *    wird zur Schilderanzeige benutzt.
         *  - Es gibt verschiedene Objekte auf der Karte, manche können durchlaufen werden, andere nicht.
         *  - Eine Gebäudekarte; Gebäude brauchen mehrere Tiles; Zusammenhänge werden durch
         *    gleiche IDs bei der Belegung gespeichert.
         *  - Eine Höhenkarte; diese zusammen mit der Tile-Karte ergibt das tatsächlich zu zeichnende Tile.
         *    Oder wie werden Licht und Schatten realisiert?
         *  - Die einzigen natürlichen Objekte, die nicht durchlaufen werden können, sind Bäume und Steine?
         *  - Eine Liste der Ökosektoren und eine dazugehörige Karte, in der die Tiles entsprechend
         *    markiert sind, die zusammengehören. Die Tiles eines Ökosektors ergeben sozusagen die
         *    erste Einschränkung für das Pathfinding von Zivilisten.
         */

        /*
         * Wie sieht das Format aus, in dem Karten gespeichert werden?
         * Nachdem wir ja keine rechteckigen Karten haben, ist XML wahrscheinlich am Besten.
         */

        public string Name {
            get;
            set;
        }

        public Size Size {
            get;
            private set;
        }

        public int[,] Tiles {
            get;
            private set;
        }

        public Map(string name, Size size) {
            if (size.Width != size.Height) {
                throw new ArgumentException("Die Map ist nicht quadratisch.");
            }

            Name = name;
            Size = size;
            Tiles = new int[size.Width, size.Height];
        }

        public static Map Load(string filename) {
            XElement xml = XElement.Load(filename);
            
            Map map = new Map(xml.Attribute("Name").Value, new Size(int.Parse(xml.Attribute("Width").Value), int.Parse(xml.Attribute("Height").Value)));

            // Tiles einlesen
            int numTiles = map.Size.Width * map.Size.Height;

            string[] tiles = xml.Element("Tiles").Value.Split(',');

            if (tiles.Length != numTiles) {
                throw new ArgumentException("Die Anzahl der Tiles stimmt nicht mit der angegebenen Map-Größe überein.");
            }


            return null;
        }
    }
}
