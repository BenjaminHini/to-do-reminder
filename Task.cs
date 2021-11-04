using System;
using System.Collections.Generic;
using System.Text;

namespace assign6
{
    public class Task
    {
        private string todo;
        private PriorityType prio;
        private DateTime date;

        /// <summary>
        /// default constructor of the class
        /// sets all its attributes to their default values
        /// </summary>
        public Task()
        {
            todo = string.Empty;
            prio = 0;
            date = DateTime.MinValue;
        }

        /// <summary>
        /// complete constructor of the class
        /// sets all its attributes to the parameter values
        /// </summary>
        /// <param name="todo">sets the todo attribute</param>
        /// <param name="prio">sets the prio attribute</param>
        /// <param name="date">sets the date attribute</param>
        public Task(string todo, PriorityType prio, DateTime date)
        {
            this.todo = todo;
            this.prio = prio;
            this.date = date;
        }

        /// <summary>
        /// property for the todo attribute
        /// </summary>
        public string Todo { get => todo; set => todo = value; }

        /// <summary>
        /// property for the prio attribute
        /// </summary>
        public PriorityType Prio { get => prio; set => prio = value; }

        /// <summary>
        /// property for the date attribute
        /// </summary>
        public DateTime Date { get => date; set => date = value; }

        /// <summary>
        /// return wether all the class' attributes are set to acceptable values
        /// </summary>
        /// <returns></returns>
        public bool CheckData() { return (todo != string.Empty && prio >= 0 && date > DateTime.Now); }
        
        /// <summary>
        /// override Object.ToString()
        /// return a formated string representing the class data
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0,-40} {1,18} {2,50}", ToString(date),ToString(prio), todo);
        }

        /// <summary>
        /// called by ToString()
        /// return a formated string representing the DateTime data
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string ToString(DateTime date)
        {
            string str = string.Format("{0,-5}", date.ToString("ddd"));
            str += string.Format("{0,-4}", date.ToString("dd"));
            str += string.Format("{0,-10}", date.ToString("MMMM"));
            str += string.Format("{0,-20}", date.ToString("yyyy"));
            str += string.Format("{0:D}", date.Hour);
            str += " : ";
            str += string.Format("{0:D}", date.Minute);
            return str;
        }

        /// <summary>
        /// called by ToString()
        /// return a formated string representing the PriorityTipe data
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
        public string ToString(PriorityType priority)
        {
            char [] str = prio.ToString().ToCharArray();
            for (int i = 0; i < str.Length; ++i)
                if (str[i] == '_')
                    str[i] = ' ';
            return new string(str);
            
        }
    }
}
