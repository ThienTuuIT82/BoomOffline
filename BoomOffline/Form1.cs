using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using WMPLib;
using System.Media;

namespace BoomOffline
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        WindowsMediaPlayer playSound = new WindowsMediaPlayer();
        System.IO.StreamReader file;
        System.IO.StreamWriter file1;
        Label txtMsg = new Label();
        Button btRestart = new Button();
        Button btReturn = new Button();
        int playerSpeed = 5;
        int playerSpeed2 = 5;
        int monsterSpeed = 3;
        int bomAmount1 = 1;
        int bomAmount2 = 1;
        int bomSize1 = 1;
        int bomSize2 = 1;
        private int[,] table = new int[11, 11];
        PictureBox player = new PictureBox();
        PictureBox[] monster = new PictureBox[3];
        Label namePlayer = new Label();
        PictureBox player2 = new PictureBox();
        Label namePlayer2 = new Label();
        bool goUp = false, goDown = false, goLeft = false, goRight = false;
        bool[] mgoUp = new bool[3], mgoDown = new bool[3], mgoLeft = new bool[3], mgoRight = new bool[3];
        String level = "man1";
        int countMonster = 0;
        bool goUp2 = false, goDown2 = false, goLeft2 = false, goRight2 = false;
        double sec = 0;
        PictureBox[] bomCountDown1 = new PictureBox[5];
        double[] bc1 = new double[5];
        int ba1 = 0;
        PictureBox[] bomCountDown2 = new PictureBox[5];
        double[] bc2 = new double[5];
        int ba2 = 0;
        Form death = new Form();
        Random r = new Random();
        bool endGame = false;
        bool menu = true;
        bool twoPlayer = false;
        bool[] isWalking = new bool[3];
        int[] vision = new int[3];
        DateTime date;
        int allTime = -1;
        private void Run()
        {
            this.Controls.Clear();
            twoPlayer = false;
            goUp = false; goDown = false; goLeft = false; goRight = false;
            for (int i = 0; i < 3; i++)
            {
                monster[i] = new PictureBox();
                mgoUp[i] = false; mgoDown[i] = false; mgoLeft[i] = false; mgoRight[i] = false; isWalking[i] = false; vision[i] = new int();
            }
            countMonster = 0;
            goUp2 = false; goDown2 = false; goLeft2 = false; goRight2 = false;
            sec = 0;
            ba1 = 0;
            ba2 = 0;
            playerSpeed = 5;
            playerSpeed2 = 5;
            bomAmount1 = 1;
            bomAmount2 = 1;
            bomSize1 = 1;
            bomSize2 = 1;
            for (int i = 0; i < 5; i++)
            {
                bomCountDown1[i] = null;
                bomCountDown2[i] = null;
                bc1[i] = 0;
                bc2[i] = 0;
            }
        }
        private void SoundEffect(string name) 
        {
            WindowsMediaPlayer SoundEffect = new WindowsMediaPlayer();
            SoundEffect.URL = Application.StartupPath + @"\sounds\" + name + ".wav";
            SoundEffect.controls.play();
        }
        private void OnePlayer()
        {
            SoundEffect("start");

            playSound.URL = Application.StartupPath + @"\sounds\soundGame.wav";
            playSound.settings.setMode("Loop", true);
            playSound.controls.play();
            if (level == "man1")
            {
                allTime = 0;
                date = DateTime.Now;
            }
            menu = false;
            string line;
            int j = 0;
            this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\image\background.jpg");
                if(level == "man1")
                    file = new System.IO.StreamReader(Application.StartupPath + @"\map1.txt");
                else if (level == "man2")
                    file = new System.IO.StreamReader(Application.StartupPath + @"\map2.txt");
                else if (level == "man3")
                    file = new System.IO.StreamReader(Application.StartupPath + @"\map3.txt");
            while ((line = file.ReadLine()) != null)
            {

                char[] charArr = line.ToCharArray();
                int i = 0;
                foreach (char ch in charArr)
                {
                    table[j, i] = int.Parse(ch.ToString());
                    if (table[j, i] != 0)
                    {
                        PictureBox px = new PictureBox();
                        px.Width = 55;
                        px.Height = 55;
                        px.Top = j * 60;
                        px.Left = i * 60;
                        if ((table[j, i] = int.Parse(ch.ToString())) == 1)
                        {
                            px.Tag = "box";
                            px.Image = Image.FromFile(Application.StartupPath + @"\image\wood.png");
                        }
                        if ((table[j, i] = int.Parse(ch.ToString())) == 2)
                        {
                            px.Tag = "wall";
                            px.Image = Image.FromFile(Application.StartupPath + @"\image\da1.png");
                        }
                        if ((table[j, i] = int.Parse(ch.ToString())) == 3)
                        {
                            px.Tag = "wall";
                            px.Image = Image.FromFile(Application.StartupPath + @"\image\goccay1.png");
                        }
                        this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                        px.BackColor = Color.Transparent;
                        px.SizeMode = PictureBoxSizeMode.StretchImage;
                        //table[j, i] = 0;
                        this.Controls.Add(px);
                    }
                    else
                    {
                        if (level == "man1")
                        {
                            countMonster = 1;
                            if (i == 1 && j == 9)
                            {
                                player.Width = 55;
                                player.Height = 55;
                                player.Top = j * 60;
                                player.Left = i * 60;
                                player.Name = "player1";
                                player.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_down.png");
                                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                                player.BackColor = Color.Transparent;
                                player.SizeMode = PictureBoxSizeMode.StretchImage;
                                namePlayer.Left = player.Left;
                                namePlayer.Top = player.Top - 20;
                                namePlayer.BackColor = Color.Transparent;
                                namePlayer.Text = "    YOU";
                                namePlayer.ForeColor = Color.Yellow;
                                namePlayer.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                                namePlayer.Size = new Size(66, 12);
                                this.Controls.Add(player);
                                this.Controls.Add(namePlayer);
                                namePlayer.BringToFront();
                            }
                            if (i == 9 && j == 1)
                            {
                                monster[0].Width = 55;
                                monster[0].Height = 55;
                                monster[0].Top = j * 60;
                                monster[0].Left = i * 60;
                                monster[0].Name = "monster1";
                                monster[0].Image = Image.FromFile(Application.StartupPath + @"\image\monster_down.png");
                                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                                monster[0].BackColor = Color.Transparent;
                                monster[0].SizeMode = PictureBoxSizeMode.StretchImage;
                                this.Controls.Add(monster[0]);
                            }
                        }
                        if (level == "man2")
                        {
                            countMonster = 2;
                            if (i == 1 && j == 9)
                            {
                                player.Width = 55;
                                player.Height = 55;
                                player.Top = j * 60;
                                player.Left = i * 60;
                                player.Name = "player1";
                                player.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_down.png");
                                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                                player.BackColor = Color.Transparent;
                                player.SizeMode = PictureBoxSizeMode.StretchImage;
                                namePlayer.Left = player.Left;
                                namePlayer.Top = player.Top - 20;
                                namePlayer.BackColor = Color.Transparent;
                                namePlayer.Text = "    YOU";
                                namePlayer.ForeColor = Color.Yellow;
                                namePlayer.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                                namePlayer.Size = new Size(66, 12);
                                this.Controls.Add(player);
                                this.Controls.Add(namePlayer);
                                namePlayer.BringToFront();
                            }
                            if (i == 9 && j == 1)
                            {
                                monster[0].Width = 55;
                                monster[0].Height = 55;
                                monster[0].Top = j * 60;
                                monster[0].Left = i * 60;
                                monster[0].Name = "monster1";
                                monster[0].Image = Image.FromFile(Application.StartupPath + @"\image\monster_down.png");
                                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                                monster[0].BackColor = Color.Transparent;
                                monster[0].SizeMode = PictureBoxSizeMode.StretchImage;
                                this.Controls.Add(monster[0]);
                            }
                            if (i == 1 && j == 1)
                            {
                                monster[1].Width = 55;
                                monster[1].Height = 55;
                                monster[1].Top = j * 60;
                                monster[1].Left = i * 60;
                                monster[1].Name = "monster2";
                                monster[1].Image = Image.FromFile(Application.StartupPath + @"\image\monster_down.png");
                                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                                monster[1].BackColor = Color.Transparent;
                                monster[1].SizeMode = PictureBoxSizeMode.StretchImage;
                                this.Controls.Add(monster[1]);
                            }
                        }
                        if (level == "man3") 
                        {
                            countMonster = 3;
                            if (i == 1 && j == 9)
                            {
                                player.Width = 55;
                                player.Height = 55;
                                player.Top = j * 60;
                                player.Left = i * 60;
                                player.Name = "player1";
                                player.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_down.png");
                                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                                player.BackColor = Color.Transparent;
                                player.SizeMode = PictureBoxSizeMode.StretchImage;
                                namePlayer.Left = player.Left;
                                namePlayer.Top = player.Top - 20;
                                namePlayer.BackColor = Color.Transparent;
                                namePlayer.Text = "    YOU";
                                namePlayer.ForeColor = Color.Yellow;
                                namePlayer.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                                namePlayer.Size = new Size(66, 12);
                                this.Controls.Add(player);
                                this.Controls.Add(namePlayer);
                                namePlayer.BringToFront();
                            }
                            if (i == 9 && j == 1)
                            {
                                monster[0].Width = 55;
                                monster[0].Height = 55;
                                monster[0].Top = j * 60;
                                monster[0].Left = i * 60;
                                monster[0].Name = "monster1";
                                monster[0].Image = Image.FromFile(Application.StartupPath + @"\image\monster_down.png");
                                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                                monster[0].BackColor = Color.Transparent;
                                monster[0].SizeMode = PictureBoxSizeMode.StretchImage;
                                this.Controls.Add(monster[0]);
                            }
                            if (i == 9 && j == 9)
                            {
                                monster[1].Width = 55;
                                monster[1].Height = 55;
                                monster[1].Top = j * 60;
                                monster[1].Left = i * 60;
                                monster[1].Name = "monster2";
                                monster[1].Image = Image.FromFile(Application.StartupPath + @"\image\monster_down.png");
                                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                                monster[1].BackColor = Color.Transparent;
                                monster[1].SizeMode = PictureBoxSizeMode.StretchImage;
                                this.Controls.Add(monster[1]);
                            }
                            if (i == 1 && j == 1)
                            {
                                monster[2].Width = 55;
                                monster[2].Height = 55;
                                monster[2].Top = j * 60;
                                monster[2].Left = i * 60;
                                monster[2].Name = "monster3";
                                monster[2].Image = Image.FromFile(Application.StartupPath + @"\image\monster_down.png");
                                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                                monster[2].BackColor = Color.Transparent;
                                monster[2].SizeMode = PictureBoxSizeMode.StretchImage;
                                this.Controls.Add(monster[2]);
                            }
                        }
                    }
                    i++;
                }
                j++;
            }
            file.Close();
            timer1.Start();
            timer2.Start();
        }
        private void TwoPlayer()
        {
            SoundEffect("start");
            playSound.URL = Application.StartupPath + @"\sounds\soundMenu.wav";
            playSound.settings.setMode("Loop", true);
            playSound.controls.play();
            date = DateTime.Now;
            twoPlayer = true;
            menu = false;
            string line;
            int j = 0;
            this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\image\background.jpg");
            System.IO.StreamReader file =
               new System.IO.StreamReader(Application.StartupPath + @"\map.txt");
            while ((line = file.ReadLine()) != null)
            {

                char[] charArr = line.ToCharArray();
                int i = 0;
                foreach (char ch in charArr)
                {
                    table[j, i] = int.Parse(ch.ToString());
                    if (table[j, i] != 0)
                    {
                        PictureBox px = new PictureBox();
                        px.Width = 55;
                        px.Height = 55;
                        px.Top = j * 60;
                        px.Left = i * 60;
                        if ((table[j, i] = int.Parse(ch.ToString())) == 1)
                        {
                            px.Tag = "box";
                            px.Image = Image.FromFile(Application.StartupPath + @"\image\wood.png");
                        }
                        if ((table[j, i] = int.Parse(ch.ToString())) == 2)
                        {
                            px.Tag = "wall";
                            px.Image = Image.FromFile(Application.StartupPath + @"\image\da1.png");
                        }
                        if ((table[j, i] = int.Parse(ch.ToString())) == 3)
                        {
                            px.Tag = "wall";
                            px.Image = Image.FromFile(Application.StartupPath + @"\image\goccay1.png");
                        }
                        this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                        px.BackColor = Color.Transparent;
                        px.SizeMode = PictureBoxSizeMode.StretchImage;
                        this.Controls.Add(px);
                    }
                    else
                    {
                        if (i == 1 && j == 9)
                        {
                            player.Width = 55;
                            player.Height = 55;
                            player.Top = j * 60;
                            player.Left = i * 60;
                            player.Name = "player1";
                            player.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_down.png");
                            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                            player.BackColor = Color.Transparent;
                            player.SizeMode = PictureBoxSizeMode.StretchImage;
                            namePlayer.Left = player.Left;
                            namePlayer.Top = player.Top - 20;
                            namePlayer.BackColor = Color.Transparent;
                            namePlayer.Text = "PLAYER 1";
                            namePlayer.ForeColor = Color.Yellow;
                            namePlayer.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                            namePlayer.Size = new Size(66, 12);
                            this.Controls.Add(player);
                            this.Controls.Add(namePlayer);
                            namePlayer.BringToFront();
                        }
                        if (i == 9 && j == 1)
                        {
                            player2.Width = 55;
                            player2.Height = 55;
                            player2.Top = j * 60;
                            player2.Left = i * 60;
                            player2.Name = "player2";
                            player2.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_down.png");
                            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                            player2.BackColor = Color.Transparent;
                            player2.SizeMode = PictureBoxSizeMode.StretchImage;
                            namePlayer2.Location = player2.Location;
                            namePlayer2.BackColor = Color.Transparent;
                            namePlayer2.Text = "PLAYER 2";
                            namePlayer2.ForeColor = Color.Yellow;
                            namePlayer2.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                            namePlayer2.Size = new Size(66, 12);
                            this.Controls.Add(player2);
                            this.Controls.Add(namePlayer2);
                            namePlayer2.BringToFront();
                        }
                    }
                    i++;
                }
                j++;
            }
            file.Close();
            timer1.Start();
            timer2.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //namePlayer.Location = new Point(player.Left, player.Top - 20
            if (twoPlayer)
            {
                int x = player.Left, y = player.Top, x2 = player2.Left, y2 = player2.Top;

                if (goUp)
                {
                    if (player.Top > 0)
                        //player.Location = new Point(player.Left, player.Top - playerSpeed);
                        y -= playerSpeed;
                }
                if (goDown)
                {
                    if (player.Top <= 10 * 60 + 10)
                        //player.Location = new Point(player.Left, player.Top + playerSpeed);
                        y += playerSpeed;
                }
                if (goLeft)
                {
                    if (player.Left > 0)
                        //player.Location = new Point(player.Left - playerSpeed, player.Top);
                        x -= playerSpeed;
                }
                if (goRight)
                {
                    if (player.Left <= 10 * 60 + 10)
                        //player.Location = new Point(player.Left + playerSpeed, player.Top);
                        x += playerSpeed;
                }
                if (goUp2)
                {
                    if (player2.Top > 0)
                        //player2.Location = new Point(player2.Left, player2.Top - playerSpeed);
                        y2 -= playerSpeed2;
                }
                if (goDown2)
                {
                    if (player2.Top <= 10 * 60 + 10)
                        //player2.Location = new Point(player2.Left, player2.Top + playerSpeed);
                        y2 += playerSpeed2;
                }
                if (goLeft2)
                {
                    if (player2.Left > 0)
                        //player2.Location = new Point(player2.Left - playerSpeed, player2.Top);
                        x2 -= playerSpeed2;
                }
                if (goRight2)
                {
                    if (player2.Left <= 10 * 60 + 10)
                        //player2.Location = new Point(player2.Left + playerSpeed, player2.Top);
                        x2 += playerSpeed2;
                }
                player.Location = new Point(x, y);
                namePlayer.Location = new Point(x - 5, y - 12);
                player2.Location = new Point(x2, y2);
                namePlayer2.Location = new Point(x2 - 5, y2 - 12);

                TouchWall();
            }
            else 
            {
                int x = player.Left, y = player.Top;

                if (goUp)
                {
                    if (player.Top > 0)
                        //player.Location = new Point(player.Left, player.Top - playerSpeed);
                        y -= playerSpeed;
                }
                if (goDown)
                {
                    if (player.Top <= 10 * 60 + 10)
                        //player.Location = new Point(player.Left, player.Top + playerSpeed);
                        y += playerSpeed;
                }
                if (goLeft)
                {
                    if (player.Left > 0)
                        //player.Location = new Point(player.Left - playerSpeed, player.Top);
                        x -= playerSpeed;
                }
                if (goRight)
                {
                    if (player.Left <= 10 * 60 + 10)
                        //player.Location = new Point(player.Left + playerSpeed, player.Top);
                        x += playerSpeed;
                }
                player.Location = new Point(x, y);
                namePlayer.Location = new Point(x - 5, y - 12);

                TouchWall();
                if (level == "man1")
                    MonsterMove(ref monster[0],ref isWalking[0],ref vision[0],ref mgoUp[0],ref mgoDown[0],ref mgoLeft[0],ref mgoRight[0]);
                if (level == "man2")
                    for(int i = 0; i<=1;i++)
                        MonsterMove(ref monster[i], ref isWalking[i], ref vision[i], ref mgoUp[i], ref mgoDown[i], ref mgoLeft[i], ref mgoRight[i]);
                if (level == "man3")
                    for (int i = 0; i <= 2; i++)
                        MonsterMove(ref monster[i], ref isWalking[i], ref vision[i], ref mgoUp[i], ref mgoDown[i], ref mgoLeft[i], ref mgoRight[i]);
            }

        }
        private void MonsterMove(ref PictureBox monster,ref bool isWalking,ref int vision,ref bool mgoUp,ref bool mgoDown,ref bool mgoLeft,ref bool mgoRight) 
        {
            int xl = (monster.Left / 60 - 1);
            int xr = (monster.Left / 60 + 1);
            int yu = (monster.Top / 60 - 1);
            int yd = (monster.Top / 60 + 1);

            if (!isWalking)
            {
                vision = r.Next(1, 5);
                switch (vision)
                {
                    case 1:
                        mgoUp = true;
                        break;
                    case 2:
                        mgoDown = true;
                        break;
                    case 3:
                        mgoLeft = true;
                        break;
                    case 4:
                        mgoRight = true;
                        break;
                }
                isWalking = true;
            }
            else
            {
                if (mgoUp)
                {
                    if (monster.Top > 0)
                    {
                        monster.Top -= monsterSpeed;
                        monster.Image = Image.FromFile(Application.StartupPath + @"\image\monster_up.png");

                    }
                    else
                    {
                        mgoUp = false;
                        isWalking = false;
                    }
                }
                if (mgoDown)
                {
                    if (monster.Top < 10 * 60)
                    {
                        monster.Top += monsterSpeed;
                        monster.Image = Image.FromFile(Application.StartupPath + @"\image\monster_down.png");

                    }
                    else
                    {
                        mgoDown = false;
                        isWalking = false;
                    }
                }
                if (mgoLeft)
                {
                    if (monster.Left > 0)
                    {
                        monster.Left -= monsterSpeed;
                        monster.Image = Image.FromFile(Application.StartupPath + @"\image\monster_left.png");

                    }
                    else
                    {
                        mgoLeft = false;
                        isWalking = false;
                    }
                }
                if (mgoRight)
                {
                    if (monster.Left < 10 * 60)
                    {
                        monster.Left += monsterSpeed;
                        monster.Image = Image.FromFile(Application.StartupPath + @"\image\monster_right.png");

                    }
                    else
                    {
                        mgoRight = false;
                        isWalking = false;
                    }
                }
                foreach (PictureBox wall in this.Controls.OfType<PictureBox>())
                {
                    if ((String)wall.Tag == "wall" || (String)wall.Tag == "box" || (String)wall.Tag == "boom")
                    {
                        if (wall.Location.Y < monster.Location.Y)
                        {
                            if (monster.Bounds.IntersectsWith(wall.Bounds) && mgoUp)
                            {
                                mgoUp = false;
                                isWalking = false;
                                monster.Location = new Point(monster.Left, wall.Top + monster.Height);

                                //player.Top = x.Top + player.Height;
                            }
                            monster.BringToFront();
                        }
                        if (wall.Location.Y > monster.Location.Y)
                        {
                            if (monster.Bounds.IntersectsWith(wall.Bounds) && mgoDown)
                            {

                                mgoDown = false;
                                isWalking = false;
                                monster.Location = new Point(monster.Left, wall.Top - monster.Height);

                                //player.Top = x.Top - player.Height;

                            }
                            monster.BringToFront();
                        }
                        if (wall.Location.X < monster.Location.X)
                        {
                            if (monster.Bounds.IntersectsWith(wall.Bounds) && mgoLeft)
                            {
                                mgoLeft = false;
                                isWalking = false;
                                monster.Location = new Point(wall.Left + monster.Width, monster.Top);

                                //player.Left = x.Left + player.Width;
                            }
                            monster.BringToFront();
                        }
                        if (wall.Location.X > monster.Location.X)
                        {
                            if (monster.Bounds.IntersectsWith(wall.Bounds) && mgoRight)
                            {
                                mgoRight = false;
                                isWalking = false;
                                monster.Location = new Point(wall.Left - monster.Width, monster.Top);
                                //player.Left = x.Left - player.Width;
                            }
                            monster.BringToFront();
                        }
                    }
                }
                if (player.Bounds.IntersectsWith(monster.Bounds))
                {
                    SoundEffect("touch");
                    this.Controls.Remove(player);
                    timer1.Stop();
                    timer2.Stop();
                    luuDiem();
                    formDeath("You Lose", 1);
                    level = "man1";
                }
            }
        }
        private void TouchWall()
        {
            foreach (Control x in this.Controls.OfType<PictureBox>())
            {
                if ((String)x.Tag == "size")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (bomSize1 < 4)
                            bomSize1 += 1;
                        this.Controls.Remove(x);
                        SoundEffect("item");
                    }
                    if (twoPlayer)
                    if (player2.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (bomSize2 < 4)
                            bomSize2 += 1;
                        this.Controls.Remove(x);
                            SoundEffect("item");
                        }
                }
                if ((String)x.Tag == "amount")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (bomAmount1 < 5)
                            bomAmount1 += 1;
                        this.Controls.Remove(x);
                        SoundEffect("item");
                    }
                    if (twoPlayer)
                    if (player2.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (bomAmount2 < 5)
                            bomAmount2 += 1;
                        this.Controls.Remove(x);
                            SoundEffect("item");
                        }
                }
                if ((String)x.Tag == "speed")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (playerSpeed < 9)
                            playerSpeed += 1;
                        this.Controls.Remove(x);
                        SoundEffect("item");
                    }
                    if (twoPlayer)
                    if (player2.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (playerSpeed2 < 9)
                            playerSpeed2 += 1;
                        this.Controls.Remove(x);
                            SoundEffect("item");
                        }
                }
                if ((String)x.Tag == "wall" || (String)x.Tag == "box" || (String)x.Tag == "boom")
                {
                    if (x.Location.Y < player.Location.Y)
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && goUp)
                        {
                            goUp = false;
                            player.Location = new Point(player.Left, x.Top + player.Height);

                            //player.Top = x.Top + player.Height;
                        }
                        player.BringToFront();
                    }
                    if (x.Location.Y > player.Location.Y)
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && goDown)
                        {

                            goDown = false;
                            player.Location = new Point(player.Left, x.Top - player.Height);

                            //player.Top = x.Top - player.Height;

                        }
                        player.BringToFront();
                    }
                    if (x.Location.X < player.Location.X)
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && goLeft)
                        {
                            goLeft = false;
                            player.Location = new Point(x.Left + player.Width, player.Top);

                            //player.Left = x.Left + player.Width;
                        }
                        player.BringToFront();
                    }
                    if (x.Location.X > player.Location.X)
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && goRight)
                        {
                            goRight = false;
                            player.Location = new Point(x.Left - player.Width, player.Top);
                            //player.Left = x.Left - player.Width;
                        }
                        player.BringToFront();
                    }
                    //Player2
                    if (twoPlayer)
                    {
                        if (x.Location.Y < player2.Location.Y)
                        {
                            if (player2.Bounds.IntersectsWith(x.Bounds) && goUp2)
                            {
                                goUp2 = false;
                                player2.Location = new Point(player2.Left, x.Top + player2.Height);

                                //player.Top = x.Top + player.Height;
                            }
                            player2.BringToFront();
                        }
                        if (x.Location.Y > player2.Location.Y)
                        {
                            if (player2.Bounds.IntersectsWith(x.Bounds) && goDown2)
                            {

                                goDown2 = false;
                                player2.Location = new Point(player2.Left, x.Top - player2.Height);

                                //player.Top = x.Top - player.Height;

                            }
                            player2.BringToFront();
                        }
                        if (x.Location.X < player2.Location.X)
                        {
                            if (player2.Bounds.IntersectsWith(x.Bounds) && goLeft2)
                            {
                                goLeft2 = false;
                                player2.Location = new Point(x.Left + player2.Width, player2.Top);

                                //player.Left = x.Left + player.Width;
                            }
                            player2.BringToFront();
                        }
                        if (x.Location.X > player2.Location.X)
                        {
                            if (player2.Bounds.IntersectsWith(x.Bounds) && goRight2)
                            {
                                goRight2 = false;
                                player2.Location = new Point(x.Left - player2.Width, player2.Top);
                                //player.Left = x.Left - player.Width;
                            }
                            player2.BringToFront();
                        }
                    }
                }
            }
        }
        private void MainMenu()
        {
            playSound.URL = Application.StartupPath + @"\sounds\soundMenu.wav";
            playSound.settings.setMode("Loop", true);
            playSound.controls.play();
            ListBox lsu1Player = new ListBox();
            ListBox lsu2Player = new ListBox();
            Label playertxt = new Label();
            Label playertxt2 = new Label();
            PictureBox start1 = new PictureBox();
            PictureBox start2 = new PictureBox();
            PictureBox info = new PictureBox();
            PictureBox lichsu = new PictureBox();
            PictureBox leave = new PictureBox();
            PictureBox infoPic = new PictureBox();
            menu = true;
            this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\image\giaodien.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            start1.Left = 120;
            start1.Top = 470;
            start1.Height = 50;
            start1.Width = 210;
            start1.BackColor = Color.Transparent;
            start1.Image = Image.FromFile(Application.StartupPath + @"\image\button_player.png");
            start1.MouseHover += (s, e) =>
            {
                start1.Image = Image.FromFile(Application.StartupPath + @"\image\button_player (1).png");
            };
            start1.MouseLeave += (s, e) =>
            {
                start1.Image = Image.FromFile(Application.StartupPath + @"\image\button_player.png");
            };
            start1.Click += (s, e) => {
                SoundEffect("click");
                Run(); OnePlayer(); };
            this.Controls.Add(start1);
            this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\image\giaodien.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            start2.Left = 340;
            start2.Top = 470;
            start2.Height = 50;
            start2.Width = 210;
            start2.BackColor = Color.Transparent;
            start2.Image = Image.FromFile(Application.StartupPath + @"\image\button_player (2).png");
            start2.MouseHover += (s, e) =>
            {
                start2.Image = Image.FromFile(Application.StartupPath + @"\image\button_player (3).png");
            };
            start2.MouseLeave += (s, e) =>
            {
                start2.Image = Image.FromFile(Application.StartupPath + @"\image\button_player (2).png");
            };
            start2.Click += (s, e) => {
                SoundEffect("click");
                Run(); TwoPlayer(); };
            this.Controls.Add(start2);
            infoPic.Left = 22;
            infoPic.Top = 10;
            infoPic.Height = 450;
            infoPic.Width = 620;
            info.Name = "HuongDan";
            infoPic.BackColor = Color.Transparent;
            infoPic.SizeMode = PictureBoxSizeMode.StretchImage;
            infoPic.Image = Image.FromFile(Application.StartupPath + @"\image\huong-dan.jpg");
            this.Controls.Add(infoPic);
            infoPic.Visible = false;
            info.Left = 120;
            info.Top = 530;
            info.Height = 50;
            info.Width = 210;
            info.BackColor = Color.Transparent;
            info.Image = Image.FromFile(Application.StartupPath + @"\image\button_huong-dan.png");
            info.MouseHover += (s, e) =>
            {
                info.Image = Image.FromFile(Application.StartupPath + @"\image\button_huong-dan (1).png");
            };
            info.MouseLeave += (s, e) =>
            {
                info.Image = Image.FromFile(Application.StartupPath + @"\image\button_huong-dan.png");
            };
            lichsu.Left = 340;
            lichsu.Top = 530;
            lichsu.Height = 50;
            lichsu.Width = 210;
            lichsu.BackColor = Color.Transparent;
            lichsu.Image = Image.FromFile(Application.StartupPath + @"\image\button_lich-su.png");
            lichsu.MouseHover += (s, e) =>
            {
                lichsu.Image = Image.FromFile(Application.StartupPath + @"\image\button_lich-su (1).png");
            };
            lichsu.MouseLeave += (s, e) =>
            {
                lichsu.Image = Image.FromFile(Application.StartupPath + @"\image\button_lich-su.png");
            };
            String line;
            lsu1Player.Left = 22;
            lsu1Player.Top = 40;
            lsu1Player.Height = 200;
            lsu1Player.Width = 620;
            lsu1Player.Font = new Font("Arial", 12, FontStyle.Regular);
            playertxt.Font = new Font("Arial", 15, FontStyle.Bold);
            playertxt.Text = "ONE PLAYER";
            playertxt.ForeColor = Color.Purple;
            playertxt.BackColor = Color.Aqua;
            playertxt.Left = 22;
            playertxt.Top = 10;
            playertxt.Width = 135;
            playertxt.Height = 30;
            playertxt.TextAlign = ContentAlignment.MiddleCenter;
            lsu2Player.Left = 22;
            lsu2Player.Top = 10 + 225 + 30;
            lsu2Player.Height = 200;
            lsu2Player.Width = 620;
            lsu2Player.Font = new Font("Arial", 12, FontStyle.Regular);
            playertxt2.Font = new Font("Arial", 15, FontStyle.Bold);
            playertxt2.Text = "TWO PLAYER";
            playertxt2.ForeColor = Color.Purple;
            playertxt2.BackColor = Color.Aqua;
            playertxt2.Left = 22;
            playertxt2.Top = 10 + 225;
            playertxt2.Width = 140;
            playertxt2.Height = 30;
            playertxt2.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(playertxt);
            this.Controls.Add(lsu1Player);
            playertxt.Visible = false;
            lsu1Player.Visible = false;
            this.Controls.Add(playertxt2);
            this.Controls.Add(lsu2Player);
            playertxt2.Visible = false;
            lsu2Player.Visible = false;
            file = new System.IO.StreamReader(Application.StartupPath + @"\oneplayer.txt");
            while ((line = file.ReadLine()) != null)
            {
                lsu1Player.Items.Add(line);
            }
            file.Close();
            file = new System.IO.StreamReader(Application.StartupPath + @"\twoplayer.txt");
            while ((line = file.ReadLine()) != null)
            {
                lsu2Player.Items.Add(line);
            }
            file.Close();
            info.Click += (s, e) => {
                SoundEffect("click");
                if (lsu1Player.Visible == true && lsu2Player.Visible == true) {
                    playertxt.Visible = false;
                    playertxt2.Visible = false; 
                    lsu1Player.Visible = false; lsu2Player.Visible = false; } infoPic.Visible = !infoPic.Visible; };
            this.Controls.Add(info);
            lichsu.Click += (s, e) => 
            {
                SoundEffect("click");
                if (infoPic.Visible == true)
                    infoPic.Visible = false;
                playertxt.Visible = !playertxt.Visible;
                playertxt2.Visible = !playertxt2.Visible;
                lsu1Player.Visible = !lsu1Player.Visible;
                lsu2Player.Visible = !lsu2Player.Visible;
            };
            this.Controls.Add(lichsu);
            leave.Left = 120;
            leave.Top = 590;
            leave.Height = 50;
            leave.Width = 432;
            leave.BackColor = Color.Transparent;
            leave.Image = Image.FromFile(Application.StartupPath + @"\image\button_thoat.png");
            leave.MouseHover += (s, e) =>
            {
                leave.Image = Image.FromFile(Application.StartupPath + @"\image\button_thoat (1).png");
            };
            leave.MouseLeave += (s, e) =>
            {
                leave.Image = Image.FromFile(Application.StartupPath + @"\image\button_thoat.png");
            };
            leave.Click += (s, e) => {
                SoundEffect("click");
                Close(); };
            this.Controls.Add(leave);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //if(this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            // Size the form to be 300 pixels in height and width.
            this.Size = new Size(680, 700);
            // Display the form in the center of the screen.
            this.CenterToScreen();
            MainMenu();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            foreach (Control a in this.Controls.OfType<PictureBox>())
            {
                if ((String)a.Tag == "1")
                {
                    this.Controls.Remove(a);
                }
            }
                CheckExplode(ref bomCountDown1, ref ba1, ref bc1, ref bomSize1);
                CheckExplode(ref bomCountDown2, ref ba2, ref bc2, ref bomSize2);
            sec++;
            if (allTime != -1)
                allTime++;
        }
        private void CheckDeath(PictureBox p)
        {
            if (twoPlayer)
            {
                if (player.Bounds.IntersectsWith(p.Bounds))
                {
                    this.Controls.Remove(player);
                    timer1.Stop();
                    timer2.Stop();
                    luuDiem(false, 2);
                    formDeath("Player 2 Win", 2);
                    endGame = true;
                }
                if (player2.Bounds.IntersectsWith(p.Bounds))
                {
                    this.Controls.Remove(player2);
                    timer1.Stop();
                    timer2.Stop();
                    luuDiem(false, 1);
                    formDeath("Player 1 Win", 2);
                    endGame = true;
                }
            }
            else
            {
                if (player.Bounds.IntersectsWith(p.Bounds))
                {
                    this.Controls.Remove(player);
                    timer1.Stop();
                    timer2.Stop();
                    luuDiem();
                    formDeath("You Lose", 1);
                    level = "man1";
                    endGame = true;
                }
                if (level == "man1" && !endGame)
                    if (monster[0].Bounds.IntersectsWith(p.Bounds))
                    {
                        this.Controls.Remove(monster[0]);
                        timer1.Stop();
                        timer2.Stop();
                        level = "man2";
                        formLevelUp("Passed Round 1");
                        endGame = true;
                    }
                if (level == "man2" && !endGame)
                    for (int i = 0; i <= 1; i++)
                        if (monster[i].Name != "death")
                            if (monster[i].Bounds.IntersectsWith(p.Bounds))
                            {
                                monster[i].Name = "death";
                                this.Controls.Remove(monster[i]);
                                countMonster--;
                                if (countMonster == 0)
                                {
                                    timer1.Stop();
                                    timer2.Stop();
                                    level = "man3";
                                    formLevelUp("Passed Round 2");
                                    endGame = true;
                                }
                            }
                if (level == "man3" && !endGame)
                    for (int i = 0; i <= 2; i++)
                        if (monster[i].Name != "death")
                            if (monster[i].Bounds.IntersectsWith(p.Bounds))
                            {
                                monster[i].Name = "death";
                                this.Controls.Remove(monster[i]);
                                countMonster--;
                                if (countMonster == 0)
                                {
                                    timer1.Stop();
                                    timer2.Stop();
                                    luuDiem(true);
                                    level = "man1";
                                    formLevelUp("Passed Round 3");
                                    endGame = true;
                                }
                            }
            }
        }

        private void formLevelUp(string text)
        {
            if (menu)
                return;
            playSound.controls.stop();
            SoundEffect("win");
            death.Text = "";
            death.Controls.Add(txtMsg);
            txtMsg.AutoSize = true;
            txtMsg.Text = text;
            txtMsg.ForeColor = Color.Yellow;
            txtMsg.Font = new Font("Arial", 20, FontStyle.Bold);
            death.Width = 300;
            death.Height = 150;
            death.FormBorderStyle = FormBorderStyle.None;
            death.MinimizeBox = false;
            death.MaximizeBox = false;
            death.BackColor = Color.DodgerBlue;
            death.ShowInTaskbar = false;
            txtMsg.Location = new Point(death.Width / 2 - txtMsg.Width / 2, death.Height / 2 - txtMsg.Height - 20);
            death.Controls.Add(btRestart);
            death.Controls.Add(btReturn);
            if(level == "man1")
                btRestart.Text = "Chơi lại";
            else
                btRestart.Text = "Qua màn";
            btReturn.Text = "Menu";
            btRestart.ForeColor = Color.Yellow;
            btRestart.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            btReturn.ForeColor = Color.Yellow;
            btReturn.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            btRestart.Height = 40;
            btRestart.Width = 100;
            btReturn.Height = 40;
            btReturn.Width = 100;
            btRestart.Location = new Point(death.Width / 2 - btRestart.Width / 2 - 70, death.Height / 2 + txtMsg.Height - 30);
            btReturn.Location = new Point(death.Width / 2 - btRestart.Width / 2 + 50, btRestart.Location.Y);
            int chon = 0;
            btRestart.Click += (s, e) => {
                SoundEffect("click");
                chon = 1; death.Close(); };
            btReturn.Click += (s, e) => {
                SoundEffect("click");
                death.Close(); };
            death.FormClosing += (s, e) => { Run(); if (chon == 0) MainMenu(); else OnePlayer(); };
            death.StartPosition = FormStartPosition.CenterParent;
            death.ShowDialog();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát Game?", "Thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                playSound.controls.stop();
                (new SoundPlayer(Application.StartupPath + @"\sounds\bye_bye.wav")).Play();
                System.Threading.Thread.Sleep(4000);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void RandomItem(PictureBox item)
        {
            int so = r.Next(0, 6);
            if (so > 2)
            {
                switch (so)
                {
                    case 3:
                        item.Image = Image.FromFile(Application.StartupPath + @"\image\item_bomb.png");
                        item.Tag = "amount";
                        break;
                    case 4:
                        item.Image = Image.FromFile(Application.StartupPath + @"\image\item_bombsize.png");
                        item.Tag = "size";
                        break;
                    case 5:
                        item.Image = Image.FromFile(Application.StartupPath + @"\image\item_shoe.png");
                        item.Tag = "speed";
                        break;
                }
                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                item.BackColor = Color.Transparent;
                item.SizeMode = PictureBoxSizeMode.StretchImage;
                item.Width = item.Height = 55;
            }
            else
            {
                this.Controls.Remove(item);
            }
        }
        private void CheckExplode(ref PictureBox[] bomCountDown, ref int ba, ref double[] bc, ref int size)
        {
            for (int z = 0; z <= 4; z++)
            {
                if ((bc[z] != 0 && bc[z] == sec))
                {
                    int x = bomCountDown[z].Left / 60;
                    int y = bomCountDown[z].Top / 60;
                    table[x, y] = 0;
                    bool up = false;
                    bool down = false;
                    bool left = false;
                    bool right = false;

                    int yu = (y - size) >= 0 ? (y - size) : 0;
                    int yd = (y + size) <= 10 ? (y + size) : 10;
                    int xl = (x - size) >= 0 ? (x - size) : 0;
                    int xr = (x + size) <= 10 ? (x + size) : 10;
                    SoundEffect("boom_bang");
                    //up
                    for (int i = y - 1; i >= yu; i--)
                    {
                        if (up)
                            break;
                        if (table[x, i] >= 2)
                            break;

                        PictureBox p = new PictureBox();
                        if (i != yu)
                            p.Image = Image.FromFile(Application.StartupPath + @"\image\bombbang_up_1.png");
                        else
                            p.Image = Image.FromFile(Application.StartupPath + @"\image\bombbang_up_2.png");
                        p.Width = 55;
                        p.Height = 60;
                        p.Left = x * 60;
                        p.Top = i * 60;
                        p.Tag = "1";
                        this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                        p.BackColor = Color.Transparent;
                        p.SizeMode = PictureBoxSizeMode.StretchImage;
                        this.Controls.Add(p);
                        p.BringToFront();
                        CheckDeath(p);
                        if (endGame)
                        {
                            up = true;
                            break;
                        }
                        foreach (PictureBox s in this.Controls.OfType<PictureBox>())
                        {
                            if ((String)s.Tag == "boom")
                            {
                                if (p.Bounds.IntersectsWith(s.Bounds))
                                {
                                    up = true;
                                    break;
                                }
                            }
                            if ((String)s.Tag == "box")
                            {
                                if (p.Bounds.IntersectsWith(s.Bounds))
                                {
                                    table[s.Top / 60, s.Left / 60] = 0;
                                    RandomItem(s);
                                    up = true;
                                    break;
                                }
                            }
                            if ((String)s.Tag == "amount" || (String)s.Tag == "speed" || (String)s.Tag == "size")
                            {
                                if (p.Bounds.IntersectsWith(s.Bounds))
                                {
                                    this.Controls.Remove(s);
                                    break;
                                }
                            }
                        }
                    }
                    if (endGame)
                    {
                        endGame = false;
                        return;
                    }
                    //down
                    for (int i = y + 1; i <= yd; i++)
                    {
                        if (down)
                            break;
                        if (table[x, i] >= 2)
                            break;
                        PictureBox p = new PictureBox();
                        if (i != yd)
                            p.Image = Image.FromFile(Application.StartupPath + @"\image\bombbang_down_1.png");
                        else
                            p.Image = Image.FromFile(Application.StartupPath + @"\image\bombbang_down_2.png");
                        p.Width = 55;
                        p.Height = 60;
                        p.Left = x * 60;
                        p.Top = i * 60;
                        p.Tag = "1";
                        this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                        p.BackColor = Color.Transparent;
                        p.SizeMode = PictureBoxSizeMode.StretchImage;
                        this.Controls.Add(p);
                        p.BringToFront();
                        CheckDeath(p);
                        if (endGame)
                        {
                            down = true;
                            break;
                        }
                        foreach (PictureBox s in this.Controls.OfType<PictureBox>())
                        {
                            if ((String)s.Tag == "boom")
                            {
                                if (p.Bounds.IntersectsWith(s.Bounds))
                                {
                                    down = true;
                                    break;
                                }
                            }
                            if ((String)s.Tag == "box")
                            {
                                if (p.Bounds.IntersectsWith(s.Bounds))
                                {
                                    table[s.Top / 60, s.Left / 60] = 0;
                                    RandomItem(s);
                                    down = true;
                                    break;
                                }
                            }
                            if ((String)s.Tag == "amount" || (String)s.Tag == "speed" || (String)s.Tag == "size")
                            {
                                if (p.Bounds.IntersectsWith(s.Bounds))
                                {
                                    this.Controls.Remove(s);
                                    break;
                                }
                            }
                        }
                    }
                    if (endGame)
                    {
                        endGame = false;
                        return;
                    }
                    //left
                    for (int i = x - 1; i >= xl; i--)
                    {
                        if (left)
                            break;
                        if (table[i, y] >= 2)
                            break;
                        PictureBox p = new PictureBox();
                        if (i != xl)
                            p.Image = Image.FromFile(Application.StartupPath + @"\image\bombbang_left_1.png");
                        else
                            p.Image = Image.FromFile(Application.StartupPath + @"\image\bombbang_left_2.png");
                        p.Width = 60;
                        p.Height = 55;
                        p.Left = i * 60;
                        p.Top = y * 60;
                        p.Tag = "1";
                        this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                        p.BackColor = Color.Transparent;
                        p.SizeMode = PictureBoxSizeMode.StretchImage;
                        this.Controls.Add(p);
                        p.BringToFront();
                        CheckDeath(p);
                        if (endGame)
                        {
                            left = true;
                            break;
                        }
                        foreach (PictureBox s in this.Controls.OfType<PictureBox>())
                        {
                            if ((String)s.Tag == "boom")
                            {
                                if (p.Bounds.IntersectsWith(s.Bounds))
                                {
                                    left = true;
                                    break;
                                }
                            }
                            if ((String)s.Tag == "box")
                            {
                                if (p.Bounds.IntersectsWith(s.Bounds))
                                {
                                    table[s.Top / 60, s.Left / 60] = 0;
                                    RandomItem(s);
                                    left = true;
                                    break;
                                }
                            }
                            if ((String)s.Tag == "amount" || (String)s.Tag == "speed" || (String)s.Tag == "size")
                            {
                                if (p.Bounds.IntersectsWith(s.Bounds))
                                {
                                    this.Controls.Remove(s);
                                    break;
                                }
                            }
                        }

                    }
                    if (endGame)
                    {
                        endGame = false;
                        return;
                    }
                    //right
                    for (int i = x + 1; i <= xr; i++)
                    {
                        if (right)
                            break;
                        if (table[i, y] >= 2)
                            break;
                        PictureBox p = new PictureBox();
                        if (i != xr)
                            p.Image = Image.FromFile(Application.StartupPath + @"\image\bombbang_right_1.png");
                        else
                            p.Image = Image.FromFile(Application.StartupPath + @"\image\bombbang_right_2.png");
                        p.Width = 60;
                        p.Height = 55;
                        p.Left = i * 60;
                        p.Top = y * 60;
                        p.Tag = "1";
                        this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                        p.BackColor = Color.Transparent;
                        p.SizeMode = PictureBoxSizeMode.StretchImage;
                        this.Controls.Add(p);
                        p.BringToFront();
                        CheckDeath(p);
                        if (endGame)
                        {
                            right = true;
                            break;
                        }
                        foreach (PictureBox s in this.Controls.OfType<PictureBox>())
                        {
                            if ((String)s.Tag == "boom")
                            {
                                if (p.Bounds.IntersectsWith(s.Bounds))
                                {
                                    right = true;
                                    break;
                                }
                            }
                            if ((String)s.Tag == "box")
                            {
                                if (p.Bounds.IntersectsWith(s.Bounds))
                                {
                                    table[s.Top / 60, s.Left / 60] = 0;
                                    RandomItem(s);
                                    right = true;
                                    break;
                                }
                            }
                            if ((String)s.Tag == "amount" || (String)s.Tag == "speed" || (String)s.Tag == "size")
                            {
                                if (p.Bounds.IntersectsWith(s.Bounds))
                                {
                                    this.Controls.Remove(s);
                                    break;
                                }
                            }
                        }
                    }
                    if (endGame)
                    {
                        endGame = false;
                        return;
                    }
                    this.Controls.Remove(bomCountDown[z]);
                    //mid
                    PictureBox m = new PictureBox();
                    m.Image = Image.FromFile(Application.StartupPath + @"\image\bombbang_mid_2.png");
                    m.Width = 60;
                    m.Height = 55;
                    m.Left = x * 60;
                    m.Top = y * 60;
                    m.Tag = "1";
                    this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                    m.BackColor = Color.Transparent;
                    m.SizeMode = PictureBoxSizeMode.StretchImage;
                    this.Controls.Add(m);
                    table[y, x] = 0;
                    CheckDeath(m);
                    bc[z] = 0;
                    ba--;
                    if (endGame)
                    {
                        endGame = false;
                        return;
                    }
                }
            }
        }
        private void DatBom(PictureBox players, ref PictureBox[] bomCountDown, ref int ba, ref double[] bc, ref int amount)
        {
            if (menu)
                return;
            if (ba >= amount)
                return;
            bool full = true;
            foreach (int x in bc)
                if (x == 0)
                {
                    full = false;
                    break;
                }
            if (full)
                return;
            double a = (double)(players.Top / 60);
            double b = (double)(players.Left / 60);
            double a2 = (double)(players.Top % 60);
            double b2 = (double)(players.Left % 60);
            //label2.Text = table[(int)(players.Top - a2) / 60, (int)(players.Left - b2) / 60].ToString() + " " + ((int)(players.Top - a2)).ToString() + " " + ((int)(players.Left - b2)).ToString();
            if (table[(int)(players.Top - a2) / 60, (int)(players.Left - b2) / 60] > 0 || table[(int)(players.Top - a2) / 60, (int)(players.Left - b2) / 60] == -1)
                return;
            //table[(int)(players.Top - a2) / 60, (int)(players.Left - b2) / 60] = -1;
            PictureBox bom = new PictureBox();
            bom.Width = 55;
            bom.Height = 55;
            bom.Tag = "boom";
            if (bom.Top != (int)(players.Top - a2) || bom.Left != (int)(players.Left - b2))
            {
                if (players.Name == "player1")
                {
                    if (goUp)
                        bom.Top = (int)(players.Top - a2) + 60;
                    else
                        if(Math.Ceiling(a2) == 55)
                        bom.Top = (int)(players.Top - Math.Ceiling(a2)) +60;
                        else
                        bom.Top = (int)(players.Top - a2);
                    if (goLeft)
                        bom.Left = (int)(players.Left - b2) + 60;
                    else
                        if(Math.Ceiling(b2) == 55)
                            bom.Left = (int)(players.Left - Math.Ceiling(b2)) + 60;
                        else
                            bom.Left = (int)(players.Left - b2);
                }
                if (players.Name == "player2")
                {
                    if (goUp2)
                        bom.Top = (int)(players.Top - a2) + 60;
                    else
                        bom.Top = (int)(players.Top - a2);
                    if (goLeft2)
                        bom.Left = (int)(players.Left - b2) + 60;
                    else
                        bom.Left = (int)(players.Left - b2);
                }
            }
            bom.Image = Image.FromFile(Application.StartupPath + @"\image\boom8.png");
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            bom.BackColor = Color.Transparent;
            bom.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(bom);
            SoundEffect("set_boom");
            if (table[bom.Top / 60, bom.Left / 60] >= 1)
            {
                    bom.Top = (int)(a - a2);
                    bom.Left = (int)(b - b2);
                table[(int)(players.Left - b2) / 60, (int)(players.Top - a2) / 60] = -1;
                
                
            }else
            if (table[bom.Top / 60, bom.Left / 60] == 0)
            {
                table[bom.Top / 60, bom.Left / 60] = -1;
            }else
            if (table[bom.Top / 60, bom.Left / 60] == -1)
            {
                this.Controls.Remove(bom);
                return;
            }
            for (int i = 0; i <= ba; i++)
            {
                if (bc[i] == 0)
                {
                    bomCountDown[i] = bom;
                    bc[i] = sec + 3;
                    ba++;
                    break;
                }
            }
        }
        private void formDeath(string text, int type)
        {
            if (menu)
                return;
            if (!twoPlayer)
                level = "man1";
            if (type == 1)
            {
                playSound.controls.stop();
                SoundEffect("die");
            }
            if (type == 2)
            {
                playSound.controls.stop();
                SoundEffect("win");
            }
            death.Text = "";
            death.Controls.Add(txtMsg);
            txtMsg.AutoSize = true;
            txtMsg.Text = text;
            txtMsg.ForeColor = Color.Yellow;
            txtMsg.Font = new Font("Arial", 20, FontStyle.Bold);
            death.Width = 300;
            death.Height = 150;
            death.FormBorderStyle = FormBorderStyle.None;
            death.MinimizeBox = false;
            death.MaximizeBox = false;
            death.BackColor = Color.DodgerBlue;
            death.ShowInTaskbar = false;
            txtMsg.Location = new Point(death.Width / 2 - txtMsg.Width / 2, death.Height / 2 - txtMsg.Height - 20);
            death.Controls.Add(btRestart);
            death.Controls.Add(btReturn);
            btRestart.Text = "Chơi lại";
            btReturn.Text = "Menu";
            btRestart.ForeColor = Color.Yellow;
            btRestart.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            btReturn.ForeColor = Color.Yellow;
            btReturn.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            btRestart.Height = 40;
            btRestart.Width = 100;
            btReturn.Height = 40;
            btReturn.Width = 100;
            btRestart.Location = new Point(death.Width / 2 - btRestart.Width / 2 - 70, death.Height / 2 + txtMsg.Height - 30);
            btReturn.Location = new Point(death.Width / 2 - btRestart.Width / 2 + 50, btRestart.Location.Y);
            int chon = 0;
            btRestart.Click += (s, e) => {
                SoundEffect("click");
                chon = 1; death.Close(); };
            btReturn.Click += (s, e) => {
                SoundEffect("click");
                death.Close(); };
            death.FormClosing += (s, e) => { Run(); if (chon == 0) MainMenu(); else { if (type == 1) OnePlayer(); if(type == 2) TwoPlayer(); } };
            death.StartPosition = FormStartPosition.CenterParent;
            death.ShowDialog();
        }
        private void luuDiem(bool Passed = false, int playerWin = 0) 
        {
            CultureInfo viVn = new CultureInfo("vi-VN");
            int minute, second; string time = "";

            if (!twoPlayer)
            {
                file1 =
                   new System.IO.StreamWriter(Application.StartupPath + @"\oneplayer.txt", true);
                int round = 0;
                if (!Passed)
                {
                    if (level == "man1")
                        round = 0;
                    if (level == "man2")
                        round = 1;
                    if (level == "man3")
                        round = 2;
                }
                else round = 3;
                minute = (allTime - allTime % 60)/60;
                second = allTime % 60;
                if (minute != 0)
                    time = minute + ":" + second+"s";
                else
                    time = second + "s";
                file1.WriteLine(date.ToString(viVn) + "   Passed " + round + " round   Time played: " + time);
                file1.Close();
            }
            else 
            {
                file1 =
                   new System.IO.StreamWriter(Application.StartupPath + @"\twoplayer.txt", true);
                minute = (int)((sec - sec % 60) / 60);
                second = (int)sec % 60;
                if (minute != 0)
                    time = minute + ":" + second + "s";
                else
                    time = second + "s";
                file1.WriteLine(date.ToString(viVn) + "   Player " + playerWin + " Win   Time played: " + time);
                file1.Close();
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (twoPlayer)
            {
                if (e.KeyCode == Keys.Up)
                    goUp2 = false;
                if (e.KeyCode == Keys.Down)
                    goDown2 = false;
                if (e.KeyCode == Keys.Left)
                    goLeft2 = false;
                if (e.KeyCode == Keys.Right)
                    goRight2 = false;
                if (e.KeyCode == Keys.T)
                    goUp = false;
                if (e.KeyCode == Keys.G)
                    goDown = false;
                if (e.KeyCode == Keys.F)
                    goLeft = false;
                if (e.KeyCode == Keys.H)
                    goRight = false;
            }
            else 
            {
                if (e.KeyCode == Keys.Up)
                    goUp = false;
                if (e.KeyCode == Keys.Down)
                    goDown = false;
                if (e.KeyCode == Keys.Left)
                    goLeft = false;
                if (e.KeyCode == Keys.Right)
                    goRight = false;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (twoPlayer)
            {
                if (e.KeyCode == Keys.Up)
                {
                    if (!goUp2)
                    {
                        goUp2 = true;
                        goDown2 = false;
                        goLeft2 = false;
                        goRight2 = false;
                        player2.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_up.png");
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    if (!goDown2)
                    {
                        goUp2 = false;
                        goDown2 = true;
                        goLeft2 = false;
                        goRight2 = false;
                        player2.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_down.png");
                    }
                }
                if (e.KeyCode == Keys.Left)
                {
                    if (!goLeft2)
                    {
                        goUp2 = false;
                        goDown2 = false;
                        goLeft2 = true;
                        goRight2 = false;
                        player2.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_left.png");
                    }
                }
                if (e.KeyCode == Keys.Right)
                {
                    if (!goRight2)
                    {
                        goUp2 = false;
                        goDown2 = false;
                        goLeft2 = false;
                        goRight2 = true;
                        player2.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_right.png");
                    }
                }
                if (e.KeyCode == Keys.OemPeriod)
                    DatBom(player2, ref bomCountDown2, ref ba2, ref bc2, ref bomAmount2);
                if (e.KeyCode == Keys.T)
                {
                    if (!goUp)
                    {
                        goUp = true;
                        goDown = false;
                        goLeft = false;
                        goRight = false;
                        player.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_up.png");
                    }
                }
                if (e.KeyCode == Keys.G)
                {
                    if (!goDown)
                    {
                        goUp = false;
                        goDown = true;
                        goLeft = false;
                        goRight = false;
                        player.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_down.png");
                    }
                }
                if (e.KeyCode == Keys.F)
                {
                    if (!goLeft)
                    {
                        goUp = false;
                        goDown = false;
                        goLeft = true;
                        goRight = false;
                        player.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_left.png");
                    }
                }
                if (e.KeyCode == Keys.H)
                {
                    if (!goRight)
                    {
                        goUp = false;
                        goDown = false;
                        goLeft = false;
                        goRight = true;
                        player.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_right.png");
                    }
                }
                if (e.KeyCode == Keys.Z)
                    DatBom(player, ref bomCountDown1, ref ba1, ref bc1, ref bomAmount1);
            }
            else 
            {
                if (e.KeyCode == Keys.Up)
                {
                    if (!goUp)
                    {
                        goUp = true;
                        goDown = false;
                        goLeft = false;
                        goRight = false;
                        player.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_up.png");
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    if (!goDown)
                    {
                        goUp = false;
                        goDown = true;
                        goLeft = false;
                        goRight = false;
                        player.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_down.png");
                    }
                }
                if (e.KeyCode == Keys.Left)
                {
                    if (!goLeft)
                    {
                        goUp = false;
                        goDown = false;
                        goLeft = true;
                        goRight = false;
                        player.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_left.png");
                    }
                }
                if (e.KeyCode == Keys.Right)
                {
                    if (!goRight)
                    {
                        goUp = false;
                        goDown = false;
                        goLeft = false;
                        goRight = true;
                        player.Image = Image.FromFile(Application.StartupPath + @"\image\bomber_right.png");
                    }
                }
                if (e.KeyCode == Keys.Space)
                    DatBom(player, ref bomCountDown1, ref ba1, ref bc1, ref bomAmount1);
            }
        }
    }
}
