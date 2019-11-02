using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Data;
using Rectangle = System.Drawing.Rectangle;
using System.Collections.Generic;
using System.Linq;
using Point = System.Drawing.Point;
using Advanced_Photo_Editor;


namespace Hines_Photo_Editor
{
    public partial class crop : Form
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

        //TEST SHAPES
        private List<APShape> shapes = new List<APShape>();
        enum Shapes { LINE, OVAL, BOX, NONE }
        private Shapes currentShape = Shapes.LINE;
        private bool trails = false;
        private bool drag = false;
        int x, y;
        Random randomGen = new Random();
        bool randColor = false;
        Color randomColor;
        int brushWidth = 4;
        Color userColor = Color.Black;
        ColorDialog cd = new ColorDialog();
        bool floodFillActive = false;



        public crop()
        {
            InitializeComponent();
            INIT();

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

        private Point firstPoint = new Point();

        public void INIT()
        {
            pictureBox3.MouseDown += (ss, ee) =>
            {
                if (ee.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    firstPoint = Control.MousePosition;
                }
            };
            pictureBox3.MouseMove += (ss, ee) =>
            {
                if (ee.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Point temp = Control.MousePosition;
                    Point res = new Point(firstPoint.X - temp.X, firstPoint.Y - temp.Y);

                    pictureBox3.Location = new Point(pictureBox3.Location.X - res.X, pictureBox3.Location.Y - res.Y);

                    firstPoint = temp;

                }
            };
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

        void openimage2()
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                file = Image.FromFile(openFileDialog1.FileName);
                pictureBox3.Image = file;
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
                trackBar1.Value = 0;
                trackBar2.Value = 0;
                trackBar3.Value = 0;

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
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            trackBar3.Value = 0;
            if (!drag) { shapes.Clear(); Refresh(); }

            pictureBox3.Location = new Point(1314, 530);

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
                pictureBox1.Image = null;
            }
            else
            {
                //this does nothing
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.AllowDrop = true;
            pictureBox3.AllowDrop = true;
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



        private void FiltergreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filter2();
        }

        private void FilteryellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filter3();
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
            Graphics g = e.Graphics;

            foreach (var item in shapes)
            {
                item.draw(e.Graphics);
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (trackBar4.Enabled == true)
            {
                //new code
                if (floodFillActive)
                {
                }
                else
                {
                    drag = true;
                    System.Drawing.Point startingPoint = e.Location;

                    x = startingPoint.X; //Creates the starting point
                    y = startingPoint.Y;

                    if (randColor) //Generates a random color if randColor
                    {
                        KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
                        KnownColor randomColorName = names[randomGen.Next(names.Length)];
                        randomColor = Color.FromKnownColor(randomColorName);
                    }

                    if (trails == false)
                    {
                        switch (currentShape) //Chooses which shape to create based on the current shape. Also chooses weather to paint it with random colors or a usercolor
                        {
                            case Shapes.LINE:
                                if (randColor) shapes.Add(new APLine(x, y, x, y, randomColor, brushWidth)); else { shapes.Add(new APLine(x, y, x, y, userColor, brushWidth)); };
                                break;
                            case Shapes.OVAL:
                                if (randColor) shapes.Add(new APOval(x, y, x, y, randomColor, brushWidth)); else { shapes.Add(new APOval(x, y, x, y, userColor, brushWidth)); };
                                break;
                            case Shapes.BOX:
                                if (randColor) shapes.Add(new APBox(x, y, x, y, randomColor, brushWidth)); else { shapes.Add(new APBox(x, y, x, y, userColor, brushWidth)); };
                                break;
                            default:
                                break;
                        }

                    }
                    else //If trails is true
                    {
                        switch (currentShape)
                        {
                            case Shapes.LINE:
                                if (randColor) shapes.Add(new APLine(x, y, x, y, randomColor, brushWidth)); else { shapes.Add(new APLine(x, y, x, y, userColor, brushWidth)); };
                                break;
                            case Shapes.OVAL:
                                if (randColor) shapes.Add(new APOval(x, y, x, y, randomColor, brushWidth)); else { shapes.Add(new APOval(x, y, x, y, userColor, brushWidth)); };
                                break;
                            case Shapes.BOX:
                                if (randColor) shapes.Add(new APBox(x, y, x, y, randomColor, brushWidth)); else { shapes.Add(new APBox(x, y, x, y, userColor, brushWidth)); };
                                break;
                            default:
                                break;

                        }


                    }
                }
            }
            else
            {
                //my code
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
        }


        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (trackBar4.Enabled == true)
            {
                //new code
                Point endingPoint = e.Location;

                int ex = endingPoint.X;
                int ey = endingPoint.Y;
                Shapes shape = currentShape;

                if (drag)
                {
                    if (!trails)
                    {
                        shapes.Last().setX2(ex);
                        shapes.Last().setY2(ey);
                    }
                    else
                    {
                        shapes.Last().setX2(ex);
                        shapes.Last().setY2(ey);
                        if (randColor)
                        {
                            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
                            KnownColor randomColorName = names[randomGen.Next(names.Length)];
                            randomColor = Color.FromKnownColor(randomColorName);
                        }

                        switch (shape)
                        {
                            case Shapes.LINE:
                                if (randColor) shapes.Add(new APLine(x, y, ex, ey, randomColor, brushWidth)); else { shapes.Add(new APLine(x, y, ex, ey, userColor, brushWidth)); };
                                break;
                            case Shapes.OVAL:
                                if (randColor) shapes.Add(new APOval(x, y, ex, ey, randomColor, brushWidth)); else { shapes.Add(new APOval(x, y, ex, ey, userColor, brushWidth)); };
                                break;
                            case Shapes.BOX:
                                if (randColor) shapes.Add(new APBox(x, y, ex, ey, randomColor, brushWidth)); else { shapes.Add(new APBox(x, y, ex, ey, userColor, brushWidth)); };
                                break;
                            default:
                                break;
                        }


                    }

                    Refresh();
                }
            }

            else
            {

                //my code
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
                int crpX, crpY, rectW, rectH;

            }
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
            //my code

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

            saveFileDialog1.FileName = "image";
            saveFileDialog1.DefaultExt = "";
            saveFileDialog1.Filter = "bmp images (*.bmp)|*.bmp";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

                pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0,
                    pictureBox1.Width, pictureBox1.Height));

