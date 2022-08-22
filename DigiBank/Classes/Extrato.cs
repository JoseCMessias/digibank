namespace DigiBank.Classes
{
    public class Extrato
    {
        public Extrato(DateTime date, string descricao, double valor)
        {
            Data = date;
            Descricao = descricao;
            Valor = valor;
        }

        public DateTime Data { get; private set; }
        public string Descricao { get; private set; }
        public double Valor { get; private set; }
    }
}
