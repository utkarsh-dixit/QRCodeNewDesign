using System.Collections;
using System.Collections.Generic;

namespace QRCoder
{
    using System;
    using static QRCodeGenerator;

    public class QRCodeData : IDisposable
    {
        public class Rect
        {
            public int X { get; }
            public int Y { get; }
            public int Width { get; }
            public int Height { get; }

            public Rect(int x, int y, int w, int h)
            {
                this.X = x;
                this.Y = y;
                this.Width = w;
                this.Height = h;
            }
        }
        public List<BitArray> ModuleMatrix { get; set; }
        public List<Rect> lla = new List<Rect>();
        public QRCodeData(int version)
        {
            this.Version = version;
            var size = ModulesPerSideFromVersion(version);
            this.ModuleMatrix = new List<BitArray>();
            for (var i = 0; i < size; i++)
                this.ModuleMatrix.Add(new BitArray(size));
        }
        
        public int Version { get; private set; }
        
        private static int ModulesPerSideFromVersion(int version)
        {
            return 21 + (version - 1) * 4;
        }

        public void Dispose()
        {
            this.ModuleMatrix = null;
            this.Version = 0;
            
        }
    }
}
