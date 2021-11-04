using System;
using System.Collections.Generic;
using System.Text;

namespace assign6
{
    public class TaskManager
    {
        private List<Task> tasks;
        
        /// <summary>
        /// default constructor of the class
        /// </summary>
        public TaskManager(){ tasks = new List<Task>(); }

        /// <summary>
        /// return the Task stored at the selected index
        /// </summary>
        /// <param name="index">index of the Task to be returned</param>
        /// <returns></returns>
        public Task getTask(int index){ return tasks.ToArray()[index]; }

        /// <summary>
        /// add a new Task to the list
        /// </summary>
        /// <param name="task">Task to be added</param>
        public void add(Task task) { tasks.Add(task); }

        /// <summary>
        /// remove the Task stored at the selected index
        /// </summary>
        /// <param name="index">index of the Task to be removed</param>
        public void remove(int index) { tasks.RemoveAt(index); }

        /// <summary>
        /// replace the Task of the selected index by the one send in parameter
        /// </summary>
        /// <param name="index">index of the Task to be removed</param>
        /// <param name="task">Task replacing it</param>
        public void change(int index, Task task)
        {
            tasks.RemoveAt(index);
            tasks.Add(task);
        }

        /// <summary>
        /// return the number of elements in the list
        /// </summary>
        /// <returns></returns>
        public int getSize() { return tasks.Count; }

        /// <summary>
        /// calls Filemanager.load() to receive the data from the selected file.
        /// sort it in order to keep only the future appointment
        /// return false if ann issue occured in Filemanager.load()
        /// </summary>
        /// <param name="file">name of the file we work with</param>
        /// <returns></returns>
        public bool openFile(string file)
        {
           if(FileManager.load(out tasks, file))
           {
                List<Task> toberemoved = new List<Task>();
                foreach (Task ele in tasks)
                    if (ele.Date < DateTime.Now)
                        toberemoved.Add(ele);
                foreach (Task ele in toberemoved)
                    tasks.Remove(ele);
                return true;
           }
            return false;
        }

        /// <summary>
        /// calls FileManager.save() to store the current data
        /// in the selected file.
        /// return whether an issue occured in FileManager.save()
        /// </summary>
        /// <param name="File">name of the file we work with</param>
        /// <returns></returns>
        public bool saveFile(string File)
        {
            return FileManager.save(tasks, File);
        }
    }
}
