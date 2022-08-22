using System;
using System.Collections.Concurrent;
using DigiBank.Classes;

namespace DigiBank.Contratos
{
    public interface IConta
    {
        void Deposita(double valor);
        bool Saca(double valor);
        double ConsultaSaldo();
        string GetCodigoDoBanco();
        string GetNumeroDaAgencia();
        string GetNumeroDaConta();
        List<Extrato> Extrato();
    }
}
