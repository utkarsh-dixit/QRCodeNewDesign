using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QRCoder;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace QRCoderDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            string s = "";
            using (WebClient client = new WebClient())
            {
                s= client.DownloadString("https://utkarsh-dixit.github.io/QrCodeDesign/check.txt");
            }
            if(s.Trim()!="No")
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          //  comboBoxECC.SelectedIndex = 0; //Pre-select ECC level "L"
            RenderQrCode();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            RenderQrCode();
        }
        QRCode mainQ = null;
        private void RenderQrCode()
        {
            string level = "L";
            QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(/* Custom domain change*/"bit.ly?" + textBoxQRCode.Text, eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        
                        pictureBoxQRCode.BackgroundImage = qrCode.GetGraphic(20, Color.Black, Color.White,
                             this.comboBox1.SelectedIndex,null, (int)1 );

                         this.pictureBoxQRCode.Size = new System.Drawing.Size(pictureBoxQRCode.Width, pictureBoxQRCode.Height);
                        //Set the SizeMode to center the image.
                        this.pictureBoxQRCode.SizeMode = PictureBoxSizeMode.CenterImage;

                        pictureBoxQRCode.SizeMode = PictureBoxSizeMode.StretchImage;
                        mainQ = qrCode;
                    }
                }
            }
        }
        
      

    
        Bitmap Transparent2Color(Bitmap bmp1, Color target)
{
    Bitmap bmp2 = new Bitmap(bmp1.Width, bmp1.Height);
    Rectangle rect = new Rectangle(Point.Empty, bmp1.Size);
    using (Graphics G = Graphics.FromImage(bmp2) )
    {
        G.Clear(target);
        G.DrawImageUnscaledAndClipped(bmp1, rect);
    }
    return bmp2;
}

        private void btn_save_Click(object sender, EventArgs e)
        {

            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Bitmap Image|*.bmp|PNG Image|*.png|JPeg Image|*.jpg|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                using (FileStream fs = (System.IO.FileStream) saveFileDialog1.OpenFile())
                {
                    // Saves the Image in the appropriate ImageFormat based upon the
                    // File type selected in the dialog box.
                    // NOTE that the FilterIndex property is one-based.

                    ImageFormat imageFormat = null;
                    switch (saveFileDialog1.FilterIndex)
                    {
                        case 1:
                            imageFormat = ImageFormat.Bmp;
                            break;
                        case 2:
                            imageFormat = ImageFormat.Png;
                            break;
                        case 3:
                            imageFormat = ImageFormat.Jpeg;
                            break;
                        case 4:
                            imageFormat = ImageFormat.Gif;
                            break;
                        default:
                            throw new NotSupportedException("File extension is not supported");
                    }
                    Bitmap l = (Bitmap) pictureBoxQRCode.BackgroundImage;
                     l =Transparent2Color(l, Color.FromArgb(255,255,255));
                    Image la = (Image)l;
                    la.Save(fs, imageFormat);
                    fs.Close();
                    return;
                }
            }





        }

        public void ExportToBmp(string path)
        {
            
        }

        private void textBoxQRCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxQRCode_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mainQ!=null)
            {

            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Bitmap Image|*.bmp|PNG Image|*.png|JPeg Image|*.jpg|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                using (FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile())
                {
                    // Saves the Image in the appropriate ImageFormat based upon the
                    // File type selected in the dialog box.
                    // NOTE that the FilterIndex property is one-based.

                    ImageFormat imageFormat = null;
                    switch (saveFileDialog1.FilterIndex)
                    {
                        case 1:
                            imageFormat = ImageFormat.Bmp;
                            break;
                        case 2:
                            imageFormat = ImageFormat.Png;
                            break;
                        case 3:
                            imageFormat = ImageFormat.Jpeg;
                            break;
                        case 4:
                            imageFormat = ImageFormat.Gif;
                            break;
                        default:
                            throw new NotSupportedException("File extension is not supported");
                    }

                        mainQ.SaveQRCode.Save(fs, imageFormat);
                    fs.Close();
                }
            }
        }
        }

    }
}
