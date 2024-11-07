using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Energytrack.core.domain
{
    public class PessoaJuridica : Usuario
    {
        private string _cnpj;

        public PessoaJuridica() {}

        public PessoaJuridica(string cnpj, string nome, List<Medidor> medidorList) : base(nome, medidorList)
        {
            this._cnpj = cnpj;
        }

        public string GetCnpj() => this._cnpj;


    }
}