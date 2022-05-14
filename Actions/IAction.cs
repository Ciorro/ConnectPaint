using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Actions
{
    internal interface IAction
    {
        public void Do();
        public void Undo();
    }
}
