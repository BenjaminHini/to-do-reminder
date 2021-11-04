using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace assign6
{
    /// <summary>
    /// static class used to create the connection with files
    /// in order to write in them (save) or to read them (load)
    /// it has a string as attribute that is insered at the beginning of the files 
    /// it writes in in order to differenciate them from other .txt files
    /// </summary>
    public static class FileManager
    {

        private const string check = "klklklkk";
        /// <summary>
        /// open a file in order to write the data from the list parameter in it
        /// </summary>
        /// <param name="list">data to be written in the file</param>
        /// <param name="filename">name of the file to be open</param>
        /// <returns></returns>
        public static bool save(List<Task> list, string filename)
        {
            bool done = true;
            StreamWriter writer = null;

            try
            {
                writer = new StreamWriter(filename);
                writer.WriteLine(check);
                writer.WriteLine(list.Count);
                foreach(Task var in list)
                {
                    writer.WriteLine(var.Prio);
                    writer.WriteLine(var.Todo);
                    writer.WriteLine(var.Date.ToString("ddd dd MMMM yyyy hh:mm"));
                }

            }
            catch { done = false; }

            if(writer != null)
                writer.Close();

            return done;
        }
        /// <summary>
        /// open a file in order to store the data it contains in the list parameter
        /// </summary>
        /// <param name="list">gets the data from the file</param>
        /// <param name="filename">name of the file to be open</param>
        /// <returns></returns>
        public static bool load(out List<Task> list, string filename)
        {
            list = new List<Task>();
            bool done = true;
            StreamReader reader = null;
            int nb = 0;
            try
            {
                reader = new StreamReader(filename);
                if (reader.ReadLine() != check)//check whether the file is linked to this application
                    throw new Exception("selected file does'nt fit");
                Int32.TryParse(reader.ReadLine(), out nb);
                for(int i =0; i<nb; ++i)
                {
                    PriorityType prio = (PriorityType)Enum.Parse(typeof(PriorityType), reader.ReadLine());
                    string todo = reader.ReadLine();
                    string strdate = reader.ReadLine();
                    DateTime date = DateTime.Parse(strdate);
                    list.Add(new Task(todo, prio, date));
                }
            }
            catch { done = false; }

            if(reader!=null)
                reader.Close();
            
            return done;
        }
    }
}
