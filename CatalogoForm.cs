using System.ComponentModel;

namespace CatalogoProdutos;

public partial class CatalogoForm : Form
{
    private readonly BindingList<Produto> produtos = new();
    private Produto? produtoEmEdicao;

    public CatalogoForm()
    {
        MontarTela();
        ConfigurarBindingGrid();

        erros.ContainerControl = this;

        CarregarCategorias();
        ConectarEventos();
        CarregarDadosExemplo();
        AplicarFiltros();
        PrepararNovo();
    }

    private void ConfigurarBindingGrid()
    {
        grade.Columns["Codigo"].DataPropertyName = nameof(Produto.Codigo);
        grade.Columns["Produto"].DataPropertyName = nameof(Produto.Nome);
        grade.Columns["Categoria"].DataPropertyName = nameof(Produto.Categoria);
        grade.Columns["Preco"].DataPropertyName = nameof(Produto.Preco);
        grade.Columns["Estoque"].DataPropertyName = nameof(Produto.Estoque);
    }

    private void CarregarCategorias()
    {
        cmbCategoria.Items.AddRange(new object[] { "Todas", "Periféricos", "Acessórios", "Hardware" });
        cmbCategoria.SelectedIndex = 0;

        cmbCategoriaDados.Items.AddRange(new object[] { "Selecione...", "Periféricos", "Acessórios", "Hardware" });
    }

    private void ConectarEventos()
    {
        btnSalvar.Click += SalvarProduto;
        btnNovo.Click += NovoProduto;
        btnExcluir.Click += ExcluirProduto;
        btnCancelar.Click += (s, e) => PrepararNovo();
        btnFechar.Click += (s, e) => Close();

        btnPesquisar.Click += (s, e) => AplicarFiltros();
        btnLimparFiltros.Click += LimparFiltros;

        grade.CellClick += SelecionarProduto;

        txtPesquisar.TextChanged += (s, e) => AplicarFiltros();
        txtPesquisar.Enter += (s, e) => AcceptButton = null;
        txtPesquisar.Leave += (s, e) => AcceptButton = btnSalvar;
        cmbCategoria.SelectedIndexChanged += (s, e) => AplicarFiltros();
        chkSomenteEstoque.CheckedChanged += (s, e) => AplicarFiltros();

        grade.CellFormatting += FormatarLinhasEstoque;
        btnSelecionarImagem.Click += SelecionarImagem;

        nudPreco.ValueChanged += (s, e) => AtualizarPrecoFinal();
        nudDesconto.ValueChanged += (s, e) => AtualizarPrecoFinal();
        chkEmPromocao.CheckedChanged += (s, e) =>
        {
            lblDesconto.Visible = chkEmPromocao.Checked;
            nudDesconto.Visible = chkEmPromocao.Checked;
            AtualizarPrecoFinal();
        };
    }

    private void NovoProduto(object? sender, EventArgs args)
    {
        PrepararNovo();
    }

    private void PrepararNovo()
    {
        produtoEmEdicao = null;
        txtCodigo.Clear();
        txtNome.Clear();
        nudEstoque.Value = 0;
        nudPreco.Value = 0;
        cmbCategoriaDados.SelectedIndex = 0;
        rbAtivo.Checked = true;
        chkEmPromocao.Checked = false;
        dtpCadastro.Value = DateTime.Now;

        grade.ClearSelection();
        erros.Clear();

        btnSalvar.Text = "Salvar";
        btnExcluir.Enabled = false;
        btnCancelar.Enabled = false;
        txtCodigo.Enabled = true;

        AtualizarPrecoFinal();
        txtCodigo.Focus();

        picProduto.Image?.Dispose();
        picProduto.Image = null;
        nudDesconto.Value = nudDesconto.Minimum;
    }

    private bool ValidarCampos()
    {
        erros.Clear();

        string codigo = txtCodigo.Text.Trim();
        string nome = txtNome.Text.Trim();

        if (codigo.Length == 0)
            erros.SetError(txtCodigo, "Informe o código do produto.");
        else if (produtos.Any(p => p != produtoEmEdicao &&
                 p.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase)))
            erros.SetError(txtCodigo, "Código já cadastrado.");

        if (nome.Length == 0)
            erros.SetError(txtNome, "Informe o nome do produto.");

        if (cmbCategoriaDados.SelectedIndex <= 0)
            erros.SetError(cmbCategoriaDados, "Selecione uma categoria.");

        if (nudPreco.Value <= 0)
            erros.SetError(nudPreco, "Informe um preço maior que zero.");

        // Foca o primeiro campo com erro
        Control[] campos = { txtCodigo, txtNome, cmbCategoriaDados, nudPreco };

        string[] mensagens = campos
            .Select(c => erros.GetError(c))
            .Where(m => m.Length > 0)
            .ToArray();

        if (mensagens.Length == 0)
            return true;

        MessageBox.Show(this, string.Join(Environment.NewLine, mensagens),
            "Corrija os campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        campos.First(c => erros.GetError(c).Length > 0).Focus();
        return false;
    }

