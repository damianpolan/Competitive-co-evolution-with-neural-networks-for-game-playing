using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using COMP4106_Project.Game;

namespace COMP4106_Project
{
    public partial class TestingForm : Form
    {
        public TestingForm()
        {
            InitializeComponent();
            Board b = new Board();
        }
    }
}
