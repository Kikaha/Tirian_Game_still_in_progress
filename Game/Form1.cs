﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class Form1 : Form
    {
        PictureBox[] stars;
        int backgroundspeed;
        int playerSpeed;
        Random rmd;


        PictureBox[] munitions;
        int munitionsSpeed;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            backgroundspeed = 4;
            playerSpeed = 4;
            munitionsSpeed = 20;
            stars = new PictureBox[15];
            rmd = new Random();
            munitions = new PictureBox[1];

            Image munition = Image.FromFile(@"asserts\munition.png");

            for (int i = 0; i < munitions.Length; i++)
            {
                munitions[i] = new PictureBox();
                munitions[i].Size = new Size(8,8);
                munitions[i].Image = munition;
                munitions[i].SizeMode = PictureBoxSizeMode.Zoom;
                munitions[i].BorderStyle = BorderStyle.None;
                this.Controls.Add(munitions[i]);
            }


            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new PictureBox();
                stars[i].BorderStyle = BorderStyle.None;
                stars[i].Location = new Point(rmd.Next(20, 580), rmd.Next(-10, 400));
                if (i % 2 == 1)
                {
                    stars[i].Size = new Size(2, 2);
                    stars[i].BackColor = Color.White;
                }
                else
                {
                    stars[i].Size = new Size(3, 3);
                    stars[i].BackColor = Color.Red;
                }
                this.Controls.Add(stars[i]);
            }

        }
        private void MoveBackground_Tick(object sender, EventArgs e)
        {

            for (int i = 0; i < stars.Length / 2; i++)
            {
                stars[i].Top += backgroundspeed;
                if (stars[i].Top >= this.Height)
                {
                    stars[i].Top = -stars[i].Height;
                }
            }
            for (int i = stars.Length / 2; i < stars.Length; i++)
            {
                stars[i].Top += backgroundspeed - 2;
                if (stars[i].Top >= this.Height)
                {
                    stars[i].Top = -stars[i].Height;
                }
            }
        }

        private void LeftMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Left>10)
            {
                Player.Left -= playerSpeed;
            }
        }

        private void RightMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Right<580)
            {
                Player.Left += playerSpeed;
            }
        }

        private void UpMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Top < 400)
            {
                Player.Top -= playerSpeed;
            }
        }

        private void DownMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Top > 10)
            {
                Player.Top += playerSpeed;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            RightMoveTimer.Stop();
            LeftMoveTimer.Stop();
            UpMoveTimer.Stop();
            DownMoveTimer.Stop();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== Keys.Right)
            {
                RightMoveTimer.Start();
            }
            else if (e.KeyCode == Keys.Left)
            {
                LeftMoveTimer.Start();
            }
            else if (e.KeyCode == Keys.Up)
            {
                UpMoveTimer.Start();
            }
            else if (e.KeyCode == Keys.Down)
            {
                DownMoveTimer.Start();
            }
        }

        private void MoveMunitionTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < munitions.Length; i++)
            {
                if (munitions[i].Top>0)
                {
                    munitions[i].Visible = true;
                    munitions[i].Top -= munitionsSpeed;
                }
                else
                {
                    munitions[i].Visible = false;
                    munitions[i].Location = new Point(Player.Location.X + 20, Player.Location.Y - i * 30);
                }

            }
        }
    }
}
