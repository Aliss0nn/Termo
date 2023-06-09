using System.Windows.Forms;

namespace Termo
{
    public partial class Form1 : Form
    {
        private JogodasPalavras jogoPalavras;
        private int palavra;
        private int letra;
        private Button[] botoesDigitados;


        public Form1()
        {
            InitializeComponent();
            btnEnter.Enabled = false;
        }

        private int inicial = 1;
        private int final = 5;
        public string palavraChutada = "";

        JogodasPalavras jogo = new JogodasPalavras();

        private void AcaoChute_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                string letra = btn.Text;

                if (DivisivelPorCinco())
                {
                    EscreverNaCaixa(letra);
                    DesabilitarTeclado();

                }
                else
                    EscreverNaCaixa(letra);

            }

            jogo.contador++;
        }

        private void EscreverNaCaixa(string letra)
        {
            foreach (TextBox textBox in panel.Controls.OfType<TextBox>().OrderBy(tb => tb.Tag))
            {

                string strTag = textBox.Tag.ToString();

                int tag = Convert.ToInt32(strTag);

                if (tag == jogo.contador)
                {
                    textBox.Text = letra;
                    break;
                }
            }
        }

        private bool DivisivelPorCinco()
        {

            if (jogo.contador % 5 == 0 && jogo.contador != 0)
            {
                btnEnter.Enabled = true;
                return true;
            }

            else
                return false;
        }

        private void DesabilitarTeclado()
        {
            foreach (Control control in pnBotoes.Controls)
            {
                if (control is Button button)
                    button.Enabled = false;
            }
        }

        private void LiberarTeclado()
        {
            foreach (Control control in pnBotoes.Controls)
            {
                if (control is Button button)
                    button.Enabled = true;
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {

            VerificarSeAcertou();

            LiberarTeclado();
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            foreach (TextBox textBox in panel.Controls.OfType<TextBox>().OrderBy(tb => tb.Tag))
            {


                string strTag = textBox.Tag.ToString();

                int tag = Convert.ToInt32(strTag);

                if (tag == jogo.contador - 1)
                {
                    if (textBox.WordWrap == false)
                    {
                        MessageBox.Show("Nao pode apagar um elemento ja verificado!");
                        return;
                    }

                    textBox.Text = "";
                    jogo.contador--;
                    break;
                }
            }
        }

        private void VerificarSeAcertou()
        {
            string palavra = jogo.

            palavraChutada = PegarPalavraCaixa();



            foreach (TextBox textBox in panel.Controls.OfType<TextBox>())
            {
                string strTag = textBox.Tag.ToString();
                int tag = Convert.ToInt32(strTag);

                if (tag >= inicial && tag <= final)
                {

                    if (palavra.Contains(textBox.Text.ToLower()))
                    {
                        int index = palavra.IndexOf(textBox.Text.ToLower());

                        try
                        {
                            if (palavra[index].ToString().ToLower() != palavraChutada[index].ToString().ToLower())
                            {
                                textBox.BackColor = Color.Yellow;
                                textBox.WordWrap = false;

                                continue;
                            }

                            if (palavra[index].ToString().ToLower() == palavraChutada[index].ToString().ToLower())
                            {
                                textBox.BackColor = Color.DarkGreen;
                                textBox.WordWrap = false;
                                continue;
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            PegarPalavraCaixa();
                        }


                    }

                    else
                    {
                        textBox.BackColor = Color.DarkGray;
                        textBox.WordWrap = false;

                    }
                }

                else
                    continue;
            }

            if (JogadorVenceu(palavra))
                return;

            if (JogadorPerdeu())
                return;

            inicial += 5;
            final += 5;
        }

        private bool JogadorPerdeu()
        {
            if (jogo.contador > 25)
            {
                MessageBox.Show($"Vc perdeu a palavra era: {jogo}");
                this.jogo = new JogodasPalavras();
                this.inicial = 1;
                this.final = 5;

                Limpar();
                return true;
            }
            return false;
        }

        private bool JogadorVenceu(string palavra)
        {
            if (palavraChutada.ToLower() == palavra.ToLower())
            {
                MessageBox.Show("Win");
                this.jogo = new JogodasPalavras();
                this.inicial = 1;
                this.final = 5;

                Limpar();
                return true;
            }

            return false;
        }

        private void Limpar()
        {
            foreach (TextBox textBox in panel.Controls.OfType<TextBox>())
            {
                textBox.Text = "";
                textBox.WordWrap = true;
                textBox.BackColor = SystemColors.Window;
            }
        }

        private string PegarPalavraCaixa()
        {
            string palavra = "";
            int ponto = inicial;

            foreach (TextBox textBox in panel.Controls.OfType<TextBox>().OrderBy(tb => tb.Tag))
            {
                string strTag = textBox.Tag.ToString();
                int tag = Convert.ToInt32(strTag);


                if (tag == ponto && tag <= final)
                {
                    palavra += textBox.Text;
                    ponto++;

                    if (palavra.Length == 5)
                        break;
                }
            }

            return palavra;
        }
    }

}



