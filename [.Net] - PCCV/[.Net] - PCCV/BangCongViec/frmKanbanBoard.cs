namespace BangCongViec;

public partial class frmKanbanBoard : Form
{
    public frmKanbanBoard()
    {
        InitializeComponent();
        LoadSampleData();
    }

    private void btnTaoMoi_Click(object sender, EventArgs e)
    {
        using FrmTaoMoiGiaoCongViec frmTaoMoi = new();
        frmTaoMoi.ShowDialog(this);
    }

    private void LoadSampleData()
    {
        dgvCanLam.Rows.Add("CV001", "Lap ke hoach", "An", "24/05/2026", "Cao");
        dgvCanLam.Rows.Add("CV002", "Thu thap yeu cau", "Binh", "25/05/2026", "Trung binh");

        dgvDangLam.Rows.Add("CV003", "Thiet ke giao dien", "Chi", "26/05/2026", "Cao");
        dgvDangLam.Rows.Add("CV004", "Tao co so du lieu", "Dung", "27/05/2026", "Trung binh");

        dgvHoanThanh.Rows.Add("CV005", "Tao project", "Ha", "22/05/2026", "Thap");
        dgvHoanThanh.Rows.Add("CV006", "Phan cong nhom", "Linh", "23/05/2026", "Trung binh");
    }
}
