namespace CettoDataGridView
{
    public partial class TestView : Form
    {
        public TestView()
        {
            InitializeComponent();

            GrdSetup();
            GrdRefreshData();
        }

        private void GrdRefreshData()
        {
            cgrd.RowsClear();

            var allData = TEST.DataGenerator.ConsumptionDataGenerator.GetAll();

            foreach (var data in allData)
            {
                cgrd.RowsAdd(new object[]
                {
                    data.Id,
                    data.Type,
                    data.Date.ToString("dd/MM/yyyy"),
                    data.Description,
                    data.Amount
                });
            }
        }

        private void GrdSetup()
        {
            //Configuracion de columnas
            cgrd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "id",
                HeaderText = "Id",
                ReadOnly = true,
                Visible = true,
            }); //0 id
            cgrd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "consumptionType",
                HeaderText = "Tipo de consumo",
                ReadOnly = true,
            }); //1 consumptionType
            cgrd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "date",
                HeaderText = "Fecha",
                ReadOnly = true,
            }); //2 date
            cgrd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "description",
                HeaderText = "Descripcion",
                ReadOnly = true,
            }); //3 date
            cgrd.Columns.Add(new DataGridViewColumn()
            {
                CellTemplate = new DataGridViewTextBoxCell(),
                Name = "amount",
                HeaderText = "Monto",
                ReadOnly = true,
            }); //4 amount
        }
    }
}