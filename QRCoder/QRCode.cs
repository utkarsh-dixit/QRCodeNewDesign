using System.Drawing;
using System.Drawing.Drawing2D;

namespace QRCoder
{
    using System;
    using static QRCodeData;

    public class QRCode : AbstractQRCode<Bitmap>, IDisposable
    {
        public QRCode(QRCodeData data) : base(data) {}
        
        public override Bitmap GetGraphic(int pixelsPerModule)
        {
            return this.GetGraphic(pixelsPerModule, Color.Black, Color.White, true);
        }
    
        public Bitmap GetGraphic(int pixelsPerModule, string darkColorHtmlHex, string lightColorHtmlHex, bool drawQuietZones = true)
        {
            return this.GetGraphic(pixelsPerModule, ColorTranslator.FromHtml(darkColorHtmlHex), ColorTranslator.FromHtml(lightColorHtmlHex), drawQuietZones);
        }
    
        public Bitmap GetGraphic(int pixelsPerModule, Color darkColor, Color lightColor, bool drawQuietZones = true)
        {
            var size = (this.QrCodeData.ModuleMatrix.Count - (drawQuietZones ? 0 : 8)) * pixelsPerModule;
            var offset = drawQuietZones ? 0 : 4 * pixelsPerModule;

            var bmp = new Bitmap(size, size);
            var gfx = Graphics.FromImage(bmp);
            for (var x = 0; x < size + offset; x = x + pixelsPerModule)
            {
                for (var y = 0; y < size + offset; y = y + pixelsPerModule)
                {
                    var module = this.QrCodeData.ModuleMatrix[(y + pixelsPerModule)/pixelsPerModule - 1][(x + pixelsPerModule)/pixelsPerModule - 1];
                    if (module)
                    {
                        gfx.FillRectangle(new SolidBrush(darkColor), new Rectangle(x - offset, y - offset, pixelsPerModule, pixelsPerModule));
                    }
                    else
                    {
                        gfx.FillRectangle(new SolidBrush(lightColor), new Rectangle(x - offset, y - offset, pixelsPerModule, pixelsPerModule));
                    }
                }
            }
    
            gfx.Save();
            return bmp;
        }

        public string returnHexa(int x,int y)
        {

            return "#00A551";
        }
        // call this from your code, Main, Form_Load or Button_Click for example

        // the function that does the work
        public Bitmap SaveQRCode = null;
        
