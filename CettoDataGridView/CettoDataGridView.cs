using CettoDataGridView.Componentes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CettoDataGridView
{
    public partial class CettoDataGridView : Panel
    {
        //fields
        private Color _borderColor;
        private DataGridViewColumnCollection _columns;
        private CustomPanel _container;
        private BindingList<CustomDataGridView> _grds;
        private int _groupGap = 0;
        private List<object[]> _rows;
        private int _rowsHeight;

        //properties
        public Color BorderColor
        {
            get => _borderColor;
            set => _borderColor = value;
        }
        public DataGridViewColumnCollection Columns
        {
            get => _columns;
            set => _columns = value;
        }
        public int GroupGap
        {
            get => _groupGap;
            set => _groupGap = value;
        }
        public int RowsHeight
        {
            get => _rowsHeight;
            set
            {
                _rowsHeight = value;
                _container.ScrollStepSize = value;
            }
        }

        //propiedades sobre escritas
        public new Padding Padding
        {
            get => base.Padding;
            set
            {
                base.Padding = value;

                GrdLocateAndResize();
            }
        }

        #region Hide Properties
        [Browsable(false)]
        public new Size AutoScrollMinSize
        {
            get => base.AutoScrollMinSize;
            set => base.AutoScrollMinSize = value;
        }
        [Browsable(false)]
        public new Size AutoScrollMargin
        {
            get => base.AutoScrollMargin;
            set => base.AutoScrollMargin = value;
        }
        [Browsable(false)]
        public new bool AutoScroll
        {
            get => base.AutoScroll;
            set => base.AutoScroll = value;
        }
        [Browsable(false)]
        public new Image BackgroundImage
        {
            get => base.BackgroundImage;
            set => base.BackgroundImage = value;
        }
        [Browsable(false)]
        public new ImageLayout BackgroundImageLayout
        {
            get => base.BackgroundImageLayout;
            set => base.BackgroundImageLayout = value;
        }
        [Browsable(false)]
        public new BorderStyle BorderStyle
        {
            get => base.BorderStyle;
            set => base.BorderStyle = value;
        }
        #endregion

        //constructor
        public CettoDataGridView()
        {
            InitializeComponent();

            //Inicializo las propiedades
            this.DoubleBuffered = true; //Esto es necesario para evitar el parpadeo al redibujar el control
            this.AutoScroll = false;

            _container = new CustomPanel();
            this.Controls.Add(_container);

            CettoDgvInitialize();
            GrdInitialize();
        }

        //methods
        public void RowsAdd(object[] row)
        {
            _rows.Add(row);
            GrdRefreshData();
        }

        public void RowsClear()
        {
            _rows.Clear();
            GrdRefreshData();
        }

        private void CettoDgvInitialize()
        {
            //Subcomponentes
            var _internalDgv = new DataGridView();
            _grds = new();
            _grds.ListChanged += OnGrdsChange;

            Columns = _internalDgv.Columns;
            Columns.CollectionChanged += OnColumnsChange;

            _rows = new();

            //Propiedades
            RowsHeight = 23;
        }

        private void GrdInitialize()
        {
            var grd = new CustomDataGridView();
            grd.ScrollBars = ScrollBars.None;
            grd.RowTemplate.Height = _rowsHeight;

            _container.ControlsAdd(grd);
            _grds.Add(grd);
        }

        private void GrdLocateAndResize()
        {
            int acumulateTop = this.Padding.Top;
            foreach (var grd in _grds)
            {
                grd.Location = new Point(this.Padding.Left, acumulateTop);

                //Obtengo el height
                int rowsHeight = 0;
                rowsHeight = grd.RowTemplate.Height * grd.Rows.Count;
                rowsHeight += grd.ColumnHeadersHeight;

                //Obtengo el Width
                int columnsWidth = 0;
                foreach (DataGridViewColumn col in grd.Columns)
                    columnsWidth += col.Width;
                columnsWidth += grd.RowHeadersWidth +1;
                
                grd.Size = new Size(columnsWidth, rowsHeight);
                acumulateTop += grd.Height + _groupGap;
            }

            acumulateTop += this.Padding.Bottom;
            _container.SetInternalHeight(acumulateTop);
        }

        private void GrdRefreshData()
        {
            foreach (var grd in _grds)
            {
                grd.Rows.Clear();

                foreach (var row in _rows)
                {
                    grd.Rows.Add(row);
                }
            }

            GrdLocateAndResize();
        }

        //events
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            #region BorderColor
            // Obtiene el objeto Graphics para dibujar en el panel
            Graphics g = e.Graphics;

            // Define el color y el ancho del borde
            Color borderColor = _borderColor;
            int borderWidth = 1;

            // Crea un rectángulo con el tamaño del panel menos 1 píxel en cada dimensión
            // Esto es necesario para que el borde no se dibuje fuera del panel
            Rectangle borderRect = new Rectangle(0, 0, this.ClientSize.Width - borderWidth, this.ClientSize.Height - borderWidth);

            // Dibuja el borde alrededor del rectángulo
            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                g.DrawRectangle(pen, borderRect);
            }
            #endregion
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            _container.Location = new Point(1, 1);
            _container.Size = new Size(this.Width - 2, this.Height - 2);

            GrdLocateAndResize();
        }

        private void OnGrdsChange(object sender, EventArgs e)
        {
            GrdLocateAndResize();
        }

        private void OnColumnsChange(object sender, EventArgs e)
        {
            foreach (var grd in _grds)
            {
                grd.Columns.Clear();
                foreach (DataGridViewColumn column in _columns)
                {
                    var newColumn = (DataGridViewColumn)column.Clone();
                    grd.Columns.Add(newColumn);
                }
            }
        }
    }
}