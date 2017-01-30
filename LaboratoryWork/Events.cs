using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaboratoryWork
{
    public static class Delegates//убрать статик
    {
        public delegate void MyEvent(int Button);
        public static MyEvent EnableButtonsGraphic;

        public delegate void ChangeConsts();
        public static ChangeConsts OnChangeConsts;

        public delegate void ChangeTypeFunctions();
        public static ChangeTypeFunctions OnChangeTypeFunctions;
    }
}
