using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class MainForm : Form
    {
        Logic Game = new Logic();
        int FirstInstance = 0;

        public MainForm()
        {
            InitializeComponent();
        }
        private void TimeEventProcessor(object sender, EventArgs e) //Event for one Timer tick
        {
            Game.GameEvent(pictureBox1.Width, pictureBox1.Height);
            lblScore.Text = Game.Score.ToString();
            lblLevel.Text = Game.Level.ToString();
            timer1.Interval =150-Game.Speed;    //Game speed
            if (Game.GameOver==true)    //Ending instructions
            {
                timer1.Stop();
                MessageBox.Show("Game Over! Your score: "+Game.Score.ToString(), "INFO", MessageBoxButtons.OK);
                newGameToolStripMenuItem.Enabled = true;
            }
        }
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e) //New Game click
        {
            Graphics Target= pictureBox1.CreateGraphics();
            newGameToolStripMenuItem.Enabled = false;
            lblScore.Text = "0";
            lblLevel.Text = "1";
            Game.Target = Target;
            Game.NewGame();
            timer1.Interval = 150;
            if(FirstInstance==0)    //Only one EventHandler Adding 
            {
                timer1.Tick += new EventHandler(TimeEventProcessor);
                FirstInstance = 1;
            }
            timer1.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)    //Exit Button
        {
            Application.Exit();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)    //Need to override btn id because not all keys are visible
        {
            switch(keyData)
            {
                case Keys.Up:
                    Game.GameControls(4);
                    return true;
                case Keys.Down:
                    Game.GameControls(3);
                    return true;
                case Keys.Right:
                    Game.GameControls(1);
                    return true;
                case Keys.Left:
                    Game.GameControls(2);
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
