using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging.ComplexFilters;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace lab1_pr
{
    public partial class Form1 : Form
    {
        private OpenFileDialog open;
        private Bitmap bmp = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // open file dialog 
            open = new OpenFileDialog();

            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.bmp; *.png)|*.jpg; *.jpeg; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                //dispoze previous ui objects
                if (bmp != null)
                {
                    bmp.Dispose();
                }

                // display image in picture box
                bmp = new Bitmap(open.FileName);
                image.Width = bmp.Width;
                image.Height = bmp.Height;
                image.Image = bmp;
                open.Dispose();
            }
               
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                //save file dialog
                SaveFileDialog save = new SaveFileDialog();

                // image filters
                save.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    // If the file name is not an empty string open it for saving.
                    if (save.FileName != "")
                    {
                        // Saves the Image via a FileStream created by the OpenFile method.
                        System.IO.FileStream fs =
                           (System.IO.FileStream)save.OpenFile();

                        switch (save.FilterIndex)
                        {
                            case 1:
                                this.image.Image.Save(fs,
                                   System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;

                            case 2:
                                this.image.Image.Save(fs,
                                   System.Drawing.Imaging.ImageFormat.Bmp);
                                break;

                            case 3:
                                this.image.Image.Save(fs,
                                   System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                            case 4:
                                this.image.Image.Save(fs,
                                   System.Drawing.Imaging.ImageFormat.Png);
                                break;
                        }
                        fs.Close();
                    }
                }
            }
        }

        private void grayscaleImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                GrayscaleBT709 gray = new GrayscaleBT709();
                image.Image = gray.Apply(bmp);
            }
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                Sepia sepia = new Sepia();
                image.Image = sepia.Apply(bmp);
            }
        }

        private void originalImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            image.Image = bmp;
            
        }

        private void blurImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                Blur blur = new Blur();
                image.Image = blur.Apply(bmp);
            }
        }

        private void invertColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                Invert invert = new Invert();
                image.Image = invert.Apply(bmp);
            }
        }
    }
}
