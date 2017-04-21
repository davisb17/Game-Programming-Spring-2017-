using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine
{
    public partial class Engine : Form
    {
        public static Form form;
        public static Thread renderThread;
        public static Thread updateThread;
        public static Sprite canvas = new Sprite();

        public Engine()
        {
            InitializeComponent();
            DoubleBuffered = true;
            form = this;

            renderThread();

        }
    }
}
