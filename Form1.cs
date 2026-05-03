using System;
using System.Drawing;
using System.Windows.Forms;

namespace Task_Manager
{
    public partial class Form1 : Form
    {
        string ForkMyFuture = "Fork My Future";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTaskDate.Text) || string.IsNullOrEmpty(txtTaskName.Text))
            {
                MessageBox.Show("Title or Date is Empty Please Fill Them First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ListViewItem item = new ListViewItem(txtTaskName.Text.Trim());
            item.SubItems.Add(txtTaskDate.Text.Trim());

            item.ImageIndex = 1;

            if (cbImportant.Checked == true)
            {
                item.ForeColor = Color.Red;

                item.ImageIndex = 0;
                
            }
            item.Tag = 0;

            listView1.Items.Add(item);
            
            dateTimePicker1.Value = DateTime.Now;
            cbImportant.Checked = false;
            txtTaskName.Clear();
            txtTaskDate.Clear();
            txtTaskName.Focus();

            Notification();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            txtTaskDate.Text = dateTimePicker1.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
      
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
            
                if (item.Checked == true)
                {
                    
                    item.Remove();
                }
            }
            
        }
        
        private void listView1_MouseUp(object sender, MouseEventArgs e) // تخصيص  contex menue  للايتمز
        {
            if(e.Button == MouseButtons.Right)
            {
                ListViewItem item = listView1.GetItemAt(e.X, e.Y);
                if (item != null) //  تتاكد اذا الماوس علس ايتم مش مكان فرغ
                {

                    contextMenuStrip1.Show(listView1, e.Location);
                    if (item.Tag.ToString() != "1")
                    {
                        finishedToolStripMenuItem.Enabled = true; // اغلاق خيار ال finished  اذا كان مختار بافعل من قبل
                    }
                    else
                    {
                        finishedToolStripMenuItem.Enabled = false;
                    }

                }

            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                listView1.Items.Remove(item);
            }
        }



        private void finishedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {

                ((ToolStripMenuItem)sender).Enabled = true;
                
                    item.ForeColor = Color.RosyBrown;
                    item.Text += "   (Finished)✔";
                    item.BackColor = Color.Pink;
                    item.Tag = 1;

                

            }
        }

        private void titleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.SelectedItems[0].BeginEdit();

            }
            else
            {
                MessageBox.Show("Please Select Item!");
            }


        }

        private void listView1_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e) // حدث يتم بعد انتهاء التعديل
        {
            if (e.Label == null || e.Label.Trim() == "") // اذا كان التايتل فارغ نلغي التعديل
            {
                MessageBox.Show("You Cant Let It Empty!");

                e.CancelEdit = true;
            }
           
        }

        private void dateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0].Tag.ToString() != "1") 
            {

                listView1.SelectedItems[0].SubItems[1].Text = dateTimePicker1.Text;
            }
        }

        private void yesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0].Tag.ToString() != "1")
            {
                listView1.SelectedItems[0].ForeColor = Color.Red;
                listView1.SelectedItems[0].ImageIndex = 0;
            }
        }

        private void noToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0].Tag.ToString() != "1")
            {
                listView1.SelectedItems[0].ForeColor = Color.Black;
                listView1.SelectedItems[0].ImageIndex = 1;
            }
        }

        private void unFinishedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0].Tag.ToString() == "1")
            {
                listView1.SelectedItems[0].Tag = 0;
                listView1.SelectedItems[0].ForeColor = Color.Black;
                listView1.SelectedItems[0].BackColor= Color.Transparent;

                string S = listView1.SelectedItems[0].Text;
                listView1.SelectedItems[0].Text = S.Remove(S.Length - 14, 14).Trim(); // (Finished)✔ حذف كلمة 
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem item = listView1.SelectedItems[0];
            string txt = item.Text;
            string Date = item.SubItems[1].Text;
            string Importancy = item.ForeColor != Color.Red ? "Normal" : "Important";
            MessageBox.Show($"Title: {txt}\n" + $"Date:{Date}\n" + $"Importancy: {Importancy}\n", "Properties", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
        }

        private void Notification()
        {
            notifyIcon1.Icon = SystemIcons.Application;

            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "Task Info!";
            notifyIcon1.BalloonTipText = "New task has added successfully ✔😉";

            notifyIcon1.ShowBalloonTip(2000);


        }
    }
   
}
