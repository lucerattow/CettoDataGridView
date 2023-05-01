using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CettoDataGridView.Componentes
{
    public partial class CustomPanel : Panel
    {
        //fields
        private Panel contentPanel;
        private VScrollBar vScrollBar;
        private int _scrollStepSize; 

        //properties
        public int ScrollStepSize
        { 
            get => _scrollStepSize;
            set
            {
                _scrollStepSize = value;
                UpdateScrollBar();
            }
        }

        public CustomPanel()
        {
            InitializeComponent();
            Initialize();

            this.DoubleBuffered = true;
            this.AutoScroll = false;

            this.Resize += (sender, e) => { this.Invalidate(); };

            //Inicializo las propiedades
            ScrollStepSize = 1;
        }

        //Methods
        private void Initialize()
        {
            contentPanel = new Panel();
            vScrollBar = new VScrollBar();

            this.Controls.Add(contentPanel);
            this.Controls.Add(vScrollBar);

            this.AutoScroll = false;

            contentPanel.BackColor = Color.Gray;
            contentPanel.Location = new Point(0, 0);
            contentPanel.Size = new Size(this.ClientSize.Width - vScrollBar.Width, this.ClientSize.Height);
            contentPanel.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            contentPanel.MouseWheel += ContentPanel_MouseWheel;
            contentPanel.Resize += ContentPanel_Resize;

            vScrollBar.Dock = DockStyle.Right;
            vScrollBar.Minimum = this.ClientSize.Height;
            vScrollBar.LargeChange = _scrollStepSize * 5;
            vScrollBar.SmallChange = _scrollStepSize;
            vScrollBar.ValueChanged += VScrollBar_ValueChanged;

            UpdateScrollBar();
        }

        private void UpdateScrollBar()
        {
            vScrollBar.Minimum = 0;
            vScrollBar.Maximum = contentPanel.Height - 1; // La altura máxima será la altura del contentPanel
            vScrollBar.LargeChange = this.Height; // El cambio mayor será igual a la altura del CustomPanel
            vScrollBar.SmallChange = _scrollStepSize; // El cambio menor es el incremento en unidades que se aplica al hacer clic en las flechas de la barra de desplazamiento
        }

        public void ControlsAdd(Control control)
        {
            this.contentPanel.Controls.Add(control);
        }

        public void ControlsRemove(Control control)
        {
            this.contentPanel.Controls.Remove(control);
        }

        public void ControlsClear()
        {
            this.contentPanel.Controls.Clear();
        }

        public void SetInternalHeight(int height)
        {
            contentPanel.Height = height;
            contentPanel.Location = new Point(0, 0);
            contentPanel.Size = new Size(this.Width - vScrollBar.Width, contentPanel.Height);

            vScrollBar.Maximum = height - this.ClientSize.Height + vScrollBar.LargeChange - 1;

            UpdateScrollBar(); // Actualiza las propiedades del VScrollBar
        }

        //events
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateScrollBar();
        }

        private void ContentPanel_Resize(object sender, EventArgs e)
        {
            UpdateScrollBar();
        }

        private void ContentPanel_MouseWheel(object? sender, MouseEventArgs e)
        {
            // La propiedad Delta indica la cantidad de movimiento de la rueda del mouse
            // Un valor positivo indica que la rueda se movió hacia arriba, un valor negativo indica que se movió hacia abajo

            int scrollAmount = e.Delta / 120; // La cantidad de movimiento en "notches" (un notch es igual a 120 unidades en Delta)
            int newScrollValue = vScrollBar.Value - (scrollAmount * vScrollBar.SmallChange);

            // Asegúrate de que el nuevo valor esté dentro del rango permitido
            newScrollValue = Math.Max(vScrollBar.Minimum, Math.Min(vScrollBar.Maximum - vScrollBar.LargeChange + 1, newScrollValue));

            // Establece el nuevo valor de la barra de desplazamiento
            vScrollBar.Value = newScrollValue;
        }

        private void VScrollBar_ValueChanged(object sender, EventArgs e)
        {
            int adjustedValue = vScrollBar.Value;
            int maxPossibleValue = vScrollBar.Maximum - vScrollBar.LargeChange - vScrollBar.SmallChange + 2;

            if (vScrollBar.SmallChange != 0)
            {
                // Calcula el nuevo valor ajustado según el SmallChange
                adjustedValue = (vScrollBar.Value / vScrollBar.SmallChange) * vScrollBar.SmallChange;

                // Comprueba si el nuevo valor ajustado supera el límite máximo posible
                if (adjustedValue > maxPossibleValue)
                {
                    // Reduzca el valor ajustado para que se muestren los últimos píxeles del contentPanel
                    adjustedValue -= vScrollBar.SmallChange;
                }

                // Asegúrate de que el valor ajustado esté dentro del rango válido
                adjustedValue = Math.Max(vScrollBar.Minimum, adjustedValue);
                adjustedValue = Math.Min(maxPossibleValue, adjustedValue);
            }

            // Actualiza el valor de la barra de desplazamiento
            vScrollBar.Value = adjustedValue;

            // Actualiza la posición del contentPanel
            contentPanel.Location = new Point(0, -vScrollBar.Value);
        }
    }
}
