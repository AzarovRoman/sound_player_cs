using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace soundPlayer
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        int index;
        SoundPlayer player = null;
        String fileName = "";
        string MainPath;
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        string[] musicList;
        int duration = 0;
        double CurrentPos = 0;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Enabled = true;
            trackBar1.TickStyle = TickStyle.Both;
            player = new SoundPlayer();
            timer1.Enabled = true;
            timer1.Interval = 100;
            wplayer.URL = Directory.GetCurrentDirectory() + "dr_Zoidberg.mp3";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentPos == 0)
                {
                    index = listBox1.SelectedIndex;
                    if (index == -1)
                    {
                        index = 0;
                    }

                    fileName = MainPath + "\\" + listBox1.Items[index].ToString();
                    playMusic(index);

                }
                else
                {
                    index = listBox1.SelectedIndex;
                    if (index == -1)
                    {
                        index = 0;
                    }

                    fileName = MainPath + "\\" + listBox1.Items[index].ToString();
                    playMusic(index);

                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Похоже, вы ничего не выбрали :(", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Похоже, вы ничего не выбрали :(", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            wplayer.controls.pause();
            CurrentPos = wplayer.controls.currentPosition;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MainPath = SelectFolder();
            musicList = GetMusicList(MainPath);
            foreach (string music in musicList)
            {
                listBox1.Items.Add(getMusicName(music));
            }
        }

        private string SelectFolder()
        {
            FolderBrowserDialog DirDialog = new FolderBrowserDialog();
            DirDialog.Description = "Выбор директории";
            DirDialog.SelectedPath = @"C:\";

            listBox1.Items.Clear();

            if (DirDialog.ShowDialog() == DialogResult.OK)
            {
                return DirDialog.SelectedPath;
            }
            return DirDialog.SelectedPath;
        }

        private string[] GetMusicList(string path)
        {
            
            string[] allPath = Directory.GetFiles(path, "*.mp3");
            return allPath;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            NextTrek();
        }

        bool DurationReady = false;
        private void playMusic(int id)
        {
            int dur = Convert.ToInt32(wplayer.currentMedia.duration);
            wplayer.URL = MainPath + '\\' +listBox1.Items[id].ToString();
            textBox1.Text = listBox1.Items[id].ToString();
            wplayer.controls.currentPosition = CurrentPos;
            wplayer.controls.play();
        }

        /*private void playMusic(string path)
        {
            int dur = Convert.ToInt32(wplayer.currentMedia.duration);
            wplayer.URL = textBox1.Text = path;
            wplayer.controls.play();
        }*/

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            wplayer.controls.currentPosition = trackBar1.Value;
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            duration = Convert.ToInt32(wplayer.currentMedia.duration);
            if (wplayer.currentMedia.duration > 0 && DurationReady == false)
            {
                trackBar1.Maximum = duration;
                DurationReady = true;
            }

            try
            {
                trackBar1.Value = Convert.ToInt32(wplayer.controls.currentPosition);
                if (trackBar1.Value == trackBar1.Maximum)
                {
                    NextTrek();
                }
                DurationReady = false;
            }
            catch (ArgumentOutOfRangeException)
            {
                trackBar1.Value = 0;
                DurationReady = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private string getMusicName(string path)
        {
            int ind = path.LastIndexOf('\\');
            Console.WriteLine(ind);
            string res = path.Substring(ind+1);
            return res;
        }

        private void button3_Click(object sender, EventArgs e)//shuffle
        {
            int MusicListCount = listBox1.Items.Count;
            List<string> ShuffledList = new List<string>();

            foreach (String strCol in listBox1.Items)
            {
                ShuffledList.Add(strCol);
            }

            Random rand = new Random();

            for (int i = ShuffledList.Count - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                string tmp = ShuffledList[j];
                ShuffledList[j] = ShuffledList[i];
                ShuffledList[i] = tmp;
            }

            listBox1.Items.Clear();

            foreach (String str in ShuffledList)
            {
                listBox1.Items.Add(str);
            }
        }

        private void NextTrek()
        {
            if (fileName != "")
            {
                try
                {
                    CurrentPos = 0;
                    index += 1;
                    playMusic(index);
                    listBox1.SetSelected(index, true);
                }
                catch (Exception)
                {
                    CurrentPos = 0;
                    index = 0;
                    playMusic(index);
                    listBox1.SetSelected(index, true);
                }
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            int Volume = trackBar2.Value;
            wplayer.settings.volume = Volume;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            wplayer.controls.currentPosition = trackBar1.Value;
        }
    }
}
