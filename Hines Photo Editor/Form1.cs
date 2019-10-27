using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using System.Net;

namespace Hines_Photo_Editor
{
    public partial class APE : Form
    {
        public System.Drawing.Point current = new System.Drawing.Point();
        public System.Drawing.Point old = new System.Drawing.Point();
        public System.Windows.Forms.TextBox t = new System.Windows.Forms.TextBox();
        public Pen p = new Pen(Color.Red, 5);
        public Graphics g;
        Bitmap surface;
        Graphics graph;
        string s = "Picture";
        int i = 1;

        public APE()
        {
            InitializeComponent();

            g = pictureBox1.CreateGraphics();
            p.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);

            // Enable drag-and-drop operations and 
            // add handlers for DragEnter and DragDrop.
            this.AllowDrop = true;
            this.DragDrop += new DragEventHandler(this.Form1_Load);
            this.DragEnter += new DragEventHandler(this.Form1_Load);
            pictureBox1.BackgroundImage = surface;
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
        }


        void openimage()
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                file = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = file;
                opened = true;
            }
        }


        void filter2()
        {
            if (!opened)
            {
                MessageBox.Show("Open an image then apply changes");

            }
            else
            {

                Image img = pictureBox1.Image;
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);


                ImageAttributes ia = new ImageAttributes();
                ColorMatrix cmPicture = new ColorMatrix(new float[][]
                {
                    new float[] {.393f, .349f+0.5f, .272f, 0, 0},
                    new float[] {.769f+0.3f, .686f, .534f, 0, 0},
                    new float[] {.189f, .168f, .131f+0.5f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });
                ia.SetColorMatrix(cmPicture);
                Graphics g = Graphics.FromImage(bmpInverted);

                g.DrawImage(img, new System.Drawing.Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
                g.Dispose();
                pictureBox1.Image = bmpInverted;

            }
        }


        void filter3()
        {
            if (!opened)
            {
                MessageBox.Show("Open an image then apply changes");

            }
            else
            {

                Image img = pictureBox1.Image;
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);


                ImageAttributes ia = new ImageAttributes();
                ColorMatrix cmPicture = new ColorMatrix(new float[][]
                {
                    new float[] {.53f, .39f, 0, 0, 0},
                    new float[] {.769f+0.3f, .986f, .534f, 0, 0},
                    new float[] {.189f, .168f, 0, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });
                ia.SetColorMatrix(cmPicture);
                Graphics g = Graphics.FromImage(bmpInverted);

                g.DrawImage(img, new System.Drawing.Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
                g.Dispose();
                pictureBox1.Image = bmpInverted;

            }
        }



        void hue()
        {
            if (!opened)
            {
                MessageBox.Show("Open an image then apply changes");

            }
            else
            {

                Image img = pictureBox1.Image;
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);


                ImageAttributes ia = new ImageAttributes();
                ColorMatrix cmPicture = new ColorMatrix(new float[][]
                {
                    new float[] {1+(trackBar1.Value), 0, 0, 0, 0},
                    new float[] {0, 1+(trackBar2.Value), 0, 0, 0},
                    new float[] {0, 0, 1+(trackBar3.Value), 0, 0, },
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });
                ia.SetColorMatrix(cmPicture);
                Graphics g = Graphics.FromImage(bmpInverted);

                g.DrawImage(img, new System.Drawing.Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
                g.Dispose();
                pictureBox1.Image = bmpInverted;

            }
        }

        void reload()
        {
            if (!opened)
            {
                // MessageBox.Show("Open an ImageThen Apply Changes");
            }
            else
            {
                if (opened)
                {
                    file = Image.FromFile(openFileDialog1.FileName);
                    pictureBox1.Image = file;
                    opened = true;
                }
            }
        }

        void saveImage()
        {
            if (opened)
            {
                SaveFileDialog sfd = new SaveFileDialog(); // new save file dialog
                sfd.Filter = "Images |*.png;*.bmp;*.jpg";
                ImageFormat format = ImageFormat.Png; //store it by default format
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string ext = Path.GetExtension(sfd.FileName);
                    switch (ext)
                    {
                        case ".jpg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                    }
                    pictureBox1.Image.Save(sfd.FileName, format);
                }


            }
            else { MessageBox.Show("No Image Loaded, Please Upload Image"); }
        }

        Image file;
        Boolean opened = false; // to check if there is an existing image loaded or not

        private void button9_Click(object sender, EventArgs e)
        {
            openimage();
            pictureBox1.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            saveImage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reload();
            filter2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            reload();
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            trackBar3.Value = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reload();
            filter3();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Information info1 = new Information();
            info1.Show();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            hue();
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            hue();
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            hue();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("This Will Delete Any Unsaved Changes, Do You Wish To Continue?", "Do You Want To Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                pictureBox1.Refresh();
            }
            else
            {
                //this does nothing
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.AllowDrop = true;

            

        }

        private void filter1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void filterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void infromationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Information info = new Information();
            info.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openimage();
            pictureBox1.Visible = true;

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveImage();
        }

        private void FiltergreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filter2();
        }

        private void FilteryellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filter3();
        }

        private void Filter3ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox1_DragDrop(object sender, DragEventArgs e)
        {


            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                var fileNames = data as string[];
                if (fileNames.Length > 0)
                    pictureBox1.Image = Image.FromFile(fileNames[0]);
            }

        }

        private void PictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Rotate_180_Click(object sender, EventArgs e)
        {
            try
            {
                Image img = pictureBox1.Image;
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox1.Image = img;
            }
            catch
            {
                MessageBox.Show("Please Upload An Image First", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            old = e.Location;
            if (radioButton1.Checked)
                p.Width = 1;
            else if (radioButton2.Checked)
                p.Width = 5;
            else if (radioButton3.Checked)
                p.Width = 10;
            else if (radioButton4.Checked)
                p.Width = 15;
            else if (radioButton5.Checked)
                p.Width = 30;
            else if (radioButton6.Checked)
                p.Width = 40;
            else if (radioButton7.Checked)
                p.Width = 50;
            else if (radioButton8.Checked)
                p.Width = 60;
            else if (radioButton9.Checked)
                p.Width = 70;


        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    current = e.Location;
                    g.DrawLine(p, old, current);
                    old = current;
                }
                else
                {

                }
            }
            catch
            {
                /*stops from crashing if drawing fails */
            }

      
        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            p.Color = Color.Lime;
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            p.Color = Color.Red;
        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            p.Color = Color.Yellow;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            p.Color = Color.LightBlue;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            p.Color = Color.Blue;
        }

        private void Purple_Click(object sender, EventArgs e)
        {
            p.Color = Color.Purple;
        }

        private void Pen_Click(object sender, EventArgs e)
        {
     if (Pen.Checked == true)
            {
               
            }
        }

        private void Image_Erase_Click(object sender, EventArgs e)
        {
            if (pictureBox1.BackColor == Color.Black)
            {
                p.Color = Color.Black;
                p.Width = 50;
            }
            {
                if (pictureBox1.BackColor == Color.Red)
                {
                    p.Color = Color.Red;
                    p.Width = 50;
                }
                {
                    if (pictureBox1.BackColor == Color.Lime)
                    {
                        p.Color = Color.Lime;
                        p.Width = 50;
                    }

                }

            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                p.Color = cd.Color;
        }

        private void SaveNEWToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void ClockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Image img = pictureBox1.Image;
                img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                pictureBox1.Image = img;
            }
            catch
            {
                MessageBox.Show("Please Upload An Image First", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Dispose();
        }

        private void ResetImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Refresh();
                reload();
                trackBar1.Value = 0;
                trackBar2.Value = 0;
                trackBar3.Value = 0;
            }
            catch
            {
                //
            }
        }

        private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void RedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { pictureBox1.BackColor = Color.Red; } catch { }
        }

        private void BackBlack_Click(object sender, EventArgs e)
        {
            try { pictureBox1.BackColor = Color.Black; } catch { }
        }

        private void DarkBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { pictureBox1.BackColor = Color.DarkBlue; } catch { }

        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Windows.Forms.Application.ExitThread();
        }
    }
}
