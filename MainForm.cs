using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assign6
{
    public partial class MainForm : Form
    {
        private TaskManager manager = new TaskManager();
        private Task curr = new Task();
        
        /// <summary>
        /// constructor of the class, gets called by the main 
        /// initialize the form components, calls updateGUI() to set the tooltip and start the timer
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            prio_cmb.DataSource = Enum.GetValues(typeof(PriorityType));
            prio_cmb.SelectedIndex = -1;
            updateGUI();
        }
       
        /// <summary>
        /// reinitialize the class attributes and calls 
        /// updateGUI() to show an empty of data one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manager = new TaskManager();
            curr = new Task();
            updateGUI();
        }
       
        /// <summary>
        /// store the data from the "todo" textbox in the current task
        /// enable the Add button if CheckData() is validated = if the user entered all
        /// the required data to create a new task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void todo_txt_TextChanged(object sender, EventArgs e)
        {
            curr.Todo = todo_txt.Text;
            add_btn.Enabled = curr.CheckData();
        }
        
        /// <summary>
        /// store the data from the "priority" combobox in the current task
        /// enable the Add button if CheckData() is validated = if the user entered all
        /// the required data to create a new task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void prio_cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            curr.Prio = (PriorityType)prio_cmb.SelectedIndex;
            add_btn.Enabled = curr.CheckData();
        }
        
        /// <summary>
        /// store the data from the datetimepicker in the current task
        /// enable the Add button if CheckData() is validated = if the user entered all
        /// the required data to create a new task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            curr.Date = dateTimePicker1.Value;
            add_btn.Enabled = curr.CheckData();
        }
       
        /// <summary>
        /// enabled by CheckData(), add the current task to the taskmanager
        /// and show it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_btn_Click(object sender, EventArgs e)
        {
            manager.add(curr);
            curr = new Task();
            updateGUI();
        }
       
        /// <summary>
        /// set the tooltip, start the timer and fill the listbox
        /// with data from manager
        /// </summary>
        public void updateGUI()
        {
            toolTip1.SetToolTip(dateTimePicker1, "Click to open calendar for date, write in time here");
            timer1.Start();
            todo_list.Items.Clear();
            for (int i = 0; i < manager.getSize(); ++i)
                todo_list.Items.Add(manager.getTask(i).ToString());

            prio_cmb.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            todo_txt.Text = string.Empty;
        }
       
        /// <summary>
        /// ask the user to confirm he wants to exit 
        /// and close the application if he does
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Are you sure ", "exit program", MessageBoxButtons.YesNo);
            if(dlg == DialogResult.Yes)
                Application.Exit();
        }
        
        /// <summary>
        /// ask the user to choose the file from wich he wants to get data
        /// calls manager.openFile() to do so
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open File From";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                if (manager.openFile(openFileDialog1.FileName))
                    updateGUI();
                else
                    MessageBox.Show("an error has occured");

        }
        
        /// <summary>
        /// ask the user to choose the file in which he wants to save data
        /// calls manager.saveFile() to do so
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = string.Empty;
            saveFileDialog1.Title = "Save As ";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                if (manager.saveFile(saveFileDialog1.FileName))
                    MessageBox.Show("done");
                else
                    MessageBox.Show("an error has occured");
        }
       
        /// <summary>
        ///update the label linked to the timer every second 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer_lbl.Text = DateTime.Now.ToString("hh:mm:ss");
        }
        
        /// <summary>
        /// delete the Task selected form the user in the listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int i = todo_list.SelectedIndex;
                manager.remove(i);
                updateGUI();
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        
        /// <summary>
        /// change the task selected by the user in the listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void change_btn_Click(object sender, EventArgs e)
        {
            try
            {
                curr = new Task(todo_txt.Text, (PriorityType)prio_cmb.SelectedItem, dateTimePicker1.Value);
                int index = todo_list.SelectedIndex;
                manager.change(index, curr);
                curr = new Task();
                updateGUI();
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        /// <summary>
        /// show the data from the Task currently selected by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void todo_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                curr = manager.getTask(todo_list.SelectedIndex);
                dateTimePicker1.Value = curr.Date;
                todo_txt.Text = curr.Todo;
                prio_cmb.SelectedIndex = (int)curr.Prio;
                curr = new Task();
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        /// <summary>
        /// open the AboutBox form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AboutBox();
            form.ShowDialog();
        }
    }
}