                var fileName = saveFileDialog1.FileName;
                if (!System.IO.Path.HasExtension(fileName) || System.IO.Path.GetExtension(fileName) != "bmp")
                    fileName = fileName + ".bmp";

                bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
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

        private void APE_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar) //Determines what to do based on the key entered
            {
                case 'l': currentShape = Shapes.LINE; trackBar4.Value = 1; break;
                case 'o': currentShape = Shapes.OVAL; trackBar4.Value = 2; break;
                case 'r': currentShape = Shapes.BOX; trackBar4.Value = 3; break;
                case 't': if (trails == false) { trails = true; } else { trails = false; }; break;
                case 'c': if (!drag) { shapes.Clear(); Refresh(); } break;
                case 'a': if (randColor == false) { randColor = true; } else { randColor = false; } break;
                case 'u': cd.ShowDialog(); userColor = cd.Color; randColor = false; break;
                case 'z': if (shapes.Count >= 1 && !drag) { shapes.Remove(shapes.Last()); Refresh(); }; break;
            }
        }




        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (trackBar4.Enabled == true)
            {
                drag = false;
                Refresh();
            }

            else
            {
                //
            }


            //crop


        }

        private void HelpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HelpPage help = new HelpPage();
            help.Show();
        }

        private void LineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trackBar4.Value = 1;
        }

        private void SquareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trackBar4.Value = 2;
        }

        private void SquareToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            trackBar4.Value = 3;
        }

        private void ModifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trackBar4.Value = 4;
        }



        private void Circle_Click(object sender, EventArgs e)
        {
            HelpPage help = new HelpPage();
            help.Show();
        }



        private void DisableToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            trackBar4.Enabled = false;

        }

        private void ImageRotation_Tick(object sender, EventArgs e)
        {

        }

        private void ImageRotateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ImageRotateComboBox.SelectedItem == "Right")
            {
                try
                {
                    Image img = pictureBox1.Image;
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    pictureBox1.Image = img;
                }
                catch
                {
                    //
                }
            }
            else if (ImageRotateComboBox.SelectedItem == "Left")
            {
                try
                {
                    Image img = pictureBox1.Image;
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    pictureBox1.Image = img;
                }
                catch
                {
                    //
                }
            }
        }

        private void EnableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trackBar4.Enabled = true;
        }

        private void PictureBox3_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                var fileNames = data as string[];
                if (fileNames.Length > 0)
                    pictureBox3.Image = Image.FromFile(fileNames[0]);
            }
        }

        private void PictureBox3_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;

        }

        private void Loadimg_Click(object sender, EventArgs e)
        {
            openimage2();
        }

        private void ClearImg_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox3.Image = null;
                pictureBox3.Location = new Point(1314, 530);
                pictureBox3.BorderStyle = BorderStyle.Fixed3D;
            }
            catch
            {

            }
        }

        private void PictureBox3_DoubleClick(object sender, EventArgs e)
        {
            pictureBox3.BorderStyle = BorderStyle.None;
            pictureBox3.BackColor = Color.Transparent;
            try
            {
                pictureBox3.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBox3.Image = Image.FromFile(openFileDialog1.FileName);

                helper.BlendPictures(pictureBox1, pictureBox3);
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch
            {
                //
            }
       

    }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
        }

        private void SetImageFilter_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == "Blur Image")
            {

            }
            else
            {
                //
            }
        }

        private void PB2Background_Tick(object sender, EventArgs e)
        {
        
        }

        private void Transparent_Click(object sender, EventArgs e)
        {
            try
            {

                pictureBox3.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBox3.Image = Image.FromFile(openFileDialog1.FileName);

                helper.BlendPictures(pictureBox1, pictureBox3);
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch
            {
                //
            }

        }




    private void TrackBar4_ValueChanged(object sender, EventArgs e)
        {
            switch (trackBar4.Value)
            {
                case 1: currentShape = Shapes.LINE; break;
                case 2: currentShape = Shapes.OVAL; break;
                case 3: currentShape = Shapes.BOX; break;
                case 4: currentShape = Shapes.NONE; break;
            }
        }


    }
}

