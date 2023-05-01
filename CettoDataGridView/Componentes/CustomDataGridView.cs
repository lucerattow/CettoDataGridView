using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CettoDataGridView.Componentes
{
    public partial class CustomDataGridView : DataGridView
    {
        public CustomDataGridView()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            
        }
    }
}