    private void SalvarProduto(object? sender, EventArgs args)
    {
        if (!ValidarCampos())
            return;

        bool novoCadastro = produtoEmEdicao is null;

        if (novoCadastro)
        {
            var novo = new Produto
            {
                Codigo = txtCodigo.Text.Trim(),
                Nome = txtNome.Text.Trim(),
                Categoria = cmbCategoriaDados.SelectedItem?.ToString() ?? "",
                Preco = nudPreco.Value,
                Estoque = (int)nudEstoque.Value,
                Ativo = rbAtivo.Checked,
                EmPromocao = chkEmPromocao.Checked,
                DataCadastro = dtpCadastro.Value
            };
            produtos.Add(novo);
        }
        else
        {
            produtoEmEdicao!.Nome = txtNome.Text.Trim();
            produtoEmEdicao.Categoria = cmbCategoriaDados.SelectedItem?.ToString() ?? "";
            produtoEmEdicao.Preco = nudPreco.Value;
            produtoEmEdicao.Estoque = (int)nudEstoque.Value;
            produtoEmEdicao.Ativo = rbAtivo.Checked;
            produtoEmEdicao.EmPromocao = chkEmPromocao.Checked;
            produtoEmEdicao.DataCadastro = dtpCadastro.Value;

            produtos.ResetItem(produtos.IndexOf(produtoEmEdicao));
        }

        LimparFiltros(sender, args);

        PrepararNovo();
    }

    private void SelecionarProduto(object? sender, DataGridViewCellEventArgs args)
    {
        if (args.RowIndex < 0)
            return;

        var produto = grade.Rows[args.RowIndex].DataBoundItem as Produto;
        if (produto is null)
            return;

        produtoEmEdicao = produto;

        txtCodigo.Text = produtoEmEdicao.Codigo;
        txtNome.Text = produtoEmEdicao.Nome;
        cmbCategoriaDados.SelectedItem = produtoEmEdicao.Categoria;
        nudPreco.Value = produtoEmEdicao.Preco;
        nudEstoque.Value = produtoEmEdicao.Estoque;
        rbAtivo.Checked = produtoEmEdicao.Ativo;
        rbInativo.Checked = !produtoEmEdicao.Ativo;
        chkEmPromocao.Checked = produtoEmEdicao.EmPromocao;
        dtpCadastro.Value = produtoEmEdicao.DataCadastro;

        txtCodigo.Enabled = false; 
        btnSalvar.Text = "Atualizar";
        btnExcluir.Enabled = true;
        btnCancelar.Enabled = true;
        erros.Clear();
    }

    private void ExcluirProduto(object? sender, EventArgs args)
    {
        if (produtoEmEdicao is null)
            return;

        DialogResult resposta = MessageBox.Show(this,
            $"Deseja excluir o produto '{produtoEmEdicao.Nome}'?", "Confirmar exclusão",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2);

        if (resposta != DialogResult.Yes)
            return;

        produtos.Remove(produtoEmEdicao);
        AplicarFiltros();
        PrepararNovo();
    }

    private void AplicarFiltros()
    {
        string termo = txtPesquisar.Text.Trim();
        string categoriaSelecionada = cmbCategoria.SelectedItem?.ToString() ?? "Todas";
        bool apenasComEstoque = chkSomenteEstoque.Checked;

        var filtrados = produtos.Where(p =>
            (string.IsNullOrWhiteSpace(termo) ||
             p.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
             p.Codigo.Contains(termo, StringComparison.OrdinalIgnoreCase)) &&
            (categoriaSelecionada == "Todas" || p.Categoria == categoriaSelecionada) &&
            (!apenasComEstoque || p.Estoque > 0)
        ).ToList();

        grade.DataSource = new BindingList<Produto>(filtrados);
        grade.ClearSelection(); 
        lblResultados.Text = $"{filtrados.Count} produtos encontrados";
    }

    private void LimparFiltros(object? sender, EventArgs args)
    {
        txtPesquisar.Clear();
        cmbCategoria.SelectedIndex = 0;
        chkSomenteEstoque.Checked = false;

        AplicarFiltros();
    }

    private void CarregarDadosExemplo()
    {
        produtos.Add(new Produto { Codigo = "P001", Nome = "Teclado USB", Categoria = "Periféricos", Preco = 89.90m, Estoque = 12 });
        produtos.Add(new Produto { Codigo = "P002", Nome = "Mouse sem fio", Categoria = "Periféricos", Preco = 74.50m, Estoque = 0 });
        produtos.Add(new Produto { Codigo = "P003", Nome = "Suporte para notebook", Categoria = "Acessórios", Preco = 119.00m, Estoque = 8 });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            erros.Dispose();
        base.Dispose(disposing);
    }

    private void FormatarLinhasEstoque(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.CellStyle is null)   
        return;

        if (grade.Rows[e.RowIndex].DataBoundItem is not Produto produto)
            return;

        if (produto.Estoque == 0)
            e.CellStyle.BackColor = Color.FromArgb(255, 200, 200);   
        else if (produto.Estoque >= 1 && produto.Estoque <= 5)
            e.CellStyle.BackColor = Color.FromArgb(255, 250, 190);   
    }

    private void SelecionarImagem(object? sender, EventArgs args)
    {
        using var dialogo = new OpenFileDialog
        {
            Title = "Selecionar imagem do produto",
            Filter = "Imagens PNG e JPG|*.png;*.jpg;*.jpeg",
            Multiselect = false
        };

        if (dialogo.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            using var original = Image.FromFile(dialogo.FileName);
            var copia = new Bitmap(original);

            picProduto.Image?.Dispose();
            picProduto.Image = copia;
        }
        catch (Exception ex) when (ex is OutOfMemoryException or IOException)
        {
            MessageBox.Show(this, "O arquivo escolhido não é uma imagem válida.",
                "Imagem inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void AtualizarPrecoFinal()
    {
        decimal preco = nudPreco.Value;

        if (chkEmPromocao.Checked)
            preco -= preco * nudDesconto.Value / 100m;

        lblPrecoFinal.Text = preco.ToString("C2");
    }
}