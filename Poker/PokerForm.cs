﻿namespace Poker
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.IO;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	public partial class PokerForm : Form
    {
        #region Variables

        // ProgressBar progressBar = new ProgressBar();
        // public int Nm;
        private const string AssetsPath = @"..\..\Assets\";

        private Panel playerPanel = new Panel();
        private Panel firstBotPanel = new Panel();
        private Panel secondBotPanel = new Panel();
        private Panel thirdBotPanel = new Panel();
        private Panel fourthBotPanel = new Panel();
        private Panel fifthBotPlayer = new Panel();
        private int call = 500;

        private int foldedPlayers = 5;

        private int chips = 10000;

        private int bot1Chips = 10000;

        private int bot2Chips = 10000;

        private int bot3Chips = 10000;

        private int bot4Chips = 10000;

        private int bot5Chips = 10000;

		double type;

		double rounds;

		double b1Power;

		double b2Power;

		double b3Power;

		double b4Power;

		double b5Power;

		double pPower;

		double pType = -1;

		double Raise;

		double b1Type = -1;

		double b2Type = -1;

		double b3Type = -1;

		double b4Type = -1;

		double b5Type = -1;

		bool B1turn;

		bool B2turn;

		bool B3turn;

		bool B4turn;

		bool B5turn;

		bool B1Fturn;

		bool B2Fturn;

		bool B3Fturn;

		bool B4Fturn;

		bool B5Fturn;

		bool pFolded;

		bool b1Folded;

		bool b2Folded;

		bool b3Folded;

		bool b4Folded;

		bool b5Folded;

		bool intsadded;

		bool changed;

		int pCall;

		int b1Call;

		int b2Call;

		int b3Call;

		int b4Call;

		int b5Call;

		int pRaise;

		int b1Raise;

		int b2Raise;

		int b3Raise;

		int b4Raise;

		int b5Raise;

		int height;

		int width;

		int winners;

		int Flop = 1;

		int Turn = 2;

		int River = 3;

		int End = 4;

		int maxLeft = 6;

		int last = 123;

		int raisedTurn = 1;

		List<bool?> bools = new List<bool?>();
        List<Type> Win = new List<Type>();
        List<string> CheckWinners = new List<string>();
        List<int> ints = new List<int>();
        bool PFturn;

		bool Pturn = true;

		bool restart;

		bool raising;

		Type sorted;
        string[] ImgLocation = Directory.GetFiles(AssetsPath + @"Cards", "*.png", SearchOption.TopDirectoryOnly);
        int[] Reserve = new int[17];
        Image[] Deck = new Image[52];
        PictureBox[] Holder = new PictureBox[52];
        Timer timer = new Timer();
        Timer Updates = new Timer();
        int t = 60;

		int i;

		int bb = 500;

		int sb = 250;

		int up = 10000000;

		int turnCount;

		#endregion
        public PokerForm()
        {
            // bools.Add(PFturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);
            call = bb;
            MaximizeBox = false;
            MinimizeBox = false;
            Updates.Start();
            InitializeComponent();
            width = this.Width;
            height = this.Height;
            Shuffle();
            tbPot.Enabled = false;
            tbChips.Enabled = false;
            tbBotChips1.Enabled = false;
            tbBotChips2.Enabled = false;
            tbBotChips3.Enabled = false;
            tbBotChips4.Enabled = false;
            tbBotChips5.Enabled = false;
            tbChips.Text = "chips : " + this.chips;
            tbBotChips1.Text = "chips : " + this.bot1Chips;
            tbBotChips2.Text = "chips : " + this.bot2Chips;
            tbBotChips3.Text = "chips : " + this.bot3Chips;
            tbBotChips4.Text = "chips : " + this.bot4Chips;
            tbBotChips5.Text = "chips : " + this.bot5Chips;
            timer.Interval = 1 * 1 * 1000;
            timer.Tick += timer_Tick;
            Updates.Interval = 1 * 1 * 100;
            Updates.Tick += Update_Tick;
            tbBB.Visible = true;
            tbSB.Visible = true;
            bBB.Visible = true;
            bSB.Visible = true;
            tbBB.Visible = true;
            tbSB.Visible = true;
            bBB.Visible = true;
            bSB.Visible = true;
            tbBB.Visible = false;
            tbSB.Visible = false;
            bBB.Visible = false;
            bSB.Visible = false;
            tbRaise.Text = (bb * 2).ToString();
        }

        async Task Shuffle()
        {
            bools.Add(PFturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);
            bCall.Enabled = false;
            bRaise.Enabled = false;
            bFold.Enabled = false;
            bCheck.Enabled = false;
            MaximizeBox = false;
            MinimizeBox = false;
            bool check = false;
            Bitmap backImage = new Bitmap(AssetsPath + @"Back\Back.png");
            int horizontal = 580, vertical = 480;
            Random r = new Random();
            for (int i = ImgLocation.Length; i > 0; i--)
            {
                int j = r.Next(i);
                var k = ImgLocation[j];
                ImgLocation[j] = ImgLocation[i - 1];
                ImgLocation[i - 1] = k;
            }

            for (int i = 0; i < 17; i++)
            {

                Deck[i] = Image.FromFile(ImgLocation[i]);
                var charsToRemove = new[] { AssetsPath + @"Cards\", ".png" };
                foreach (var c in charsToRemove)
                {
                    ImgLocation[i] = ImgLocation[i].Replace(c, string.Empty);
                }

                Reserve[i] = int.Parse(ImgLocation[i]) - 1;
                Holder[i] = new PictureBox();
                Holder[i].SizeMode = PictureBoxSizeMode.StretchImage;
                Holder[i].Height = 130;
                Holder[i].Width = 80;
                this.Controls.Add(Holder[i]);
                Holder[i].Name = "pb" + this.i;
                await Task.Delay(200);
                
                if (i < 2)
                {
	                if (Holder[0].Tag != null)
	                {
		                Holder[1].Tag = Reserve[1];
	                }

	                Holder[0].Tag = Reserve[0];
	                Holder[i].Image = Deck[i];
	                Holder[i].Anchor = AnchorStyles.Bottom;

	                // Holder[i].Dock = DockStyle.Top;
	                Holder[i].Location = new Point(horizontal, vertical);
	                horizontal += Holder[i].Width;
	                this.Controls.Add(playerPanel);
	                playerPanel.Location = new Point(Holder[0].Left - 10, Holder[0].Top - 10);
	                playerPanel.BackColor = Color.DarkBlue;
	                playerPanel.Height = 150;
	                playerPanel.Width = 180;
	                playerPanel.Visible = false;
                }

	            if (bot1Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 2 && i < 4)
                    {
	                    if (Holder[2].Tag != null)
	                    {
		                    Holder[3].Tag = Reserve[3];
	                    }

	                    Holder[2].Tag = Reserve[2];
	                    if (!check)
	                    {
		                    horizontal = 15;
		                    vertical = 420;
	                    }

	                    check = true;
	                    Holder[i].Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
	                    Holder[i].Image = backImage;

	                    // Holder[i].Image = Deck[i];
	                    Holder[i].Location = new Point(horizontal, vertical);
	                    horizontal += Holder[i].Width;
	                    Holder[i].Visible = true;
	                    this.Controls.Add(firstBotPanel);
	                    firstBotPanel.Location = new Point(Holder[2].Left - 10, Holder[2].Top - 10);
	                    firstBotPanel.BackColor = Color.DarkBlue;
	                    firstBotPanel.Height = 150;
	                    firstBotPanel.Width = 180;
	                    firstBotPanel.Visible = false;
	                    if (i == 3)
	                    {
		                    check = false;
	                    }
                    }
                }

                if (bot2Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 4 && i < 6)
                    {
	                    if (Holder[4].Tag != null)
	                    {
		                    Holder[5].Tag = Reserve[5];
	                    }

	                    Holder[4].Tag = Reserve[4];
	                    if (!check)
	                    {
		                    horizontal = 75;
		                    vertical = 65;
	                    }

	                    check = true;
	                    Holder[i].Anchor = AnchorStyles.Top | AnchorStyles.Left;
	                    Holder[i].Image = backImage;

	                    // Holder[i].Image = Deck[i];
	                    Holder[i].Location = new Point(horizontal, vertical);
	                    horizontal += Holder[i].Width;
	                    Holder[i].Visible = true;
	                    this.Controls.Add(secondBotPanel);
	                    secondBotPanel.Location = new Point(Holder[4].Left - 10, Holder[4].Top - 10);
	                    secondBotPanel.BackColor = Color.DarkBlue;
	                    secondBotPanel.Height = 150;
	                    secondBotPanel.Width = 180;
	                    secondBotPanel.Visible = false;
	                    if (i == 5)
	                    {
		                    check = false;
	                    }
                    }
                }

                if (bot3Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 6 && i < 8)
                    {
	                    if (Holder[6].Tag != null)
	                    {
		                    Holder[7].Tag = Reserve[7];
	                    }

	                    Holder[6].Tag = Reserve[6];
	                    if (!check)
	                    {
		                    horizontal = 590;
		                    vertical = 25;
	                    }

	                    check = true;
	                    Holder[i].Anchor = AnchorStyles.Top;
	                    Holder[i].Image = backImage;

	                    // Holder[i].Image = Deck[i];
	                    Holder[i].Location = new Point(horizontal, vertical);
	                    horizontal += Holder[i].Width;
	                    Holder[i].Visible = true;
	                    this.Controls.Add(thirdBotPanel);
	                    thirdBotPanel.Location = new Point(Holder[6].Left - 10, Holder[6].Top - 10);
	                    thirdBotPanel.BackColor = Color.DarkBlue;
	                    thirdBotPanel.Height = 150;
	                    thirdBotPanel.Width = 180;
	                    thirdBotPanel.Visible = false;
	                    if (i == 7)
	                    {
		                    check = false;
	                    }
                    }
                }

                if (bot4Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 8 && i < 10)
                    {
	                    if (Holder[8].Tag != null)
	                    {
		                    Holder[9].Tag = Reserve[9];
	                    }

	                    Holder[8].Tag = Reserve[8];
	                    if (!check)
	                    {
		                    horizontal = 1115;
		                    vertical = 65;
	                    }

	                    check = true;
	                    Holder[i].Anchor = AnchorStyles.Top | AnchorStyles.Right;
	                    Holder[i].Image = backImage;

	                    // Holder[i].Image = Deck[i];
	                    Holder[i].Location = new Point(horizontal, vertical);
	                    horizontal += Holder[i].Width;
	                    Holder[i].Visible = true;
	                    this.Controls.Add(fourthBotPanel);
	                    fourthBotPanel.Location = new Point(Holder[8].Left - 10, Holder[8].Top - 10);
	                    fourthBotPanel.BackColor = Color.DarkBlue;
	                    fourthBotPanel.Height = 150;
	                    fourthBotPanel.Width = 180;
	                    fourthBotPanel.Visible = false;
	                    if (i == 9)
	                    {
		                    check = false;
	                    }
                    }
                }

                if (bot5Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 10 && i < 12)
                    {
	                    if (Holder[10].Tag != null)
	                    {
		                    Holder[11].Tag = Reserve[11];
	                    }

	                    Holder[10].Tag = Reserve[10];
	                    if (!check)
	                    {
		                    horizontal = 1160;
		                    vertical = 420;
	                    }

	                    check = true;
	                    Holder[i].Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
	                    Holder[i].Image = backImage;

	                    // Holder[i].Image = Deck[i];
	                    Holder[i].Location = new Point(horizontal, vertical);
	                    horizontal += Holder[i].Width;
	                    Holder[i].Visible = true;
	                    this.Controls.Add(fifthBotPlayer);
	                    fifthBotPlayer.Location = new Point(Holder[10].Left - 10, Holder[10].Top - 10);
	                    fifthBotPlayer.BackColor = Color.DarkBlue;
	                    fifthBotPlayer.Height = 150;
	                    fifthBotPlayer.Width = 180;
	                    fifthBotPlayer.Visible = false;
	                    if (i == 11)
	                    {
		                    check = false;
	                    }
                    }
                }

                if (i >= 12)
                {
                    Holder[12].Tag = Reserve[12];
                    if (i > 15)
                    {
                        Holder[16].Tag = Reserve[16];

                    }

                    else if (i > 14)
                    {
                        Holder[15].Tag = Reserve[15];
                    }

                    else if (i > 13)
                    {
                        Holder[14].Tag = Reserve[14];
                    }

                    else if (i > 12)
                    {
                        Holder[13].Tag = Reserve[13];
                    }

                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }

                    check = true;
                    if (Holder[i] != null)
                    {
	                    Holder[i].Anchor = AnchorStyles.None;
	                    Holder[i].Image = backImage;

	                    // Holder[i].Image = Deck[i];
	                    Holder[i].Location = new Point(horizontal, vertical);
	                    horizontal += 110;
                    }
                }
                
                if (bot1Chips <= 0)
                {
                    B1Fturn = true;
                    Holder[2].Visible = false;
                    Holder[3].Visible = false;
                }
                else
                {
                    B1Fturn = false;
                    if (i == 3)
                    {
                        if (Holder[3] != null)
                        {
                            Holder[2].Visible = true;
                            Holder[3].Visible = true;
                        }
                    }
                }

                if (bot2Chips <= 0)
                {
                    B2Fturn = true;
                    Holder[4].Visible = false;
                    Holder[5].Visible = false;
                }
                else
                {
                    B2Fturn = false;
                    if (i == 5)
                    {
                        if (Holder[5] != null)
                        {
                            Holder[4].Visible = true;
                            Holder[5].Visible = true;
                        }
                    }
                }

                if (bot3Chips <= 0)
                {
                    B3Fturn = true;
                    Holder[6].Visible = false;
                    Holder[7].Visible = false;
                }
                else
                {
                    B3Fturn = false;
                    if (i == 7)
                    {
                        if (Holder[7] != null)
                        {
                            Holder[6].Visible = true;
                            Holder[7].Visible = true;
                        }
                    }
                }

                if (bot4Chips <= 0)
                {
                    B4Fturn = true;
                    Holder[8].Visible = false;
                    Holder[9].Visible = false;
                }
                else
                {
                    B4Fturn = false;
                    if (i == 9)
                    {
                        if (Holder[9] != null)
                        {
                            Holder[8].Visible = true;
                            Holder[9].Visible = true;
                        }
                    }
                }

                if (bot5Chips <= 0)
                {
                    B5Fturn = true;
                    Holder[10].Visible = false;
                    Holder[11].Visible = false;
                }
                else
                {
                    B5Fturn = false;
                    if (i == 11)
                    {
                        if (Holder[11] != null)
                        {
                            Holder[10].Visible = true;
                            Holder[11].Visible = true;
                        }
                    }
                }

                if (i == 16)
                {
                    if (!restart)
                    {
                        MaximizeBox = true;
                        MinimizeBox = true;
                    }

                    timer.Start();
                }
            }

            if (foldedPlayers == 5)
            {
                DialogResult dialogResult = MessageBox.Show("Would You Like To Play Again ?", "You Won , Congratulations ! ", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                foldedPlayers = 5;
            }

            if (i == 17)
            {
                bRaise.Enabled = true;
                bCall.Enabled = true;
                bRaise.Enabled = true;
                bRaise.Enabled = true;
                bFold.Enabled = true;
            }
        }

        async Task Turns()
        {
            
            if (!PFturn)
            {
                if (Pturn)
                {
	                FixCall(pStatus, ref pCall, ref pRaise, 1);

	                // MessageBox.Show("Player's Turn");
	                pbTimer.Visible = true;
	                pbTimer.Value = 1000;
	                t = 60;
	                up = 10000000;
	                timer.Start();
	                bRaise.Enabled = true;
	                bCall.Enabled = true;
	                bRaise.Enabled = true;
	                bRaise.Enabled = true;
	                bFold.Enabled = true;
	                turnCount++;
	                FixCall(pStatus, ref pCall, ref pRaise, 2);
                }
            }

            if (PFturn || !Pturn)
            {
                await AllIn();
                if (PFturn && !pFolded)
                {
                    if (bCall.Text.Contains("All in") == false || bRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        pFolded = true;
                    }
                }

                await CheckRaise(0, 0);
                pbTimer.Visible = false;
                bRaise.Enabled = false;
                bCall.Enabled = false;
                bRaise.Enabled = false;
                bRaise.Enabled = false;
                bFold.Enabled = false;
                timer.Stop();
                B1turn = true;
                if (!B1Fturn)
                {
                    if (B1turn)
                    {
                        FixCall(b1Status, ref b1Call, ref b1Raise, 1);
                        FixCall(b1Status, ref b1Call, ref b1Raise, 2);
                        Rules(2, 3, "Bot 1", ref b1Type, ref b1Power, B1Fturn);
                        MessageBox.Show("Bot 1's Turn");
                        AI(2, 3, ref bot1Chips, ref B1turn, ref  B1Fturn, b1Status, 0, b1Power, b1Type);
                        turnCount++;
                        last = 1;
                        B1turn = false;
                        B2turn = true;
                    }
                }

                if (B1Fturn && !b1Folded)
                {
                    bools.RemoveAt(1);
                    bools.Insert(1, null);
                    maxLeft--;
                    b1Folded = true;
                }

                if (B1Fturn || !B1turn)
                {
                    await CheckRaise(1, 1);
                    B2turn = true;
                }

                if (!B2Fturn)
                {
                    if (B2turn)
                    {
                        FixCall(b2Status, ref b2Call, ref b2Raise, 1);
                        FixCall(b2Status, ref b2Call, ref b2Raise, 2);
                        Rules(4, 5, "Bot 2", ref b2Type, ref b2Power, B2Fturn);
                        MessageBox.Show("Bot 2's Turn");
                        AI(4, 5, ref bot2Chips, ref B2turn, ref  B2Fturn, b2Status, 1, b2Power, b2Type);
                        turnCount++;
                        last = 2;
                        B2turn = false;
                        B3turn = true;
                    }
                }

                if (B2Fturn && !b2Folded)
                {
                    bools.RemoveAt(2);
                    bools.Insert(2, null);
                    maxLeft--;
                    b2Folded = true;
                }

                if (B2Fturn || !B2turn)
                {
                    await CheckRaise(2, 2);
                    B3turn = true;
                }

                if (!B3Fturn)
                {
                    if (B3turn)
                    {
                        FixCall(b3Status, ref b3Call, ref b3Raise, 1);
                        FixCall(b3Status, ref b3Call, ref b3Raise, 2);
                        Rules(6, 7, "Bot 3", ref b3Type, ref b3Power, B3Fturn);
                        MessageBox.Show("Bot 3's Turn");
                        AI(6, 7, ref bot3Chips, ref B3turn, ref  B3Fturn, b3Status, 2, b3Power, b3Type);
                        turnCount++;
                        last = 3;
                        B3turn = false;
                        B4turn = true;
                    }
                }

                if (B3Fturn && !b3Folded)
                {
                    bools.RemoveAt(3);
                    bools.Insert(3, null);
                    maxLeft--;
                    b3Folded = true;
                }

                if (B3Fturn || !B3turn)
                {
                    await CheckRaise(3, 3);
                    B4turn = true;
                }

                if (!B4Fturn)
                {
                    if (B4turn)
                    {
                        FixCall(b4Status, ref b4Call, ref b4Raise, 1);
                        FixCall(b4Status, ref b4Call, ref b4Raise, 2);
                        Rules(8, 9, "Bot 4", ref b4Type, ref b4Power, B4Fturn);
                        MessageBox.Show("Bot 4's Turn");
                        AI(8, 9, ref bot4Chips, ref B4turn, ref  B4Fturn, b4Status, 3, b4Power, b4Type);
                        turnCount++;
                        last = 4;
                        B4turn = false;
                        B5turn = true;
                    }
                }

                if (B4Fturn && !b4Folded)
                {
                    bools.RemoveAt(4);
                    bools.Insert(4, null);
                    maxLeft--;
                    b4Folded = true;
                }

                if (B4Fturn || !B4turn)
                {
                    await CheckRaise(4, 4);
                    B5turn = true;
                }

                if (!B5Fturn)
                {
                    if (B5turn)
                    {
                        FixCall(b5Status, ref b5Call, ref b5Raise, 1);
                        FixCall(b5Status, ref b5Call, ref b5Raise, 2);
                        Rules(10, 11, "Bot 5", ref b5Type, ref b5Power, B5Fturn);
                        MessageBox.Show("Bot 5's Turn");
                        AI(10, 11, ref bot5Chips, ref B5turn, ref  B5Fturn, b5Status, 4, b5Power, b5Type);
                        turnCount++;
                        last = 5;
                        B5turn = false;
                    }
                }

                if (B5Fturn && !b5Folded)
                {
                    bools.RemoveAt(5);
                    bools.Insert(5, null);
                    maxLeft--;
                    b5Folded = true;
                }

                if (B5Fturn || !B5turn)
                {
                    await CheckRaise(5, 5);
                    Pturn = true;
                }

                if (PFturn && !pFolded)
                {
                    if (bCall.Text.Contains("All in") == false || bRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        pFolded = true;
                    }
                }
            
                await AllIn();
                if (!restart)
                {
                    await Turns();
                }

                restart = false;
            }
        }

        void Rules(int c1, int c2, string currentText, ref double current, ref double Power, bool foldedTurn)
        {
            if (c1 == 0 && c2 == 1)
            {
            }

            if (!foldedTurn || c1 == 0 && c2 == 1 && pStatus.Text.Contains("Fold") == false)
            {
                
                bool done = false, vf = false;
                int[] Straight1 = new int[5];
                int[] Straight = new int[7];
                Straight[0] = Reserve[c1];
                Straight[1] = Reserve[c2];
                Straight1[0] = Straight[2] = Reserve[12];
                Straight1[1] = Straight[3] = Reserve[13];
                Straight1[2] = Straight[4] = Reserve[14];
                Straight1[3] = Straight[5] = Reserve[15];
                Straight1[4] = Straight[6] = Reserve[16];
                var a = Straight.Where(o => o % 4 == 0).ToArray();
                var b = Straight.Where(o => o % 4 == 1).ToArray();
                var c = Straight.Where(o => o % 4 == 2).ToArray();
                var d = Straight.Where(o => o % 4 == 3).ToArray();
                var st1 = a.Select(o => o / 4).Distinct().ToArray();
                var st2 = b.Select(o => o / 4).Distinct().ToArray();
                var st3 = c.Select(o => o / 4).Distinct().ToArray();
                var st4 = d.Select(o => o / 4).Distinct().ToArray();
                Array.Sort(Straight); Array.Sort(st1); Array.Sort(st2); Array.Sort(st3); Array.Sort(st4);
                
                for (int i = 0; i < 16; i++)
                {
                    if (Reserve[i] == int.Parse(Holder[c1].Tag.ToString()) && Reserve[i + 1] == int.Parse(Holder[c2].Tag.ToString()))
                    {
                        // Pair from Hand current = 1
                        rPairFromHand(ref current, ref Power);

                        #region Pair or Two Pair from Table current = 2 || 0
                        rPairTwoPair(ref current, ref Power);
                        #endregion

                        #region Two Pair current = 2
                        rTwoPair(ref current, ref Power);
                        #endregion

                        #region Three of a kind current = 3
                        rThreeOfAKind(ref current, ref Power, Straight);
                        #endregion

                        #region Straight current = 4
                        rStraight(ref current, ref Power, Straight);
                        #endregion

                        #region Flush current = 5 || 5.5
                        rFlush(ref current, ref Power, ref vf, Straight1);
                        #endregion

                        #region Full House current = 6
                        rFullHouse(ref current, ref Power, ref done, Straight);
                        #endregion

                        #region Four of a Kind current = 7
                        rFourOfAKind(ref current, ref Power, Straight);
                        #endregion

                        #region Straight Flush current = 8 || 9
                        rStraightFlush(ref current, ref Power, st1, st2, st3, st4);
                        #endregion

                        #region High Card current = -1
                        rHighCard(ref current, ref Power);
                        #endregion
                    }
                }
            }
        }

        private void rStraightFlush(ref double current, ref double Power, int[] st1, int[] st2, int[] st3, int[] st4)
        {
            if (current >= -1)
            {
                if (st1.Length >= 5)
                {
                    if (st1[0] + 4 == st1[4])
                    {
                        current = 8;
                        Power = st1.Max() / 4 + current * 100;
                        Win.Add(new Type { Power = Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st1[0] == 0 && st1[1] == 9 && st1[2] == 10 && st1[3] == 11 && st1[0] + 12 == st1[4])
                    {
                        current = 9;
                        Power = st1.Max() / 4 + current * 100;
                        Win.Add(new Type { Power = Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (st2.Length >= 5)
                {
                    if (st2[0] + 4 == st2[4])
                    {
                        current = 8;
                        Power = st2.Max() / 4 + current * 100;
                        Win.Add(new Type { Power = Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st2[0] == 0 && st2[1] == 9 && st2[2] == 10 && st2[3] == 11 && st2[0] + 12 == st2[4])
                    {
                        current = 9;
                        Power = st2.Max() / 4 + current * 100;
                        Win.Add(new Type { Power = Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (st3.Length >= 5)
                {
                    if (st3[0] + 4 == st3[4])
                    {
                        current = 8;
                        Power = st3.Max() / 4 + current * 100;
                        Win.Add(new Type { Power = Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st3[0] == 0 && st3[1] == 9 && st3[2] == 10 && st3[3] == 11 && st3[0] + 12 == st3[4])
                    {
                        current = 9;
                        Power = st3.Max() / 4 + current * 100;
                        Win.Add(new Type { Power = Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (st4.Length >= 5)
                {
                    if (st4[0] + 4 == st4[4])
                    {
                        current = 8;
                        Power = st4.Max() / 4 + current * 100;
                        Win.Add(new Type { Power = Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st4[0] == 0 && st4[1] == 9 && st4[2] == 10 && st4[3] == 11 && st4[0] + 12 == st4[4])
                    {
                        current = 9;
                        Power = st4.Max() / 4 + current * 100;
                        Win.Add(new Type { Power = Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void rFourOfAKind(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4 &&
                        Straight[j] / 4 == Straight[j + 3] / 4)
                    {
                        current = 7;
                        Power = (Straight[j] / 4) * 4 + current * 100;
                        Win.Add(new Type { Power = Power, Current = 7 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
                    {
                        current = 7;
                        Power = 13 * 4 + current * 100;
                        Win.Add(new Type { Power = Power, Current = 7 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void rFullHouse(ref double current, ref double Power, ref bool done, int[] Straight)
        {
            if (current >= -1)
            {
                type = Power;
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                current = 6;
                                Power = 13 * 2 + current * 100;
                                Win.Add(new Type { Power = Power, Current = 6 });
                                sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }

                            if (fh.Max() / 4 > 0)
                            {
                                current = 6;
                                Power = fh.Max() / 4 * 2 + current * 100;
                                Win.Add(new Type { Power = Power, Current = 6 });
                                sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                        }

                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                Power = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                Power = fh.Max() / 4;
                                done = true;
                                j = -1;
                            }
                        }
                    }
                }

                if (current != 6)
                {
                    Power = type;
                }
            }
        }

        private void rFlush(ref double current, ref double Power, ref bool vf, int[] Straight1)
        {
            if (current >= -1)
            {
	            var f1 = Straight1.Where(o => o % 4 == 0).ToArray();
	            var f2 = Straight1.Where(o => o % 4 == 1).ToArray();
	            var f3 = Straight1.Where(o => o % 4 == 2).ToArray();
	            var f4 = Straight1.Where(o => o % 4 == 3).ToArray();
	            if (f1.Length == 3 || f1.Length == 4)
	            {
		            if (Reserve[i] % 4 == Reserve[i + 1] % 4 && Reserve[i] % 4 == f1[0] % 4)
		            {
			            if (Reserve[i] / 4 > f1.Max() / 4)
			            {
				            current = 5;
				            Power = Reserve[i] + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }

			            if (Reserve[i + 1] / 4 > f1.Max() / 4)
			            {
				            current = 5;
				            Power = Reserve[i + 1] + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
			            else if (Reserve[i] / 4 < f1.Max() / 4 && Reserve[i + 1] / 4 < f1.Max() / 4)
			            {
				            current = 5;
				            Power = f1.Max() + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
		            }
	            }

	            if (f1.Length == 4)
	            {
		            // different cards in hand
		            if (Reserve[i] % 4 != Reserve[i + 1] % 4 && Reserve[i] % 4 == f1[0] % 4)
		            {
			            if (Reserve[i] / 4 > f1.Max() / 4)
			            {
				            current = 5;
				            Power = Reserve[i] + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
			            else
			            {
				            current = 5;
				            Power = f1.Max() + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
		            }

		            if (Reserve[i + 1] % 4 != Reserve[i] % 4 && Reserve[i + 1] % 4 == f1[0] % 4)
		            {
			            if (Reserve[i + 1] / 4 > f1.Max() / 4)
			            {
				            current = 5;
				            Power = Reserve[i + 1] + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
			            else
			            {
				            current = 5;
				            Power = f1.Max() + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
		            }
	            }

	            if (f1.Length == 5)
	            {
		            if (Reserve[i] % 4 == f1[0] % 4 && Reserve[i] / 4 > f1.Min() / 4)
		            {
			            current = 5;
			            Power = Reserve[i] + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
			            vf = true;
		            }

		            if (Reserve[i + 1] % 4 == f1[0] % 4 && Reserve[i + 1] / 4 > f1.Min() / 4)
		            {
			            current = 5;
			            Power = Reserve[i + 1] + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
			            vf = true;
		            }
		            else if (Reserve[i] / 4 < f1.Min() / 4 && Reserve[i + 1] / 4 < f1.Min())
		            {
			            current = 5;
			            Power = f1.Max() + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
			            vf = true;
		            }
	            }

	            if (f2.Length == 3 || f2.Length == 4)
	            {
		            if (Reserve[i] % 4 == Reserve[i + 1] % 4 && Reserve[i] % 4 == f2[0] % 4)
		            {
			            if (Reserve[i] / 4 > f2.Max() / 4)
			            {
				            current = 5;
				            Power = Reserve[i] + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }

			            if (Reserve[i + 1] / 4 > f2.Max() / 4)
			            {
				            current = 5;
				            Power = Reserve[i + 1] + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
			            else if (Reserve[i] / 4 < f2.Max() / 4 && Reserve[i + 1] / 4 < f2.Max() / 4)
			            {
				            current = 5;
				            Power = f2.Max() + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
		            }
	            }

	            if (f2.Length == 4)
	            {
		            // different cards in hand
		            if (Reserve[i] % 4 != Reserve[i + 1] % 4 && Reserve[i] % 4 == f2[0] % 4)
		            {
			            if (Reserve[i] / 4 > f2.Max() / 4)
			            {
				            current = 5;
				            Power = Reserve[i] + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
			            else
			            {
				            current = 5;
				            Power = f2.Max() + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
		            }

		            if (Reserve[i + 1] % 4 != Reserve[i] % 4 && Reserve[i + 1] % 4 == f2[0] % 4)
		            {
			            if (Reserve[i + 1] / 4 > f2.Max() / 4)
			            {
				            current = 5;
				            Power = Reserve[i + 1] + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
			            else
			            {
				            current = 5;
				            Power = f2.Max() + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
		            }
	            }

	            if (f2.Length == 5)
	            {
		            if (Reserve[i] % 4 == f2[0] % 4 && Reserve[i] / 4 > f2.Min() / 4)
		            {
			            current = 5;
			            Power = Reserve[i] + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
			            vf = true;
		            }

		            if (Reserve[i + 1] % 4 == f2[0] % 4 && Reserve[i + 1] / 4 > f2.Min() / 4)
		            {
			            current = 5;
			            Power = Reserve[i + 1] + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
			            vf = true;
		            }
		            else if (Reserve[i] / 4 < f2.Min() / 4 && Reserve[i + 1] / 4 < f2.Min())
		            {
			            current = 5;
			            Power = f2.Max() + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
			            vf = true;
		            }
	            }

	            if (f3.Length == 3 || f3.Length == 4)
	            {
		            if (Reserve[i] % 4 == Reserve[i + 1] % 4 && Reserve[i] % 4 == f3[0] % 4)
		            {
			            if (Reserve[i] / 4 > f3.Max() / 4)
			            {
				            current = 5;
				            Power = Reserve[i] + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }

			            if (Reserve[i + 1] / 4 > f3.Max() / 4)
			            {
				            current = 5;
				            Power = Reserve[i + 1] + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
			            else if (Reserve[i] / 4 < f3.Max() / 4 && Reserve[i + 1] / 4 < f3.Max() / 4)
			            {
				            current = 5;
				            Power = f3.Max() + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
		            }
	            }

	            if (f3.Length == 4)
	            {
		            // different cards in hand
		            if (Reserve[i] % 4 != Reserve[i + 1] % 4 && Reserve[i] % 4 == f3[0] % 4)
		            {
			            if (Reserve[i] / 4 > f3.Max() / 4)
			            {
				            current = 5;
				            Power = Reserve[i] + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
			            else
			            {
				            current = 5;
				            Power = f3.Max() + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
		            }

		            if (Reserve[i + 1] % 4 != Reserve[i] % 4 && Reserve[i + 1] % 4 == f3[0] % 4)
		            {
			            if (Reserve[i + 1] / 4 > f3.Max() / 4)
			            {
				            current = 5;
				            Power = Reserve[i + 1] + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
			            else
			            {
				            current = 5;
				            Power = f3.Max() + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
		            }
	            }

	            if (f3.Length == 5)
	            {
		            if (Reserve[i] % 4 == f3[0] % 4 && Reserve[i] / 4 > f3.Min() / 4)
		            {
			            current = 5;
			            Power = Reserve[i] + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
			            vf = true;
		            }

		            if (Reserve[i + 1] % 4 == f3[0] % 4 && Reserve[i + 1] / 4 > f3.Min() / 4)
		            {
			            current = 5;
			            Power = Reserve[i + 1] + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
			            vf = true;
		            }
		            else if (Reserve[i] / 4 < f3.Min() / 4 && Reserve[i + 1] / 4 < f3.Min())
		            {
			            current = 5;
			            Power = f3.Max() + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
			            vf = true;
		            }
	            }

	            if (f4.Length == 3 || f4.Length == 4)
	            {
		            if (Reserve[i] % 4 == Reserve[i + 1] % 4 && Reserve[i] % 4 == f4[0] % 4)
		            {
			            if (Reserve[i] / 4 > f4.Max() / 4)
			            {
				            current = 5;
				            Power = Reserve[i] + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }

			            if (Reserve[i + 1] / 4 > f4.Max() / 4)
			            {
				            current = 5;
				            Power = Reserve[i + 1] + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
			            else if (Reserve[i] / 4 < f4.Max() / 4 && Reserve[i + 1] / 4 < f4.Max() / 4)
			            {
				            current = 5;
				            Power = f4.Max() + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
		            }
	            }

	            if (f4.Length == 4)
	            {
		            // different cards in hand
		            if (Reserve[i] % 4 != Reserve[i + 1] % 4 && Reserve[i] % 4 == f4[0] % 4)
		            {
			            if (Reserve[i] / 4 > f4.Max() / 4)
			            {
				            current = 5;
				            Power = Reserve[i] + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
			            else
			            {
				            current = 5;
				            Power = f4.Max() + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
		            }

		            if (Reserve[i + 1] % 4 != Reserve[i] % 4 && Reserve[i + 1] % 4 == f4[0] % 4)
		            {
			            if (Reserve[i + 1] / 4 > f4.Max() / 4)
			            {
				            current = 5;
				            Power = Reserve[i + 1] + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
			            else
			            {
				            current = 5;
				            Power = f4.Max() + current * 100;
				            Win.Add(new Type { Power = Power, Current = 5 });
				            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
				            vf = true;
			            }
		            }
	            }

	            if (f4.Length == 5)
	            {
		            if (Reserve[i] % 4 == f4[0] % 4 && Reserve[i] / 4 > f4.Min() / 4)
		            {
			            current = 5;
			            Power = Reserve[i] + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
			            vf = true;
		            }

		            if (Reserve[i + 1] % 4 == f4[0] % 4 && Reserve[i + 1] / 4 > f4.Min() / 4)
		            {
			            current = 5;
			            Power = Reserve[i + 1] + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
			            vf = true;
		            }
		            else if (Reserve[i] / 4 < f4.Min() / 4 && Reserve[i + 1] / 4 < f4.Min())
		            {
			            current = 5;
			            Power = f4.Max() + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
			            vf = true;
		            }
	            }

	            // ace
	            if (f1.Length > 0)
	            {
		            if (Reserve[i] / 4 == 0 && Reserve[i] % 4 == f1[0] % 4 && vf && f1.Length > 0)
		            {
			            current = 5.5;
			            Power = 13 + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5.5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
		            }

		            if (Reserve[i + 1] / 4 == 0 && Reserve[i + 1] % 4 == f1[0] % 4 && vf && f1.Length > 0)
		            {
			            current = 5.5;
			            Power = 13 + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5.5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
		            }
	            }

	            if (f2.Length > 0)
	            {
		            if (Reserve[i] / 4 == 0 && Reserve[i] % 4 == f2[0] % 4 && vf && f2.Length > 0)
		            {
			            current = 5.5;
			            Power = 13 + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5.5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
		            }

		            if (Reserve[i + 1] / 4 == 0 && Reserve[i + 1] % 4 == f2[0] % 4 && vf && f2.Length > 0)
		            {
			            current = 5.5;
			            Power = 13 + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5.5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
		            }
	            }

	            if (f3.Length > 0)
	            {
		            if (Reserve[i] / 4 == 0 && Reserve[i] % 4 == f3[0] % 4 && vf && f3.Length > 0)
		            {
			            current = 5.5;
			            Power = 13 + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5.5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
		            }

		            if (Reserve[i + 1] / 4 == 0 && Reserve[i + 1] % 4 == f3[0] % 4 && vf && f3.Length > 0)
		            {
			            current = 5.5;
			            Power = 13 + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5.5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
		            }
	            }

	            if (f4.Length > 0)
	            {
		            if (Reserve[i] / 4 == 0 && Reserve[i] % 4 == f4[0] % 4 && vf && f4.Length > 0)
		            {
			            current = 5.5;
			            Power = 13 + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5.5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
		            }

		            if (Reserve[i + 1] / 4 == 0 && Reserve[i + 1] % 4 == f4[0] % 4 && vf)
		            {
			            current = 5.5;
			            Power = 13 + current * 100;
			            Win.Add(new Type { Power = Power, Current = 5.5 });
			            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
		            }
	            }
            }
        }

        private void rStraight(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                var op = Straight.Select(o => o / 4).Distinct().ToArray();
                for (int j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {
                            current = 4;
                            Power = op.Max() + current * 100;
                            Win.Add(new Type { Power = Power, Current = 4 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                        else
                        {
                            current = 4;
                            Power = op[j + 4] + current * 100;
                            Win.Add(new Type { Power = Power, Current = 4 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                    }

                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        current = 4;
                        Power = 13 + current * 100;
                        Win.Add(new Type { Power = Power, Current = 4 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void rThreeOfAKind(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            current = 3;
                            Power = 13 * 3 + current * 100;
                            Win.Add(new Type { Power = Power, Current = 3 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 3;
                            Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + current * 100;
                            Win.Add(new Type { Power = Power, Current = 3 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                }
            }
        }

        private void rTwoPair(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    if (Reserve[i] / 4 != Reserve[i + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }

                            if (tc - k >= 12)
                            {
                                if (Reserve[i] / 4 == Reserve[tc] / 4 && Reserve[i + 1] / 4 == Reserve[tc - k] / 4 ||
                                    Reserve[i + 1] / 4 == Reserve[tc] / 4 && Reserve[i] / 4 == Reserve[tc - k] / 4)
                                {
                                    if (!msgbox)
                                    {
                                        if (Reserve[i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (Reserve[i + 1] / 4) * 2 + current * 100;
                                            Win.Add(new Type { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }

                                        if (Reserve[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (Reserve[i] / 4) * 2 + current * 100;
                                            Win.Add(new Type { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }

                                        if (Reserve[i + 1] / 4 != 0 && Reserve[i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (Reserve[i] / 4) * 2 + (Reserve[i + 1] / 4) * 2 + current * 100;
                                            Win.Add(new Type { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }

                                    msgbox = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void rPairTwoPair(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                bool msgbox1 = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    for (int k = 1; k <= max; k++)
                    {
                        if (tc - k < 12)
                        {
                            max--;
                        }

                        if (tc - k >= 12)
                        {
                            if (Reserve[tc] / 4 == Reserve[tc - k] / 4)
                            {
                                if (Reserve[tc] / 4 != Reserve[i] / 4 && Reserve[tc] / 4 != Reserve[i + 1] / 4 && current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (Reserve[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (Reserve[i] / 4) * 2 + 13 * 4 + current * 100;
                                            Win.Add(new Type { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }

                                        if (Reserve[i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (Reserve[i + 1] / 4) * 2 + 13 * 4 + current * 100;
                                            Win.Add(new Type { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }

                                        if (Reserve[i + 1] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (Reserve[tc] / 4) * 2 + (Reserve[i + 1] / 4) * 2 + current * 100;
                                            Win.Add(new Type { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }

                                        if (Reserve[i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (Reserve[tc] / 4) * 2 + (Reserve[i] / 4) * 2 + current * 100;
                                            Win.Add(new Type { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }

                                    msgbox = true;
                                }

                                if (current == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (Reserve[i] / 4 > Reserve[i + 1] / 4)
                                        {
                                            if (Reserve[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + Reserve[i] / 4 + current * 100;
                                                Win.Add(new Type { Power = Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = Reserve[tc] / 4 + Reserve[i] / 4 + current * 100;
                                                Win.Add(new Type { Power = Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                        else
                                        {
                                            if (Reserve[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + Reserve[i + 1] + current * 100;
                                                Win.Add(new Type { Power = Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = Reserve[tc] / 4 + Reserve[i + 1] / 4 + current * 100;
                                                Win.Add(new Type { Power = Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                    }

                                    msgbox1 = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void rPairFromHand(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                if (Reserve[i] / 4 == Reserve[i + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (Reserve[i] / 4 == 0)
                        {
                            current = 1;
                            Power = 13 * 4 + current * 100;
                            Win.Add(new Type { Power = Power, Current = 1 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 1;
                            Power = (Reserve[i + 1] / 4) * 4 + current * 100;
                            Win.Add(new Type { Power = Power, Current = 1 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }

                    msgbox = true;
                }

                for (int tc = 16; tc >= 12; tc--)
                {
                    if (Reserve[i + 1] / 4 == Reserve[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (Reserve[i + 1] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + Reserve[i] / 4 + current * 100;
                                Win.Add(new Type { Power = Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (Reserve[i + 1] / 4) * 4 + Reserve[i] / 4 + current * 100;
                                Win.Add(new Type { Power = Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }

                        msgbox = true;
                    }

                    if (Reserve[i] / 4 == Reserve[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (Reserve[i] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + Reserve[i + 1] / 4 + current * 100;
                                Win.Add(new Type { Power = Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (Reserve[tc] / 4) * 4 + Reserve[i + 1] / 4 + current * 100;
                                Win.Add(new Type { Power = Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }

                        msgbox = true;
                    }
                }
            }
        }

        private void rHighCard(ref double current, ref double Power)
        {
            if (current == -1)
            {
                if (Reserve[i] / 4 > Reserve[i + 1] / 4)
                {
                    current = -1;
                    Power = Reserve[i] / 4;
                    Win.Add(new Type { Power = Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    current = -1;
                    Power = Reserve[i + 1] / 4;
                    Win.Add(new Type { Power = Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }

                if (Reserve[i] / 4 == 0 || Reserve[i + 1] / 4 == 0)
                {
                    current = -1;
                    Power = 13;
                    Win.Add(new Type { Power = Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }

        void Winner(double current, double Power, string currentText, int chips, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }

            for (int j = 0; j <= 16; j++)
            {
                // await Task.Delay(5);
                if (Holder[j].Visible)
                    Holder[j].Image = Deck[j];
            }

            if (current == sorted.Current)
            {
                if (Power == sorted.Power)
                {
                    winners++;
                    CheckWinners.Add(currentText);
                    if (current == -1)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }

                    if (current == 1 || current == 0)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }

                    if (current == 2)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }

                    if (current == 3)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }

                    if (current == 4)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }

                    if (current == 5 || current == 5.5)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }

                    if (current == 6)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }

                    if (current == 7)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }

                    if (current == 8)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }

                    if (current == 9)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }

            if (currentText == lastly)
            {
	            // lastfixed
                if (winners > 1)
                {
	                if (CheckWinners.Contains("Player"))
	                {
		                this.chips += int.Parse(tbPot.Text) / winners;
		                tbChips.Text = this.chips.ToString();

		                // playerPanel.Visible = true;
	                }

	                if (CheckWinners.Contains("Bot 1"))
	                {
		                bot1Chips += int.Parse(tbPot.Text) / winners;
		                tbBotChips1.Text = bot1Chips.ToString();

		                // firstBotPanel.Visible = true;
	                }

	                if (CheckWinners.Contains("Bot 2"))
	                {
		                bot2Chips += int.Parse(tbPot.Text) / winners;
		                tbBotChips2.Text = bot2Chips.ToString();

		                // secondBotPanel.Visible = true;
	                }

	                if (CheckWinners.Contains("Bot 3"))
	                {
		                bot3Chips += int.Parse(tbPot.Text) / winners;
		                tbBotChips3.Text = bot3Chips.ToString();

		                // thirdBotPanel.Visible = true;
	                }

	                if (CheckWinners.Contains("Bot 4"))
	                {
		                bot4Chips += int.Parse(tbPot.Text) / winners;
		                tbBotChips4.Text = bot4Chips.ToString();

		                // fourthBotPanel.Visible = true;
	                }

	                if (CheckWinners.Contains("Bot 5"))
	                {
		                bot5Chips += int.Parse(tbPot.Text) / winners;
		                tbBotChips5.Text = bot5Chips.ToString();

		                // fifthBotPlayer.Visible = true;
	                }

	                // await Finish(1);
                }

	            if (winners == 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
	                    this.chips += int.Parse(tbPot.Text);

	                    // await Finish(1);
	                    // playerPanel.Visible = true;
                    }

	                if (CheckWinners.Contains("Bot 1"))
	                {
		                bot1Chips += int.Parse(tbPot.Text);

		                // await Finish(1);
		                // firstBotPanel.Visible = true;
	                }

	                if (CheckWinners.Contains("Bot 2"))
	                {
		                bot2Chips += int.Parse(tbPot.Text);

		                // await Finish(1);
		                // secondBotPanel.Visible = true;
	                }

	                if (CheckWinners.Contains("Bot 3"))
	                {
		                bot3Chips += int.Parse(tbPot.Text);

		                // await Finish(1);
		                // thirdBotPanel.Visible = true;
	                }

	                if (CheckWinners.Contains("Bot 4"))
	                {
		                bot4Chips += int.Parse(tbPot.Text);

		                // await Finish(1);
		                // fourthBotPanel.Visible = true;
	                }

	                if (CheckWinners.Contains("Bot 5"))
	                {
		                bot5Chips += int.Parse(tbPot.Text);

		                // await Finish(1);
		                // fifthBotPlayer.Visible = true;
	                }
                }
            }
        }

        async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (raising)
            {
                turnCount = 0;
                raising = false;
                raisedTurn = currentTurn;
                changed = true;
            }
            else
            {
                if (turnCount >= maxLeft - 1 || !changed && turnCount == maxLeft)
                {
                    if (currentTurn == raisedTurn - 1 || !changed && turnCount == maxLeft || raisedTurn == 0 && currentTurn == 5)
                    {
                        changed = false;
                        turnCount = 0;
                        Raise = 0;
                        call = 0;
                        raisedTurn = 123;
                        rounds++;
                        if (!PFturn)
                            pStatus.Text = string.Empty;
                        if (!B1Fturn)
                            b1Status.Text = string.Empty;
                        if (!B2Fturn)
                            b2Status.Text = string.Empty;
                        if (!B3Fturn)
                            b3Status.Text = string.Empty;
                        if (!B4Fturn)
                            b4Status.Text = string.Empty;
                        if (!B5Fturn)
                            b5Status.Text = string.Empty;
                    }
                }
            }

            if (rounds == Flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    if (Holder[j].Image != Deck[j])
                    {
                        Holder[j].Image = Deck[j];
                        pCall = 0; pRaise = 0;
                        b1Call = 0; b1Raise = 0;
                        b2Call = 0; b2Raise = 0;
                        b3Call = 0; b3Raise = 0;
                        b4Call = 0; b4Raise = 0;
                        b5Call = 0; b5Raise = 0;
                    }
                }
            }

            if (rounds == Turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (Holder[j].Image != Deck[j])
                    {
                        Holder[j].Image = Deck[j];
                        pCall = 0; pRaise = 0;
                        b1Call = 0; b1Raise = 0;
                        b2Call = 0; b2Raise = 0;
                        b3Call = 0; b3Raise = 0;
                        b4Call = 0; b4Raise = 0;
                        b5Call = 0; b5Raise = 0;
                    }
                }
            }

            if (rounds == River)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (Holder[j].Image != Deck[j])
                    {
                        Holder[j].Image = Deck[j];
                        pCall = 0; pRaise = 0;
                        b1Call = 0; b1Raise = 0;
                        b2Call = 0; b2Raise = 0;
                        b3Call = 0; b3Raise = 0;
                        b4Call = 0; b4Raise = 0;
                        b5Call = 0; b5Raise = 0;
                    }
                }
            }

            if (rounds == End && maxLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!pStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    Rules(0, 1, "Player", ref pType, ref pPower, PFturn);
                }

                if (!b1Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    Rules(2, 3, "Bot 1", ref b1Type, ref b1Power, B1Fturn);
                }

                if (!b2Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    Rules(4, 5, "Bot 2", ref b2Type, ref b2Power, B2Fturn);
                }

                if (!b3Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    Rules(6, 7, "Bot 3", ref b3Type, ref b3Power, B3Fturn);
                }

                if (!b4Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    Rules(8, 9, "Bot 4", ref b4Type, ref b4Power, B4Fturn);
                }

                if (!b5Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    Rules(10, 11, "Bot 5", ref b5Type, ref b5Power, B5Fturn);
                }

                Winner(pType, pPower, "Player", chips, fixedLast);
                Winner(b1Type, b1Power, "Bot 1", bot1Chips, fixedLast);
                Winner(b2Type, b2Power, "Bot 2", bot2Chips, fixedLast);
                Winner(b3Type, b3Power, "Bot 3", bot3Chips, fixedLast);
                Winner(b4Type, b4Power, "Bot 4", bot4Chips, fixedLast);
                Winner(b5Type, b5Power, "Bot 5", bot5Chips, fixedLast);
                restart = true;
                Pturn = true;
                PFturn = false;
                B1Fturn = false;
                B2Fturn = false;
                B3Fturn = false;
                B4Fturn = false;
                B5Fturn = false;
                if (chips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        chips = f2.a;
                        bot1Chips += f2.a;
                        bot2Chips += f2.a;
                        bot3Chips += f2.a;
                        bot4Chips += f2.a;
                        bot5Chips += f2.a;
                        PFturn = false;
                        Pturn = true;
                        bRaise.Enabled = true;
                        bFold.Enabled = true;
                        bCheck.Enabled = true;
                        bRaise.Text = "Raise";
                    }
                }

                playerPanel.Visible = false; firstBotPanel.Visible = false; secondBotPanel.Visible = false; thirdBotPanel.Visible = false; fourthBotPanel.Visible = false; fifthBotPlayer.Visible = false;
                pCall = 0; pRaise = 0;
                b1Call = 0; b1Raise = 0;
                b2Call = 0; b2Raise = 0;
                b3Call = 0; b3Raise = 0;
                b4Call = 0; b4Raise = 0;
                b5Call = 0; b5Raise = 0;
                last = 0;
                call = bb;
                Raise = 0;
                ImgLocation = Directory.GetFiles(AssetsPath + @"Cards", "*.png", SearchOption.TopDirectoryOnly);
                bools.Clear();
                rounds = 0;
                pPower = 0; pType = -1;
                type = 0; b1Power = 0; b2Power = 0; b3Power = 0; b4Power = 0; b5Power = 0;
                b1Type = -1; b2Type = -1; b3Type = -1; b4Type = -1; b5Type = -1;
                ints.Clear();
                CheckWinners.Clear();
                winners = 0;
                Win.Clear();
                sorted.Current = 0;
                sorted.Power = 0;
                for (int os = 0; os < 17; os++)
                {
                    Holder[os].Image = null;
                    Holder[os].Invalidate();
                    Holder[os].Visible = false;
                }

                tbPot.Text = "0";
                pStatus.Text = string.Empty;
                await Shuffle();
                await Turns();
            }
        }

        void FixCall(Label status, ref int cCall, ref int cRaise, int options)
        {
            if (rounds != 4)
            {
                if (options == 1)
                {
                    if (status.Text.Contains("Raise"))
                    {
                        var changeRaise = status.Text.Substring(6);
                        cRaise = int.Parse(changeRaise);
                    }

                    if (status.Text.Contains("Call"))
                    {
                        var changeCall = status.Text.Substring(5);
                        cCall = int.Parse(changeCall);
                    }

                    if (status.Text.Contains("Check"))
                    {
                        cRaise = 0;
                        cCall = 0;
                    }
                }

                if (options == 2)
                {
                    if (cRaise != Raise && cRaise <= Raise)
                    {
                        call = Convert.ToInt32(Raise) - cRaise;
                    }

                    if (cCall != call || cCall <= call)
                    {
                        call = call - cCall;
                    }

                    if (cRaise == Raise && Raise > 0)
                    {
                        call = 0;
                        bCall.Enabled = false;
                        bCall.Text = "Callisfuckedup";
                    }
                }
            }
        }

        async Task AllIn()
        {
            
            if (chips <= 0 && !intsadded)
            {
                if (pStatus.Text.Contains("Raise"))
                {
                    ints.Add(chips);
                    intsadded = true;
                }

                if (pStatus.Text.Contains("Call"))
                {
                    ints.Add(chips);
                    intsadded = true;
                }
            }

            intsadded = false;
            if (bot1Chips <= 0 && !B1Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(bot1Chips);
                    intsadded = true;
                }

                intsadded = false;
            }

            if (bot2Chips <= 0 && !B2Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(bot2Chips);
                    intsadded = true;
                }

                intsadded = false;
            }

            if (bot3Chips <= 0 && !B3Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(bot3Chips);
                    intsadded = true;
                }

                intsadded = false;
            }

            if (bot4Chips <= 0 && !B4Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(bot4Chips);
                    intsadded = true;
                }

                intsadded = false;
            }

            if (bot5Chips <= 0 && !B5Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(bot5Chips);
                    intsadded = true;
                }
            }

            if (ints.ToArray().Length == maxLeft)
            {
                await Finish(2);
            }
            else
            {
                ints.Clear();
            }
            

            var abc = bools.Count(x => x == false);

            #region LastManStanding
            if (abc == 1)
            {
                int index = bools.IndexOf(false);
                if (index == 0)
                {
                    chips += int.Parse(tbPot.Text);
                    tbChips.Text = chips.ToString();
                    playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }

                if (index == 1)
                {
                    bot1Chips += int.Parse(tbPot.Text);
                    tbChips.Text = bot1Chips.ToString();
                    firstBotPanel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }

                if (index == 2)
                {
                    bot2Chips += int.Parse(tbPot.Text);
                    tbChips.Text = bot2Chips.ToString();
                    secondBotPanel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }

                if (index == 3)
                {
                    bot3Chips += int.Parse(tbPot.Text);
                    tbChips.Text = bot3Chips.ToString();
                    thirdBotPanel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }

                if (index == 4)
                {
                    bot4Chips += int.Parse(tbPot.Text);
                    tbChips.Text = bot4Chips.ToString();
                    fourthBotPanel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }

                if (index == 5)
                {
                    bot5Chips += int.Parse(tbPot.Text);
                    tbChips.Text = bot5Chips.ToString();
                    fifthBotPlayer.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }

                for (int j = 0; j <= 16; j++)
                {
                    Holder[j].Visible = false;
                }

                await Finish(1);
            }

            intsadded = false;
            #endregion

            #region FiveOrLessLeft
            if (abc < 6 && abc > 1 && rounds >= End)
            {
                await Finish(2);
            }

            #endregion


        }

        async Task Finish(int n)
        {
	        if (n == 2)
	        {
		        FixWinners();
	        }

	        playerPanel.Visible = false;
	        firstBotPanel.Visible = false;
	        secondBotPanel.Visible = false;
	        thirdBotPanel.Visible = false;
	        fourthBotPanel.Visible = false;
	        fifthBotPlayer.Visible = false;
	        call = bb;
	        Raise = 0;
	        foldedPlayers = 5;
	        type = 0;
	        rounds = 0;
	        b1Power = 0;
	        b2Power = 0;
	        b3Power = 0;
	        b4Power = 0;
	        b5Power = 0;
	        pPower = 0;
	        pType = -1;
	        Raise = 0;
	        b1Type = -1;
	        b2Type = -1;
	        b3Type = -1;
	        b4Type = -1;
	        b5Type = -1;
	        B1turn = false;
	        B2turn = false;
	        B3turn = false;
	        B4turn = false;
	        B5turn = false;
	        B1Fturn = false;
	        B2Fturn = false;
	        B3Fturn = false;
	        B4Fturn = false;
	        B5Fturn = false;
	        pFolded = false;
	        b1Folded = false;
	        b2Folded = false;
	        b3Folded = false;
	        b4Folded = false;
	        b5Folded = false;
	        PFturn = false;
	        Pturn = true;
	        restart = false;
	        raising = false;
	        pCall = 0;
	        b1Call = 0;
	        b2Call = 0;
	        b3Call = 0;
	        b4Call = 0;
	        b5Call = 0;
	        pRaise = 0;
	        b1Raise = 0;
	        b2Raise = 0;
	        b3Raise = 0;
	        b4Raise = 0;
	        b5Raise = 0;
	        height = 0;
	        width = 0;
	        winners = 0;
	        Flop = 1;
	        Turn = 2;
	        River = 3;
	        End = 4;
	        maxLeft = 6;
	        last = 123;
	        raisedTurn = 1;
	        bools.Clear();
	        CheckWinners.Clear();
	        ints.Clear();
	        Win.Clear();
	        sorted.Current = 0;
	        sorted.Power = 0;
	        tbPot.Text = "0";
	        t = 60;
	        up = 10000000;
	        turnCount = 0;
	        pStatus.Text = string.Empty;
	        b1Status.Text = string.Empty;
	        b2Status.Text = string.Empty;
	        b3Status.Text = string.Empty;
	        b4Status.Text = string.Empty;
	        b5Status.Text = string.Empty;
	        if (chips <= 0)
	        {
		        AddChips f2 = new AddChips();
		        f2.ShowDialog();
		        if (f2.a != 0)
		        {
			        chips = f2.a;
			        bot1Chips += f2.a;
			        bot2Chips += f2.a;
			        bot3Chips += f2.a;
			        bot4Chips += f2.a;
			        bot5Chips += f2.a;
			        PFturn = false;
			        Pturn = true;
			        bRaise.Enabled = true;
			        bFold.Enabled = true;
			        bCheck.Enabled = true;
			        bRaise.Text = "Raise";
		        }
	        }

	        ImgLocation = Directory.GetFiles(AssetsPath + @"Cards", "*.png", SearchOption.TopDirectoryOnly);
	        for (int os = 0; os < 17; os++)
	        {
		        Holder[os].Image = null;
		        Holder[os].Invalidate();
		        Holder[os].Visible = false;
	        }

	        await Shuffle();

	        // await Turns();
        }

		void FixWinners()
        {
            Win.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
            string fixedLast = "qwerty";
            if (!pStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                Rules(0, 1, "Player", ref pType, ref pPower, PFturn);
            }

            if (!b1Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                Rules(2, 3, "Bot 1", ref b1Type, ref b1Power, B1Fturn);
            }

            if (!b2Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                Rules(4, 5, "Bot 2", ref b2Type, ref b2Power, B2Fturn);
            }

            if (!b3Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                Rules(6, 7, "Bot 3", ref b3Type, ref b3Power, B3Fturn);
            }

            if (!b4Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                Rules(8, 9, "Bot 4", ref b4Type, ref b4Power, B4Fturn);
            }

            if (!b5Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                Rules(10, 11, "Bot 5", ref b5Type, ref b5Power, B5Fturn);
            }

            Winner(pType, pPower, "Player", chips, fixedLast);
            Winner(b1Type, b1Power, "Bot 1", bot1Chips, fixedLast);
            Winner(b2Type, b2Power, "Bot 2", bot2Chips, fixedLast);
            Winner(b3Type, b3Power, "Bot 3", bot3Chips, fixedLast);
            Winner(b4Type, b4Power, "Bot 4", bot4Chips, fixedLast);
            Winner(b5Type, b5Power, "Bot 5", bot5Chips, fixedLast);
        }

        void AI(int c1, int c2, ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, double botCurrent)
        {
            if (!sFTurn)
            {
                if (botCurrent == -1)
                {
                    HighCard(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }

                if (botCurrent == 0)
                {
                    PairTable(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }

                if (botCurrent == 1)
                {
                    PairHand(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }

                if (botCurrent == 2)
                {
                    TwoPair(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }

                if (botCurrent == 3)
                {
                    ThreeOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }

                if (botCurrent == 4)
                {
                    Straight(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }

                if (botCurrent == 5 || botCurrent == 5.5)
                {
                    Flush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }

                if (botCurrent == 6)
                {
                    FullHouse(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }

                if (botCurrent == 7)
                {
                    FourOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }

                if (botCurrent == 8 || botCurrent == 9)
                {
                    StraightFlush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
            }

            if (sFTurn)
            {
                Holder[c1].Visible = false;
                Holder[c2].Visible = false;
            }
        }

        private void HighCard(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            HP(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 20, 25);
        }

        private void PairTable(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            HP(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 16, 25);
        }

        private void PairHand(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);
            if (botPower <= 199 && botPower >= 140)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 6, rRaise);
            }

            if (botPower <= 139 && botPower >= 128)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 7, rRaise);
            }

            if (botPower < 128 && botPower >= 101)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 9, rRaise);
            }
        }

        private void TwoPair(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);
            if (botPower <= 290 && botPower >= 246)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 3, rRaise);
            }

            if (botPower <= 244 && botPower >= 234)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 4, rRaise);
            }

            if (botPower < 234 && botPower >= 201)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 4, rRaise);
            }
        }

        private void ThreeOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
            if (botPower <= 390 && botPower >= 330)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }

            if (botPower <= 327 && botPower >= 321)
            {
	            // 10  8
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }

            if (botPower < 321 && botPower >= 303)
            {
	            // 7 2
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }
        }

        private void Straight(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
            if (botPower <= 480 && botPower >= 410)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }

            if (botPower <= 409 && botPower >= 407)
            {
	            // 10  8
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }

            if (botPower < 407 && botPower >= 404)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }
        }

        private void Flush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fCall, fRaise);
        }

        private void FullHouse(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);
            if (botPower <= 626 && botPower >= 620)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise);
            }

            if (botPower < 620 && botPower >= 602)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise);
            }
        }

        private void FourOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (botPower <= 752 && botPower >= 704)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fkCall, fkRaise);
            }
        }

        private void StraightFlush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sfCall, sfRaise);
            }
        }

        private void Fold(ref bool sTurn, ref bool sFTurn, Label sStatus)
        {
            raising = false;
            sStatus.Text = "Fold";
            sTurn = false;
            sFTurn = true;
        }

        private void Check(ref bool cTurn, Label cStatus)
        {
            cStatus.Text = "Check";
            cTurn = false;
            raising = false;
        }

        private void Call(ref int sChips, ref bool sTurn, Label sStatus)
        {
            raising = false;
            sTurn = false;
            sChips -= call;
            sStatus.Text = "Call " + call;
            tbPot.Text = (int.Parse(tbPot.Text) + call).ToString();
        }

        private void Raised(ref int sChips, ref bool sTurn, Label sStatus)
        {
            sChips -= Convert.ToInt32(Raise);
            sStatus.Text = "Raise " + Raise;
            tbPot.Text = (int.Parse(tbPot.Text) + Convert.ToInt32(Raise)).ToString();
            call = Convert.ToInt32(Raise);
            raising = true;
            sTurn = false;
        }

        private static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }

        private void HP(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, int n, int n1)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (call <= 0)
            {
                Check(ref sTurn, sStatus);
            }

            if (call > 0)
            {
                if (rnd == 1)
                {
                    if (call <= RoundN(sChips, n))
                    {
                        Call(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }

                if (rnd == 2)
                {
                    if (call <= RoundN(sChips, n1))
                    {
                        Call(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
            }

            if (rnd == 3)
            {
                if (Raise == 0)
                {
                    Raise = call * 2;
                    Raised(ref sChips, ref sTurn, sStatus);
                }
                else
                {
                    if (Raise <= RoundN(sChips, n))
                    {
                        Raise = call * 2;
                        Raised(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
            }

            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }

        private void PH(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int n, int n1, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (rounds < 2)
            {
                if (call <= 0)
                {
                    Check(ref sTurn, sStatus);
                }

                if (call > 0)
                {
                    if (call >= RoundN(sChips, n1))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (Raise > RoundN(sChips, n))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (!sFTurn)
                    {
                        if (call >= RoundN(sChips, n) && call <= RoundN(sChips, n1))
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }

                        if (Raise <= RoundN(sChips, n) && Raise >= RoundN(sChips, n) / 2)
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }

                        if (Raise <= RoundN(sChips, n) / 2)
                        {
                            if (Raise > 0)
                            {
                                Raise = RoundN(sChips, n);
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                Raise = call * 2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }

                    }
                }
            }

            if (rounds >= 2)
            {
                if (call > 0)
                {
                    if (call >= RoundN(sChips, n1 - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (Raise > RoundN(sChips, n - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (!sFTurn)
                    {
                        if (call >= RoundN(sChips, n - rnd) && call <= RoundN(sChips, n1 - rnd))
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }

                        if (Raise <= RoundN(sChips, n - rnd) && Raise >= RoundN(sChips, n - rnd) / 2)
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }

                        if (Raise <= RoundN(sChips, n - rnd) / 2)
                        {
                            if (Raise > 0)
                            {
                                Raise = RoundN(sChips, n - rnd);
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                Raise = call * 2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }
                    }
                }

                if (call <= 0)
                {
                    Raise = RoundN(sChips, r - rnd);
                    Raised(ref sChips, ref sTurn, sStatus);
                }
            }

            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }

        void Smooth(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, int n, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (call <= 0)
            {
                Check(ref botTurn, botStatus);
            }
            else
            {
                if (call >= RoundN(botChips, n))
                {
                    if (botChips > call)
                    {
                        Call(ref botChips, ref botTurn, botStatus);
                    }
                    else if (botChips <= call)
                    {
                        raising = false;
                        botTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        tbPot.Text = (int.Parse(tbPot.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (Raise > 0)
                    {
                        if (botChips >= Raise * 2)
                        {
                            Raise *= 2;
                            Raised(ref botChips, ref botTurn, botStatus);
                        }
                        else
                        {
                            Call(ref botChips, ref botTurn, botStatus);
                        }
                    }
                    else
                    {
                        Raise = call * 2;
                        Raised(ref botChips, ref botTurn, botStatus);
                    }
                }
            }

            if (botChips <= 0)
            {
                botFTurn = true;
            }
        }

        #region UI
        private async void timer_Tick(object sender, object e)
        {
            if (pbTimer.Value <= 0)
            {
                PFturn = true;
                await Turns();
            }

            if (t > 0)
            {
                t--;
                pbTimer.Value = (t / 6) * 100;
            }
        }

        private void Update_Tick(object sender, object e)
        {
            if (chips <= 0)
            {
                tbChips.Text = "chips : 0";
            }

            if (bot1Chips <= 0)
            {
                tbBotChips1.Text = "chips : 0";
            }

            if (bot2Chips <= 0)
            {
                tbBotChips2.Text = "chips : 0";
            }

            if (bot3Chips <= 0)
            {
                tbBotChips3.Text = "chips : 0";
            }

            if (bot4Chips <= 0)
            {
                tbBotChips4.Text = "chips : 0";
            }

            if (bot5Chips <= 0)
            {
                tbBotChips5.Text = "chips : 0";
            }

            tbChips.Text = "chips : " + this.chips;
            tbBotChips1.Text = "chips : " + this.bot1Chips;
            tbBotChips2.Text = "chips : " + this.bot2Chips;
            tbBotChips3.Text = "chips : " + this.bot3Chips;
            tbBotChips4.Text = "chips : " + this.bot4Chips;
            tbBotChips5.Text = "chips : " + this.bot5Chips;
            if (chips <= 0)
            {
                Pturn = false;
                PFturn = true;
                bCall.Enabled = false;
                bRaise.Enabled = false;
                bFold.Enabled = false;
                bCheck.Enabled = false;
            }

            if (up > 0)
            {
                up--;
            }

            if (chips >= call)
            {
                bCall.Text = "Call " + this.call;
            }
            else
            {
                bCall.Text = "All in";
                bRaise.Enabled = false;
            }

            if (call > 0)
            {
                bCheck.Enabled = false;
            }

            if (call <= 0)
            {
                bCheck.Enabled = true;
                bCall.Text = "Call";
                bCall.Enabled = false;
            }

            if (chips <= 0)
            {
                bRaise.Enabled = false;
            }

            int parsedValue;

            if (tbRaise.Text != string.Empty && int.TryParse(tbRaise.Text, out parsedValue))
            {
                if (chips <= int.Parse(tbRaise.Text))
                {
                    bRaise.Text = "All in";
                }
                else
                {
                    bRaise.Text = "Raise";
                }
            }

            if (chips < call)
            {
                bRaise.Enabled = false;
            }
        }

        private async void bFold_Click(object sender, EventArgs e)
        {
            pStatus.Text = "Fold";
            Pturn = false;
            PFturn = true;
            await Turns();
        }

        private async void bCheck_Click(object sender, EventArgs e)
        {
            if (call <= 0)
            {
                Pturn = false;
                pStatus.Text = "Check";
            }
            else
            {
                // pStatus.Text = "All in " + chips;
                bCheck.Enabled = false;
            }

            await Turns();
        }

        private async void bCall_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref pType, ref pPower, PFturn);
            if (chips >= call)
            {
                chips -= call;
                tbChips.Text = "chips : " + this.chips;
                if (tbPot.Text != string.Empty)
                {
                    tbPot.Text = (int.Parse(tbPot.Text) + call).ToString();
                }
                else
                {
                    tbPot.Text = call.ToString();
                }

                Pturn = false;
                pStatus.Text = "Call " + call;
                pCall = call;
            }
            else if (chips <= call && call > 0)
            {
                tbPot.Text = (int.Parse(tbPot.Text) + chips).ToString();
                pStatus.Text = "All in " + chips;
                chips = 0;
                tbChips.Text = "chips : " + this.chips;
                Pturn = false;
                bFold.Enabled = false;
                pCall = chips;
            }

            await Turns();
        }

        private async void bRaise_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref pType, ref pPower, PFturn);
            int parsedValue;
            if (tbRaise.Text != string.Empty && int.TryParse(tbRaise.Text, out parsedValue))
            {
                if (chips > call)
                {
	                if (Raise * 2 > int.Parse(tbRaise.Text))
                    {
                        tbRaise.Text = (Raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }

	                if (this.chips >= int.Parse(this.tbRaise.Text))
	                {
		                this.call = int.Parse(this.tbRaise.Text);
		                this.Raise = int.Parse(this.tbRaise.Text);
		                this.pStatus.Text = "Raise " + this.call;
		                this.tbPot.Text = (int.Parse(this.tbPot.Text) + this.call).ToString();
		                this.bCall.Text = "Call";
		                this.chips -= int.Parse(this.tbRaise.Text);
		                this.raising = true;
		                this.last = 0;
		                this.pRaise = Convert.ToInt32(this.Raise);
	                }
	                else
	                {
		                this.call = this.chips;
		                this.Raise = this.chips;
		                this.tbPot.Text = (int.Parse(this.tbPot.Text) + this.chips).ToString();
		                this.pStatus.Text = "Raise " + this.call;
		                this.chips = 0;
		                this.raising = true;
		                this.last = 0;
		                this.pRaise = Convert.ToInt32(this.Raise);
	                }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }

            Pturn = false;
            await Turns();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (tbAdd.Text == string.Empty) { }
            else
            {
                chips += int.Parse(tbAdd.Text);
                bot1Chips += int.Parse(tbAdd.Text);
                bot2Chips += int.Parse(tbAdd.Text);
                bot3Chips += int.Parse(tbAdd.Text);
                bot4Chips += int.Parse(tbAdd.Text);
                bot5Chips += int.Parse(tbAdd.Text);
            }

            tbChips.Text = "chips : " + this.chips;
        }

        private void bOptions_Click(object sender, EventArgs e)
        {
            tbBB.Text = bb.ToString();
            tbSB.Text = sb.ToString();
            if (tbBB.Visible == false)
            {
                tbBB.Visible = true;
                tbSB.Visible = true;
                bBB.Visible = true;
                bSB.Visible = true;
            }
            else
            {
                tbBB.Visible = false;
                tbSB.Visible = false;
                bBB.Visible = false;
                bSB.Visible = false;
            }
        }

        private void bSB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (tbSB.Text.Contains(",") || tbSB.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                tbSB.Text = sb.ToString();
                return;
            }

            if (!int.TryParse(tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                tbSB.Text = sb.ToString();
                return;
            }

            if (int.Parse(tbSB.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                tbSB.Text = sb.ToString();
            }

            if (int.Parse(tbSB.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }

            if (int.Parse(tbSB.Text) >= 250 && int.Parse(tbSB.Text) <= 100000)
            {
                sb = int.Parse(tbSB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void bBB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (tbBB.Text.Contains(",") || tbBB.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                tbBB.Text = bb.ToString();
                return;
            }

            if (!int.TryParse(tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                tbSB.Text = bb.ToString();
                return;
            }

            if (int.Parse(tbBB.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                tbBB.Text = bb.ToString();
            }

            if (int.Parse(tbBB.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }

            if (int.Parse(tbBB.Text) >= 500 && int.Parse(tbBB.Text) <= 200000)
            {
                bb = int.Parse(tbBB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            width = this.Width;
            height = this.Height;
        }

        #endregion

        private void PokerForm_Load(object sender, EventArgs e)
        {

        }
    }
}