using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace soundPlayer
{
    public partial class Form1 : Form
    {
        int index;
        SoundPlayer player = null;
        String fileName = "";
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        string[] musicList;
        int duration = 0;
        


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Enabled = false;
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
                if (fileName == "" || fileName == Directory.GetCurrentDirectory() + "/dr_Zoidberg.mp3")
                {
                    fileName = Directory.GetCurrentDirectory() + "/dr_Zoidberg.mp3";
                    playMusic(fileName);
                }
                else
                {
                    index = listBox1.SelectedIndex;
                    fileName = listBox1.SelectedItem.ToString();
                    playMusic(index);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            wplayer.controls.pause();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string path = SelectFolder();
            musicList = GetMusicList(path);
            foreach (string music in musicList)
            {
                listBox1.Items.Add(music);
            }
        }

        private string SelectFolder()
        {
            FolderBrowserDialog DirDialog = new FolderBrowserDialog();
            DirDialog.Description = "Выбор директории";
            DirDialog.SelectedPath = @"C:\";

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
            index += 1;
            playMusic(index);
            listBox1.SetSelected(index, true);
        }

        bool DurationReady = false;
        private void playMusic(int id)
        {
            TagLib.File f = TagLib.File.Create(listBox1.Items[id].ToString(), TagLib.ReadStyle.Average);
            var duration = (int)f.Properties.Duration.TotalSeconds;
            wplayer.URL = textBox1.Text = listBox1.Items[id].ToString();
            wplayer.controls.play();
            //wplayer.controls.currentPosition;
        }

        private void playMusic(string path)
        {
            TagLib.File f = TagLib.File.Create(path, TagLib.ReadStyle.Average);
            var duration = (int)f.Properties.Duration.TotalSeconds;
            wplayer.URL = textBox1.Text = path;
            wplayer.controls.play();
            //wplayer.controls.currentPosition;
        }

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
                trackBar1.Enabled = true;
                trackBar1.Maximum = duration;
                DurationReady = true;
            }
            trackBar1.Value = Convert.ToInt32(wplayer.controls.currentPosition);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
