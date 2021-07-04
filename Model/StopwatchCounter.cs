using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopwatchApp.Model
{
    public class StopwatchCounter
    {
        private int _allSeconds;
        public int AllSeconds
        {
            get
            {
                return _allSeconds;
            }
            set
            {
                _allSeconds = value;
            }
        }

        public int Seconds
        {
            get
            {
                return AllSeconds % 60;
            }
        }

        public int Minutes
        {
            get
            {
                if (AllSeconds > 60)
                    return (AllSeconds / 60);
                return 0;
            }
        }
        public override string ToString()
        {
            return String.Format("{0}:{1}", Minutes, Seconds); 
        }
        public static StopwatchCounter operator ++(StopwatchCounter c)
        {
            c.AllSeconds++;
            return c;
        }
    }
}
