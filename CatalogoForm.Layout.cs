using Microsoft.VisualBasic;

namespace CatalogoProdutos;

public partial class CatalogoForm
{
    private void MontarTela()
    {
        Text = "Catálogo de Produtos";
        ClientSize = new Size(760, 580);
        MinimumSize = new Size(600, 480);
        StartPosition = FormStartPosition.CenterScreen;
        AutoScaleMode = AutoScaleMode.Font;
        AcceptButton = btnSalvar;

        var lblPesquisar = new Label { Text = "Pesquisar por nome ou código", AutoSize = true };
        var lblCategoria = new Label { Text = "Categoria", AutoSize = true };

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12, 12, 12, 6),
            ColumnCount = 3,
            RowCount = 7
        };

        layout.ColumnStyles.Clear();
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

        layout.RowStyles.Clear();
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); 
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     

        var pnlCabecalho = new TableLayoutPanel
        {
            BackColor = Color.FromArgb(27, 79, 114),
            ForeColor = Color.White,
            Dock = DockStyle.Fill,
            Height = 35,
            ColumnCount = 2,
            RowCount = 1,
            Margin = new Padding(0, 0, 0, 10)
        };
        pnlCabecalho.ColumnStyles.Clear();
        pnlCabecalho.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        pnlCabecalho.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

        var lblTitulo = new Label { Text = "Catálogo de Produtos", Font = new Font(Control.DefaultFont, FontStyle.Bold), AutoSize = true, Anchor = AnchorStyles.Left };
        var lblUsuario = new Label { Text = "Usuário: atendente", AutoSize = true, Anchor = AnchorStyles.Right };

        pnlCabecalho.Controls.Add(lblTitulo, 0, 0);
        pnlCabecalho.Controls.Add(lblUsuario, 1, 0);

        layout.Controls.Add(pnlCabecalho, 0, 0);
        layout.SetColumnSpan(pnlCabecalho, 3);

        layout.Controls.Add(lblPesquisar, 0, 1);
        layout.Controls.Add(lblCategoria, 1, 1);

        layout.Controls.Add(txtPesquisar, 0, 2);
        layout.Controls.Add(cmbCategoria, 1, 2);
        layout.Controls.Add(chkSomenteEstoque, 2, 2);


        var painelBotoesPesquisa = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.LeftToRight, Margin = new Padding(0) };
        painelBotoesPesquisa.Controls.Add(btnPesquisar);
        painelBotoesPesquisa.Controls.Add(btnLimparFiltros);

        layout.Controls.Add(painelBotoesPesquisa, 0, 3);
        layout.Controls.Add(lblResultados, 2, 3);

        layout.Controls.Add(grade, 0, 4);
        layout.SetColumnSpan(grade, 3);

        grade.Columns.Clear();
        grade.Columns.Add("Codigo", "Código");
        grade.Columns.Add("Produto", "Produto");
        grade.Columns.Add("Categoria", "Categoria");
        grade.Columns.Add("Preco", "Preço");
        grade.Columns.Add("Estoque", "Estoque");

        grade.Columns["Codigo"].FillWeight = 60;      
        grade.Columns["Produto"].FillWeight = 200;    
        grade.Columns["Categoria"].FillWeight = 120;  
        grade.Columns["Preco"].FillWeight = 80;       
        grade.Columns["Estoque"].FillWeight = 60;

        grade.Columns["Preco"].DefaultCellStyle.Format = "C2";

        foreach (DataGridViewColumn coluna in grade.Columns)
        {
            coluna.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        grade.Columns["Preco"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        grade.Columns["Estoque"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        // Linha 5: Dados do Produto
        var gbDados = new GroupBox
        {
            Text = "Dados do produto",
            Dock = DockStyle.Fill,
            Margin = new Padding(0, 8, 0, 8),
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };

        var layoutDados = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            ColumnCount = 5,
            RowCount = 6,
            Padding = new Padding(5),
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };

        layoutDados.ColumnStyles.Clear();
        layoutDados.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        layoutDados.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        layoutDados.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        layoutDados.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        layoutDados.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

        layoutDados.RowStyles.Clear();
        layoutDados.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layoutDados.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layoutDados.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layoutDados.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layoutDados.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layoutDados.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        layoutDados.Controls.Add(new Label { Text = "Código", AutoSize = true }, 0, 0);
        layoutDados.Controls.Add(txtCodigo, 1, 0);

        layoutDados.Controls.Add(new Label { Text = "Nome", AutoSize = true }, 0, 1);
        layoutDados.Controls.Add(txtNome, 1, 1);

        layoutDados.Controls.Add(new Label { Text = "Estoque", AutoSize = true }, 0, 2);
        layoutDados.Controls.Add(nudEstoque, 1, 2);

        var painelSituacao = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true };
        painelSituacao.Controls.Add(new Label { Text = "Situação:", AutoSize = true, Margin = new Padding(0, 5, 5, 0) });
        painelSituacao.Controls.Add(rbAtivo);
        painelSituacao.Controls.Add(rbInativo);
        layoutDados.Controls.Add(painelSituacao, 0, 3);
        layoutDados.SetColumnSpan(painelSituacao, 2);

        layoutDados.Controls.Add(new Label { Text = "Categoria", AutoSize = true }, 2, 0);
        layoutDados.Controls.Add(cmbCategoriaDados, 3, 0);

        layoutDados.Controls.Add(new Label { Text = "Preço", AutoSize = true }, 2, 1);
        layoutDados.Controls.Add(nudPreco, 3, 1);

        layoutDados.Controls.Add(new Label { Text = "Cadastro", AutoSize = true }, 2, 2);
        layoutDados.Controls.Add(dtpCadastro, 3, 2);

        layoutDados.Controls.Add(chkEmPromocao, 3, 3);

        var painelImagem = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoSize = true,
            Margin = new Padding(10, 0, 0, 0)
        };
        painelImagem.Controls.Add(picProduto);
        painelImagem.Controls.Add(btnSelecionarImagem);

        layoutDados.Controls.Add(painelImagem, 4, 0);
        layoutDados.SetRowSpan(painelImagem, 4);

        layoutDados.Controls.Add(lblDesconto, 2, 4);
        layoutDados.Controls.Add(nudDesconto, 3, 4);

        layoutDados.Controls.Add(new Label { Text = "Preço final", AutoSize = true }, 2, 5);
        layoutDados.Controls.Add(lblPrecoFinal, 3, 5);

        gbDados.Controls.Add(layoutDados);
        layout.Controls.Add(gbDados, 0, 5);
        layout.SetColumnSpan(gbDados, 3);

        var painelBotoesAcao = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 6,
            RowCount = 1,
            Margin = new Padding(0),
            AutoSize = true
        };

        painelBotoesAcao.ColumnStyles.Clear();
        painelBotoesAcao.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));     
        painelBotoesAcao.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));     
        painelBotoesAcao.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));     
        painelBotoesAcao.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); 
        painelBotoesAcao.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));     
        painelBotoesAcao.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));     

        painelBotoesAcao.RowStyles.Clear();
        painelBotoesAcao.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        painelBotoesAcao.Controls.Add(btnNovo, 0, 0);
        painelBotoesAcao.Controls.Add(btnSalvar, 1, 0);
        painelBotoesAcao.Controls.Add(btnExcluir, 2, 0);
        painelBotoesAcao.Controls.Add(btnCancelar, 4, 0);
        painelBotoesAcao.Controls.Add(btnFechar, 5, 0);

        layout.Controls.Add(painelBotoesAcao, 0, 6);
        layout.SetColumnSpan(painelBotoesAcao, 3);

        // Cor do cabeçalho
        grade.ColumnHeadersDefaultCellStyle.SelectionBackColor = grade.ColumnHeadersDefaultCellStyle.BackColor;
        grade.ColumnHeadersDefaultCellStyle.SelectionForeColor = grade.ColumnHeadersDefaultCellStyle.ForeColor;

        Controls.Add(layout);
    }

    private readonly DateTimePicker dtpCadastro = new()
    {
        Dock = DockStyle.Fill,
        Format = DateTimePickerFormat.Short
    };

    private readonly RadioButton rbAtivo = new()
    {
        Text = "Ativo",
        Checked = true,
        AutoSize = true
    };

    private readonly RadioButton rbInativo = new()
    {
        Text = "Inativo",
        AutoSize = true
    };

    private readonly CheckBox chkEmPromocao = new()
    {
        Text = "Produto em promoção",
        AutoSize = true,
        Anchor = AnchorStyles.Right
    };

    private readonly ComboBox cmbCategoriaDados = new()
    {
        Dock = DockStyle.Fill,
        DropDownStyle = ComboBoxStyle.DropDownList
    };

    private readonly TextBox txtPesquisar = new()
    {
        Dock = DockStyle.Fill,
        MaxLength = 100,
        TabIndex = 0,
        PlaceholderText = "Digite para pesquisar..."
    };

    private readonly TextBox txtCodigo = new()
    {
        Dock = DockStyle.Fill,
        MaxLength = 100,
        TabIndex = 0
    };

    private readonly TextBox txtNome = new()
    {
        Dock = DockStyle.Fill,
        MaxLength = 100,
        TabIndex = 0
    };

    private readonly NumericUpDown nudEstoque = new()
    {
        Dock = DockStyle.Fill,
        TabIndex = 101,
        Maximum = 100000,
    };

    private readonly NumericUpDown nudPreco = new()
    {
        Dock = DockStyle.Fill,
        DecimalPlaces = 2,
        Maximum = 100000,
        TabIndex = 101
    };

    private readonly ComboBox cmbCategoria = new()
    {
        Dock = DockStyle.Fill,
        TabIndex = 1,
        DropDownStyle = ComboBoxStyle.DropDownList
    };

    private readonly CheckBox chkSomenteEstoque = new()
    {
        Text = "Somente em estoque",
        AutoSize = true,
        TabIndex = 2
    };

    private readonly Label lblResultados = new()
    {
        Text = "0 produtos encontrados",
        AutoSize = true,
        Font = new Font(Control.DefaultFont, FontStyle.Bold),
        Anchor = AnchorStyles.Right | AnchorStyles.Bottom
    };

    private readonly Button btnPesquisar = new()
    {
        Text = "Pesquisar",
        AutoSize = true,
        TabIndex = 0
    };

    private readonly Button btnLimparFiltros = new()
    {
        Text = "Limpar filtros",
        AutoSize = true,
        TabIndex = 0
    };

    private readonly Button btnNovo = new()
    {
        Text = "Novo",
        AutoSize = true,
        TabIndex = 0
    };

    private readonly Button btnSalvar = new()
    {
        Text = "Salvar",
        AutoSize = true,
        TabIndex = 0
    };

    private readonly Button btnExcluir = new()
    {
        Text = "Excluir",
        AutoSize = true,
        TabIndex = 0
    };

    private readonly Button btnCancelar = new()
    {
        Text = "Cancelar",
        AutoSize = true,
        TabIndex = 0
    };

    private readonly Button btnFechar = new()
    {
        Text = "Fechar ",
        AutoSize = true,
        TabIndex = 0
    };

    private readonly PictureBox picProduto = new()
    {
        Size = new Size(120, 90),
        SizeMode = PictureBoxSizeMode.Zoom,   
        BorderStyle = BorderStyle.FixedSingle,
        Margin = new Padding(0, 0, 0, 6)
    };

    private readonly Button btnSelecionarImagem = new()
    {
        Text = "Selecionar imagem",
        AutoSize = true
    };

    private readonly Label lblDesconto = new()
    {
        Text = "Desconto (%)",
        AutoSize = true,
        Visible = false
    };

    private readonly NumericUpDown nudDesconto = new()
    {
        Dock = DockStyle.Fill,
        Minimum = 1,
        Maximum = 70,
        Visible = false
    };

    private readonly Label lblPrecoFinal = new()
    {
        AutoSize = true,
        Font = new Font(Control.DefaultFont, FontStyle.Bold)
    };

    private readonly DataGridView grade = new()
    {
        Dock = DockStyle.Fill,
        ReadOnly = true,
        TabIndex = 3,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        MultiSelect = false,
        RowHeadersVisible = false,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,

        AutoGenerateColumns = false,

        EnableHeadersVisualStyles = false,

    };

    private readonly ErrorProvider erros = new();

}