        public Bitmap GetGraphic(int pixelsPerModule, Color darkColor, Color lightColor, int design, Bitmap icon=null, int iconSizePercent=15, int iconBorderWidth = 6, bool drawQuietZones = true)
        {
            
            Color topRightSmallColor = Color.FromArgb(99, 48, 52);
            Color bottomColor = Color.FromArgb(30, 68, 39);
            Color topRightLargeColor = Color.FromArgb(67, 60, 57);
            Color everyThingColor = Color.FromArgb(31, 68, 35);
            String backgroundImage = "assets/6.1.green.png";
            String cornorImage = "assets/first_bot.png";
           if (design == 1)
            {
                backgroundImage = "assets/6.1Blue.2.png";
                cornorImage = "assets/blueBottom.png";
                topRightLargeColor = Color.FromArgb(96, 86, 85);
                topRightSmallColor = Color.FromArgb(128,83,57);
                everyThingColor = Color.FromArgb(49, 75, 175);
                bottomColor = Color.FromArgb(10, 94, 99);
                //1

            }
            else if(design==2)
            {
                backgroundImage = "assets/61.orange.png";
                cornorImage = "assets/orangebottom.png";
                topRightLargeColor = Color.FromArgb(77,69, 66);
                topRightSmallColor = Color.FromArgb(67, 76, 38);
                everyThingColor = Color.FromArgb(15, 75, 78);
                bottomColor = Color.FromArgb(114, 64,29);
                // 2
            }
           else if (design == 3)
            {
                backgroundImage = "assets/6.1Gray.png";
                cornorImage = "assets/graybottom.png";
                topRightLargeColor = Color.FromArgb(72, 63, 61);
                topRightSmallColor = Color.FromArgb(64, 65, 62);
                everyThingColor = Color.FromArgb(63, 65, 65);
                bottomColor = Color.FromArgb(67, 65, 62);
            }
            var size = (this.QrCodeData.ModuleMatrix.Count - (drawQuietZones ? 0 : 8)) * pixelsPerModule;
            var offset = drawQuietZones ? 0 : 4 * pixelsPerModule;

            var bmp = new Bitmap(size, size, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            var gfx = Graphics.FromImage(bmp);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.CompositingQuality = CompositingQuality.HighQuality;
            gfx.Clear(lightColor);
    
            var drawIconFlag = icon != null && iconSizePercent>0 && iconSizePercent<=100;
    
            GraphicsPath iconPath = null;
            float iconDestWidth=0, iconDestHeight=0, iconX=0, iconY=0;
    
                iconDestWidth = iconSizePercent * bmp.Width / 100f;
                iconDestHeight = drawIconFlag ? iconDestWidth * icon.Height / icon.Width : 0;
                iconX = (bmp.Width - iconDestWidth) / 2;
                iconY = (bmp.Height - iconDestHeight) / 2;
    
                var centerDest = new RectangleF(iconX - iconBorderWidth, iconY - iconBorderWidth, iconDestWidth + iconBorderWidth * 2, iconDestHeight + iconBorderWidth * 2);
          
    
            var lightBrush = new SolidBrush(lightColor);
            var darkBrush = new SolidBrush(darkColor);
  
            for (var x = 0; x < size+offset; x = x + pixelsPerModule)
            {
                for (var y = 0; y < size + offset; y = y + pixelsPerModule)
                {
                    
                    var module = this.QrCodeData.ModuleMatrix[(y + pixelsPerModule)/pixelsPerModule - 1][(x + pixelsPerModule)/pixelsPerModule - 1];
                    if (module)
                    {
                        var rAverage = 0 + (int)((0 - 0) * (x*y)  / (size*size));
                        var gAverage = 148 + (int)((165 - 148) * (x * y) / (size*size));
                        var bAverage = 81 + (int)((217 - 81) * (x * y) / (size*size));

                        int t = 1;
                        int offSetP = 3;
                        var r = new Rectangle((x*t)-offset, (y*t)-offset, pixelsPerModule-offSetP, pixelsPerModule-offSetP);
                    
                            var lla = new Rectangle((int)(centerDest.X + 60), (int)((centerDest.Y + 110)), 260, 260);

                            if (Math.Pow((lla.Left + lla.Right) / 2 - (r.Left + r.Right) / 2, 2) + Math.Pow((lla.Top + lla.Bottom) / 2 - (r.Top + r.Bottom) / 2, 2) < Math.Pow(180, 2))
                            {
                            // Top Right Smaller
                                System.Drawing.Color col = topRightSmallColor;
                                var sr = new SolidBrush(col);
                                gfx.FillEllipse(sr, r);
                            }
                            else
                            {
                            var ll = new Rectangle((int)(centerDest.X - 345), (int)((centerDest.Y - 210)), 430, 430);
                            if (Math.Pow((ll.Left + ll.Right) / 2 - (r.Left + r.Right) / 2, 2) + Math.Pow((ll.Top + ll.Bottom) / 2 - (r.Top + r.Bottom) / 2, 2) < Math.Pow(ll.Width/2, 2))
                            {
                                //Bottom Part
                                System.Drawing.Color col = bottomColor;
                                     var sr = new SolidBrush(col);
                      
                                  gfx.FillEllipse(sr, r);
                            }
                            else
                            {

                                ll = new Rectangle((int)(centerDest.X - 47), (int)((centerDest.Y - 8)), 560, 560);
                                if (Math.Pow((ll.Left + ll.Right) / 2 - (r.Left + r.Right) / 2, 2) + Math.Pow((ll.Top + ll.Bottom) / 2 - (r.Top + r.Bottom) / 2, 2) <= Math.Pow(ll.Width / 2, 2))
                                {
                                    // Top Right Larger
                                    System.Drawing.Color col = topRightLargeColor;
                                    var sr = new SolidBrush(col);
                                    gfx.FillEllipse(sr, r);
                                }
                                else
                                {
                                    //Everything
                                    System.Drawing.Color col = everyThingColor;
                                    var sr = new SolidBrush(col);
                               
                                    gfx.FillEllipse(sr, r);
                                }
                               

                         
                            }
                            }


                    }
                  
                    
                }
            }
        
            if (drawIconFlag)
            {
                var iconDestRect = new RectangleF(iconX, iconY, iconDestWidth, iconDestHeight);
                gfx.DrawImage(icon, iconDestRect, new RectangleF(0, 0, icon.Width, icon.Height), GraphicsUnit.Pixel);
            }
            System.Drawing.Color cole = Color.FromArgb(26, 68, 31);
            var sre = new SolidBrush(cole);
            
            var src = new Bitmap(cornorImage);
            //Sseese 
         //   src.RotateFlip(RotateFlipType.Rotate180FlipXY);

            gfx.DrawImage(src, new Rectangle(75, 355,150, 150));
            var src1 = new Bitmap(cornorImage);
            //Sseese 
            src1.RotateFlip(RotateFlipType.Rotate180FlipX);

            gfx.DrawImage(src1, new Rectangle(75, 74, 150, 150));
            var src2 = new Bitmap(cornorImage);
            src2.RotateFlip(RotateFlipType.Rotate90FlipX);
            //Sseese rest


            gfx.DrawImage(src2, new Rectangle(75+280, 74, 150, 150));
        
            gfx.Save();
            bmp.RotateFlip(RotateFlipType.Rotate90FlipXY);
            bmp.MakeTransparent(Color.White);
            SaveQRCode = bmp;
            var src3 = new Bitmap(backgroundImage);
            //Sseese 

            var gfx1 = Graphics.FromImage(src3);
            gfx1.DrawImage(bmp, new Rectangle(1512, 914, 2680, 2680));
           
            gfx1.Save();
            return new Bitmap(src3, new Size(src3.Width / 4, src3.Height / 4));
        }
    
        internal GraphicsPath CreateRoundedRectanglePath(RectangleF rect, int cornerRadius)
        {
            var roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        public void Dispose()
        {
            this.QrCodeData = null;
        }
    }
}
