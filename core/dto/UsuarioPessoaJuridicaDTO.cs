using Energytrack.core.domain;

namespace Energytrack.core.DTO
{
    public class UsuarioPessoaJuridicaDTO
    {
        private string _nome;
        private string _apelido;
        private string _serial;
        private string _cnpj;
        public UsuarioPessoaJuridicaDTO() { }
        public UsuarioPessoaJuridicaDTO(string nome, string apelido, string serial, string cnpj)
        {
            this._nome = nome;
            this._apelido = apelido;
            this._serial = serial;
            this._cnpj = cnpj;
        }

        public UsuarioPessoaJuridicaDTO(PessoaJuridica pessoaJuridica)
        {
            this._nome = pessoaJuridica.GetNome();
            List<Medidor> list = pessoaJuridica.GetMedidorList();
            foreach (Medidor item in list)
            {
                this._apelido = item.GetApelido();
                this._serial = item.GetSerial();
            }
            this._cnpj = pessoaJuridica.GetCnpj();
        }
        public string GetApelido() => this._apelido;
        public string GetNome() => this._nome;
        public string GetSerial() => this._serial;
        public void SetNome(string value)
        {
            this._nome = value;
        }
        public void SetApelido(string value)
        {
            this._apelido = value;
        }
        public void SetSerial(string value)
        {
            this._serial = value;
        }
        public List<UsuarioPessoaJuridicaDTO> UsuarioList()
        {
            List<UsuarioPessoaJuridicaDTO> list = new List<UsuarioPessoaJuridicaDTO>();
            list.Add(new UsuarioPessoaJuridicaDTO(this._nome, this._apelido, this._serial, this._cnpj));
            return list;
        }
        public override string ToString()
        {
            return $"Nome: {this._nome}, Apelido: {this._apelido}, Serial: {this._serial}";
        }

    }
}